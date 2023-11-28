<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="ProposalDetailsService.aspx.cs" Inherits="Portal_Proposal_ProposalDetailsService" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
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
        .panel .panel-heading h4
        {
            float: none;
            font-size: 15px;
        }
        
        .panel-group .panel
        {
            border-radius: 0;
            box-shadow: none;
            border-color: #EEEEEE;
            box-shadow: 0px 0px 5px #b3b3b3;
        }
        
        .panel-default > .panel-heading
        {
            padding: 0;
            border-radius: 0;
            color: #212121;
            background-color: #FAFAFA;
            border-color: #EEEEEE;
        }
        
        .panel-title
        {
            font-size: 14px;
        }
        
        .panel-title > a
        {
            display: block;
            padding: 6px;
            text-decoration: none;
        }
        
        .more-less
        {
            float: right;
            color: #212121;
            margin-top: 5px;
            margin-right: 10px;
        }
        
        .panel-default > .panel-heading + .panel-collapse > .panel-body
        {
            border-top-color: #EEEEEE;
        }
        .table tr td a
        {
            color: #337ab7;
            font-size: 14px;
        }
        .table tr td a .fa
        {
            font-size: 15px;
            display: block;
            margin-bottom: 10px;
        }
        
        
        @media print
        {
            .navbar-brand
            {
                float: left;
            }
            .header-investorDetails
            {
                display: none;
            }
            .investrs-tab
            {
                display: none;
            }
            .col-sm-4
            {
                width: 33%;
                float: left;
            }
            label
            {
                font-weight: 400;
            }
            .iconsdiv, .footer-wrapper
            {
                display: none;
            }
            .form-group
            {
                margin-bottom: 8px;
            }
            header
            {
                border-bottom: 1px solid #ccc;
            }
            .form-header
            {
                padding: 0px;
                border-bottom: 0px;
            }
            .form-body
            {
                padding: 10px 0px;
            }
            .form-sec h2
            {
                font-weight: 400;
                color: #000;
                border-bottom: 0px;
                background: #ccc;
            }
            .form-sec
            {
                margin-bottom: 10px;
            }
            .col-sm-3
            {
                width: 25%;
                float: left;
            }
            .col-sm-6
            {
                width: 50%;
                float: left;
            }
            .collapse, collapsed
            {
                display: block;
                height: auto !important;
            }
            .more-less
            {
                display: none;
            }
            .navbar-toggle
            {
                display: none;
            }
            .scrollup
            {
                display: none;
            }
            .panel-title
            {
                font-weight: 400;
            }
            .panel-body .h4-header, .table > tbody > tr > th
            {
                font-weight: 400;
            }
            a[href]:after
            {
                content: none !important;
            }
        }
    </style>
    <div class="content-wrapper">
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Add Take Action</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Mange users</a></li><li><a>Take Action</a></li></ul>
               </div>
            </section>
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12 lobipanel-parent-sortable ui-sortable" data-lobipanel-child-inner-id="CgbyYkSXVZ">
        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingOne">
                    <h4 class="panel-title">
                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                            aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa  fa-minus">
                            </i>Company Information</a>
                    </h4>
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                 <div  class="col-sm-12">
                                            <label for="Iname">
                                                Name of the company/enterprise M/S :</label>
                                                <asp:Label ID="lblCompName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                       
                            </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Corporate Office Address</h4>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-3">
                                    <label for="Iname">
                                        Address :</label>
                                    <asp:Label ID="lblAddress" runat="server"> </asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="Country">
                                        Country :</label>
                                    <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="State">
                                        State :</label>
                                    <asp:Label ID="lblState" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="Address2">
                                        City :</label>
                                    <asp:Label ID="lblCity" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-3">
                                    <label for="MobileNo">
                                       Phone number :</label>
                                       <asp:Label ID="lblISDPHNo" runat="server"></asp:Label>
                                    <asp:Label ID="lblPhoneNo" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="FaxNo">
                                        Fax number :</label>
                                        <asp:Label ID="lblISDFXNo" runat="server"></asp:Label>
                                    <asp:Label ID="lblFaxNo" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="Email">
                                        Email iD :</label>
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="Pin Code">
                                        Pin code :</label>
                                    <asp:Label ID="lblPin" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Correspondence Address
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <label for="Iname">
                                        Name of the contact person :</label>
                                    <asp:Label ID="lblContactPerson" runat="server"> </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-3">
                                    <label for="Iname">
                                        Address :</label>
                                    <asp:Label ID="lblCorAdd" runat="server"> </asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="Country">
                                        Country :</label>
                                    <asp:Label ID="lblCorCountry" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="State">
                                        State :</label>
                                    <asp:Label ID="lblCorState" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="Address2">
                                        City :</label>
                                    <asp:Label ID="lblCorCity" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-3">
                                    <label for="MobileNo">
                                        Mobile number :</label>
                                        <asp:Label ID="lblISDMOB" runat="server"></asp:Label>
                                    <asp:Label ID="lblCorMobileNo" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="FaxNo">
                                        Fax number :</label>
                                        <asp:Label ID="lblFaxCordet" runat="server"></asp:Label>
                                    <asp:Label ID="lblCorFaxNo" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="Email">
                                        Email iD :</label>
                                    <asp:Label ID="lblCorEmail" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <label for="Pin Code">
                                        Pin code :</label>
                                    <asp:Label ID="lblCorrPin" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-6">
                                    <label for="MobileNo">
                                        Constitution of company/enterprise :</label>
                                    <asp:Label ID="lblOtheConstituition" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-6" id="dvConstothers" runat="server">
                                    <label for="FaxNo">
                                        Others (Please specify) :</label>
                                    <asp:Label ID="lblOthers" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div id="dvPromoter" runat="server">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Promoter details</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <%-- <asp:GridView ID="grdPromoName" class="table table-bordered table-hover" runat="server"
                                                            AutoGenerateColumns="false" Width="100%" EmptyDataText="No Record(s) Found...">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl#">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="vchNameOfPromoter" HeaderText="Name of Promoter" />
                                                            </Columns>
                                                        </asp:GridView>--%>
                                        <label for="Partner">
                                            Name of promoter :</label>
                                        <asp:Label ID="lblNameOfPromoter" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="dvPartnership" runat="server">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Partnership details</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <label for="Partner">
                                            Number of partner(s) :</label>
                                        <asp:Label ID="lblNumberOfPartner" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="ManagingPartner">
                                            Name of the managing partner :</label>
                                        <asp:Label ID="lblManagPartner" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="dvBoard" runat="server">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Board of directors</h4>
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
                                <%--  <div class="col-sm-4">
                                                <label for="Email">
                                                    Shareholding pattern :</label>
                                                <asp:Label ID="lblSharPatt" runat="server"></asp:Label> <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Download"><i class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                            </div>--%>
                                <div class="col-sm-3">
                                    <label for="Iname">
                                        Educational qualification :</label>
                                    <asp:Label ID="lblEduQualif" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnQ" runat="server" />
                                    <asp:HiddenField ID="hdnEdu" runat="server" />
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank">
                                                   <i class="fa fa-download" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Iname">
                                        Technical qualification :</label>
                                    <asp:Label ID="lblTechQual" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnTechQ" runat="server" />
                                    <asp:HiddenField ID="hdnTecnical" runat="server" />
                                    <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank">
                                                    <i class="fa fa-download" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Iname">
                                        Experience in years :</label>
                                    <asp:Label ID="lblExperience" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnExperience" runat="server" />
                                    <asp:HyperLink ID="hlDoc7" runat="server" Target="_blank">
                                                   <i class="fa fa-download" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Company registration details</h4>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="Iname">
                                        Year of incorporation :</label>
                                    <asp:Label ID="lblYearIncorp" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Iname">
                                        Place of incorporation :</label>
                                    <asp:Label ID="lblPlaceIncor" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Iname">
                                        GSTIN :</label>
                                    <asp:Label ID="lblGSTIN" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnGstinFile" runat="server" />
                                    <asp:HyperLink ID="hplnkGstinFile" runat="server" Target="_blank">
                                                <i class="fa fa-download" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="Country">
                                        PAN :</label>
                                    <asp:HiddenField ID="hdnPanFile" runat="server" />
                                    <asp:HyperLink ID="hplnkPan" runat="server" Target="_blank">
                                                  <i class="fa fa-download" aria-hidden="true"></i>
                                                </asp:HyperLink>
                                </div>
                                <div class="col-sm-4">
                                    <label for="State">
                                        Memorandum & articles of association :</label>
                                    <asp:HiddenField ID="hdnMemoFile" runat="server" />
                                    <asp:HyperLink ID="hplnkMemo" runat="server" Target="_blank">
                                              <i class="fa fa-download" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="Address2">
                                        Certificate of incorporation/registration/partnership deed :</label>
                                    <asp:HiddenField ID="hdnCerti" runat="server" />
                                    <asp:HyperLink ID="hplnkCerti" runat="server" Target="_blank">
                                                 <i class="fa fa-download" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Pin Code">
                                        Project type :</label>
                                    <asp:Label ID="lblProjType" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnProjType" runat="server" />
                                </div>
                                <div class="col-sm-4">
                                    <label for="Pin Code">
                                        Application for :</label>
                                    <asp:Label ID="lblApplicationFor" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnApplicationFor" runat="server" />
                                </div>
                            </div>
                        </div>
                        <%-- Financial Status --%>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Financial status (INR in Lakhs)</h4>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    <table class="table table-bordered">
                                        <tbody>
                                            <tr>
                                                <th>
                                                </th>
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
                                                <td>
                                                    Annual turn over
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
                                                <td>
                                                    Profit after tax
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
                                            
                                            <tr>
                                                <td>
                                                    Reserve and surplus
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
                                            <tr>
                                                <td>
                                                    Share capital
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
                                                <td>
                                                    Net worth
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
                                <div class="col-sm-3">
                                    <label for="AnnualReport">
                                        Audited financial statements for first year(financial statements,profit/loss accounts, balance sheet) :</label>
                                    <asp:HiddenField ID="hdnAudit" runat="server" />
                                    <asp:HyperLink ID="hplnkAudit" runat="server" Target="_blank">
                                                <i class="fa fa-download" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                </div>
                                 <div class="col-sm-3">
                                    <label for="AnnualReport">
                                        Audited financial statements for second year(financial statements,profit/loss accounts, balance sheet) :</label>
                                     <asp:HiddenField ID="hdnFySecond" runat="server" />
                                                            <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                      <asp:HyperLink ID="hplnkFySecond" runat="server" Target="_blank">
                                                <i class="fa fa-download" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                </div>
                                 <div class="col-sm-3">
                                    <label for="AnnualReport">
                                        Audited financial statements for third year(financial statements,profit/loss accounts, balance sheet) :</label>
                                    <asp:HiddenField ID="hdnFyThird" runat="server" />
                                                            <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                           
                                                             <asp:HyperLink ID="hplnkFyThird" runat="server" Target="_blank">
                                                <i class="fa fa-download" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                </div>
                                <div class="col-sm-3" id="dvNetWorth" runat="server">
                                    <label for="NetWorth">
                                        Net Worth Certified by CA :</label>
                                    <asp:HiddenField ID="hdnNetWorth" runat="server" />
                                    <asp:HyperLink ID="hplnkNetWorth" runat="server" Target="_blank">
                                               <i class="fa fa-download" aria-hidden="true"></i>
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </div>
                        <div id="dvExistingUnit" runat="server">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Existing industry details</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <label for="PhoneNo">
                                            Existing industry name :</label>
                                        <asp:Label ID="lblExistInd" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="PhoneNo">
                                            District :</label>
                                        <asp:Label ID="lblDistrictName" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="PhoneNo">
                                            Block :</label>
                                        <asp:Label ID="lblBlock" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="PhoneNo">
                                            Whether land allotted by IDCO :</label>
                                        <asp:Label ID="lblLandAllotIDCO" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <label for="Iname">
                                            Extent of land(in acres) :
                                        </label>
                                        <asp:Label ID="lblExtentLand" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="PhoneNo">
                                            Nature of activity :</label>
                                        <asp:Label ID="lblNatActivity" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="Iname">
                                            Sector :
                                        </label>
                                        <asp:Label ID="lblSector" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <label for="Iname">
                                            Sub Sector :
                                        </label>
                                        <asp:Label ID="lblSubSector" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label for="Iname">
                                            Capacity :
                                        </label>
                                        <asp:Label ID="lblCapacity" runat="server" Text="1"></asp:Label>
                                        <asp:Label ID="lblUnitCapacity" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-6" id="dvOthrs" runat="server">
                                        <label for="PhoneNo">
                                            Others (Please specify) :</label>
                                        <asp:Label ID="lblOtherspecify" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="dvmaterial" runat="server">
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Raw material for production</h4>
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
                                                <asp:BoundField DataField="vchRawMeterialSrc" HeaderText="Material Source" />
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
                <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="headingThree">
                        <h4 class="panel-title">
                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                href="#IndustryInformation" aria-expanded="false" aria-controls="collapseThree">
                                <i class="more-less fa  fa-plus"></i>Project Information </a>
                        </h4>
                    </div>
                    <div id="IndustryInformation" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label for="Time">
                                            Name of the unit :
                                        </label>
                                        <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <%--<label for="Time">
                                            EIN/IEM/IL :
                                        </label>--%>
                                        <asp:Label ID="lblEin" runat="server"></asp:Label>
                                         <asp:HiddenField ID="hdnEin" runat="server" />
                                        <asp:Label ID="lblEINIEM" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label for="Time">
                                            Sector of activity :
                                        </label>
                                        <asp:Label ID="lblSectorActivity" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="Time">
                                            Sub sector :
                                        </label>
                                        <asp:Label ID="lblSubSect" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="Time">
                                            Proposed annual capacity :
                                        </label>
                                        <asp:Label ID="lblPropAnnual" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnAnnualUnit" runat="server" />
                                        <asp:Label ID="lblAnnualUnit" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Proposed fixed capital investment(INR in Lakhs)</h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label for="PM">
                                            Land including land development :
                                        </label>
                                        <asp:Label ID="lblLandincludinglanddevelopment" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="PM">
                                            Building &amp; civil construction :
                                        </label>
                                        <asp:Label ID="lblBuildingCivilConstruction" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="PM">
                                            Plant &amp; machinery :
                                        </label>
                                        <asp:Label ID="lblPlantMachinery" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <%--<div class="col-sm-4">
                                               <label for="Others">
                                                    Preoperative expenses :</label>
                                                <asp:Label ID="Label53" runat="server" Text="85.00000"></asp:Label>
                                            </div>--%>
                                    <div class="col-sm-4">
                                        <label for="Others">
                                            Others :</label>
                                        <asp:Label ID="lblOthersProj" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="CapitalInvestment">
                                            Total capital investment :</label>
                                        <asp:Label ID="lblCapitalInvestment" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="Type">
                                            Pollution category :</label>
                                        <asp:Label ID="lblPolCat" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnPolCat" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label for="Type">
                                            Period to commence commercial production(in months) :</label>
                                        <asp:Label ID="lblPerCommence" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="CapitalInvestment">
                                            Project coming under :</label>
                                        <asp:Label ID="lblProjectComing" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnProjComing" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                       Project implementation Schedule</h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table class="table table-bordered">
                                            <tbody>
                                                <tr>
                                                    <th align="center">
                                                        Activities
                                                    </th>
                                                    <th align="center">
                                                        Months(Zero date starts from acquisition /allotment of land)
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Ground breaking
                                                    </td>
                                                    <td class="text-left">
                                                        <asp:Label ID="lblGround" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Civil and structural completion
                                                    </td>
                                                    <td class="text-left">
                                                        <asp:Label ID="lblCivilstructural" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Major Equipment erection
                                                    </td>
                                                    <td class="text-left">
                                                        <asp:Label ID="lblEquipment" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Start of commercial production
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
                                    <div class="col-sm-6">
                                       <asp:Label ID="lblEIM" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnIndustryEntMemorandum" runat="server" />
                                        <asp:HyperLink ID="hplnkIndustryEntMemorandum" runat="server" Target="_blank">
                                        <i class="fa fa-download" aria-hidden="true"></i>
                                        </asp:HyperLink>
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="Plan">
                                            Manufacturing process flow :</label>
                                        <asp:HiddenField ID="hdnFileMnfprocess" runat="server" />
                                        <asp:HyperLink ID="hplnkMnfprocess" runat="server" Target="_blank">
                                        <i class="fa fa-download" aria-hidden="true"></i>
                                        </asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label for="Plan">
                                            Feasibility report :</label>
                                        <asp:HiddenField ID="hdnFeasibilityReport" runat="server" />
                                        <asp:HyperLink ID="hplnkFeasibilityReport" runat="server" Target="_blank">
                                        <i class="fa fa-download" aria-hidden="true"></i>
                                        </asp:HyperLink>
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="Plan">
                                            Board resolution to take up the project :</label>
                                        <asp:HiddenField ID="hdnBoardResolution" runat="server" />
                                        <asp:HyperLink ID="hplnkBoardResolution" runat="server" Target="_blank">
                                        <i class="fa fa-download" aria-hidden="true"></i>
                                        </asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Means of Finance for Fixed Capital Investment (INR in Lakh)
                                    </h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label for="Capacity Present">
                                            Equity contribution :</label>
                                        <asp:Label ID="lblEquity" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="Existing Business Intrest">
                                            Bank/Institutional finance :</label>
                                        <asp:Label ID="lblBankFin" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="Existing Business Intrest">
                                            Total :</label>
                                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label for="ProdInd">
                                            Foreign direct investment (if any) :</label>
                                        <asp:Label ID="lblFDI" runat="server" Text="NA"></asp:Label>
                                    </div>
                                    <div class="col-sm-8">
                                        <label for="ProdInd">
                                            In case of any other source of finance, please upload relevant document :</label>
                                        <asp:HiddenField ID="hdnOtherFin" runat="server" />
                                        <asp:HyperLink ID="hplnkOtherFin" runat="server" Target="_blank">
                                       <i class="fa fa-download" aria-hidden="true"></i>
                                        </asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        For IRR &amp; DSCR (INR in Lakh)</h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label for="ProdInd">
                                            IRR :</label>
                                        <asp:Label ID="lblIRR" runat="server" Text="NA"></asp:Label>
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="ProdInd">
                                            DSCR :</label>
                                        <asp:Label ID="lblDSCR" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Employment</h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table class="table table-bordered">
                                            <tbody>
                                                <tr>
                                                    <th>
                                                    </th>
                                                    <th align="center">
                                                        Existing
                                                    </th>
                                                    <th align="center">
                                                        Proposed
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Managerial
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblManagerExist" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblManagerProposed" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Supervisory
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblSupervisorExist" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblSupervisorProposed" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Skilled
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblSkilledExist" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblSkilledProposed" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Semi skilled
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblSemiskilledExist" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblSemiskilledProposed" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Un skilled
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblUnskilledExist" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="text-right">
                                                        <asp:Label ID="lblUnskilledProposed" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Total employment
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
                                        <%--<div class="form-body">
                                                    <asp:GridView ID="gvIndustryInfo" class="table table-bordered table-hover" runat="server"
                                                        AutoGenerateColumns="false" Width="100%" EmptyDataText="No Record(s) Found...">
                                                        <Columns>                                                            
                                                            <asp:BoundField DataField="vchYear" HeaderText="" />
                                                            <asp:BoundField DataField="decProfitAfterTax" HeaderText="Profit After Tax(Rs. in lacs)" />
                                                            <asp:BoundField DataField="decDepreciation" HeaderText="Depreciation(Rs. in lacs)" />
                                                            <asp:BoundField DataField="decInterestOnTermLoan" HeaderText="int. on Term Loan(Rs. in lacs)" />
                                                            <asp:BoundField DataField="decTermloanEMI" HeaderText="Term Loan Repayment(EMI)(Rs. in lacs)" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label for="ProdInd">
                                            Proposed direct employment (On Company Payroll) :</label>
                                        <asp:Label ID="lblDirectEmp" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="ProdInd">
                                            Proposed contratual employment :</label>
                                        <asp:Label ID="lblContractualEmp" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Projects at other Locations</h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <label for="ProdInd">
                                            Does the company have projects at other locations :</label>
                                        <asp:Label ID="lblProjectsLocation" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Location Details</h4>
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
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <label for="ProdInd">
                                            Is there any unit outside India :</label>
                                        <asp:Label ID="lblUnitOutSide" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="h4-header">
                                        Location Details (Top Five as per Turnover)</h4>
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
                <%--Land and Utility details--%>
                <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="Div1">
                        <h4 class="panel-title">
                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                href="#ProposedSiteDetails" aria-expanded="false" aria-controls="collapseThree">
                                <i class="more-less fa  fa-plus"></i>Land and Utility Requirement </a>
                        </h4>
                    </div>
                    <div id="ProposedSiteDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Proposed location of land</h4>
                                    </div>
                                </div>
                            </div>
                           
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label for="area">
                                            District :</label>
                                        <asp:Label ID="lblDistrictLand" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="varea">
                                            Block :</label>
                                        <asp:Label ID="lblBlockLand" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="varea">
                                            Extent of Land required (in acre) :</label>
                                        <asp:Label ID="lblExtentLandReq" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div> 
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <label for="Name">
                                            Land Required from Government :</label>
                                        <asp:Label ID="lblLandRequired" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnLandReq" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div id="dvLandReq" runat="server">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <label for="area">
                                                Whether Land is required in IDCO industrial Estate :</label>
                                            <asp:Label ID="lblLandrequiredIDCO" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-4" id="dvIDCOName" runat="server">
                                            <label for="varea">
                                                Name of the IDCO industrial estate :</label>
                                            <asp:Label ID="lblIDCOName" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <label for="varea">
                                                Whether land to be acquired by IDCO :</label>
                                            <asp:Label ID="lbllandacquired" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Power requirement during production</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-3">
                                        Sources Of regular connection</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-1">
                                        <label for="CheckBox2">
                                            GRID</label>
                                    </div>
                                    <div class="col-sm-4" id="divLDGRID">
                                        <label for="Capacity">
                                            Power demand from GRID (in KW) :</label>
                                        <asp:Label ID="lblLoadGrid" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-1">
                                        <label for="CheckBox2">
                                            CPP</label>
                                    </div>
                                    <div class="col-sm-4" id="div6">
                                        <label for="Capacity">
                                            Power drawal from CPP (in KW) :</label>
                                        <asp:Label ID="lblLoadCPP" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label for="ICapacity">
                                            Capacity of the CPP Plant (in KW) :</label>
                                        <asp:Label ID="lblCPPCapacity" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Water requirement</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table class="table table-bordered">
                                            <tbody>
                                                <tr>
                                                    <th>
                                                    </th>
                                                    <th align="center">
                                                        Existing
                                                    </th>
                                                    <th align="center">
                                                        Proposed
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Water requirement(in cusec)
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
                                    <div class="col-sm-4">
                                        <label for="Type3">
                                            Water required for production(in cusec) :</label>
                                        <asp:Label ID="lblWaterReq" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-8">
                                        <label for="Type2">
                                            Sources of water for production :</label>
                                        <asp:Label ID="lblSurface" runat="server"></asp:Label>
                                        <asp:Label ID="lblIdcoSupply" runat="server"></asp:Label>
                                        <asp:Label ID="lblRainWtrHarvesting" runat="server"></asp:Label>
                                        <asp:Label ID="lblother" runat="server"></asp:Label>
                                        <asp:Label ID="lblsourceOther" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h4 class="h4-header">
                                            Waste water management</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label for="area">
                                            Quantum of recycling of waste water :</label>
                                        <asp:Label ID="lblQuantum" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-6">
                                        <label for="varea">
                                            Waste conservation measures :</label>
                                        <asp:HiddenField ID="hdnWaterFile" runat="server" />
                                        <asp:HyperLink ID="hplnkWaterFile" runat="server" Target="_blank">
                                                   <i class="fa fa-download" aria-hidden="true"></i>
                                        </asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label for="garea">
                                            Waste water treatment technology and management of solid/hazardous waste :</label>
                                        <asp:HiddenField ID="hdnHazardousFile" runat="server" />
                                        <asp:HyperLink ID="hplnkHazardousFile" runat="server" Target="_blank">
                                                   <i class="fa fa-download" aria-hidden="true"></i>
                                        </asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading" role="tab" id="Div2">
                        <h4 class="panel-title">
                            <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                href="#QueryDet" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                </i>Query Details </a>
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
                                                                           <i class="fa fa-download" aria-hidden="true"></i>
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
            </div>
            <!-- panel-group -->
             <div class="row">
          <div class="col-sm-12">
        <div class="form-group" >
            <a href="../Service/ServiceViewAndTakeAction.aspx" class="btn btn-success">Back</a>
            <asp:LinkButton ID="LinkButton1" Text="Raise Query" runat="server" class="btn btn-danger "
                data-toggle="modal" data-target="#customer1"></asp:LinkButton>
        </div></div>
    </div>
        </div>
    </div>
    </div> </section>
    <!-- /.content -->
    </div>
    <div class="modal fade" id='customer1' tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header modal-header-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h3>
                        <i class="fa fa-user m-r-5"></i>Raise Query</h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <form class="form-horizontal">
                            <fieldset>
                                <div class="panel panel-bd ">
                                    <div class="panel-heading">
                                        Raise Query
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group" id="divQ1" runat="server">
                                            <label class="col-md-2">
                                                Initial Query</label>
                                            <div class="col-md-4">
                                                <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                <asp:Label ID="lblq1" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                <asp:TextBox ID="txtq1" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    Width="500px" onkeyup="setvalue();" Onkeypress="return inputLimiter(event,'Address')"
                                                    runat="server"></asp:TextBox>
                                                <div id="div1stcnt" runat="server" visible="false">
                                                    <i>Maximum <span id="charsLeft" class="mandatoryspan">1000</span> characters left</i></div>
                                                <asp:HiddenField ID="hdnNoofrecord" runat="server" />
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="divA1" runat="server">
                                            <label class="col-md-2">
                                                Initial Response From The Investor</label>
                                            <div class="col-md-4">
                                                <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                <asp:Label ID="lbla1" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                <%--<asp:TextBox ID="txta1" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>--%>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="divfile1" runat="server">
                                            <label class="col-md-2">
                                                Files</label>
                                            <div class="col-md-4">
                                                <asp:HyperLink ID="hlDoc1" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon1" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" />
                                                </asp:HyperLink>
                                                <asp:HyperLink ID="hlDoc2" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon2" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" /></asp:HyperLink>
                                                <asp:HyperLink ID="hlDoc3" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon3" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" /></asp:HyperLink>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="divQ2" runat="server">
                                            <label class="col-md-2">
                                                Second Set Of Query</label>
                                            <div class="col-md-4">
                                                <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                <asp:Label ID="lblq2" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                <asp:TextBox ID="txtq2" MaxLength="1000" TextMode="MultiLine" Rows="3" Width="500px"
                                                    CssClass="form-control" Onkeypress="return inputLimiter(event,'Address')" onkeyup="setvalue1();"
                                                    runat="server"></asp:TextBox>
                                                <div id="div2ndcnt" runat="server" visible="false">
                                                    <i>Maximum <span id="charsLeft1" class="mandatoryspan">1000</span> characters left</i></div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="divA2" runat="server">
                                            <label class="col-md-2">
                                                Second Set Of Response From The Investor</label>
                                            <div class="col-md-4">
                                                <%--  <textarea name="address" class="form-control" rows="3"></textarea>--%>
                                                <asp:Label ID="lbla2" runat="server" class="bindlabel" Text="" Width="500px"></asp:Label>
                                                <%--<asp:TextBox ID="txta2" MaxLength="1000" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    Onkeypress="return inputLimiter(event,'Address')" runat="server"></asp:TextBox>--%>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group" id="divfile2" runat="server">
                                            <label class="col-md-2">
                                                Files</label>
                                            <div class="col-md-4">
                                                <asp:HyperLink ID="hlDoc4" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon4" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" />
                                                </asp:HyperLink>
                                                <asp:HyperLink ID="hlDoc5" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon5" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" /></asp:HyperLink>
                                                <asp:HyperLink ID="hlDoc6" runat="server" Target="_blank">
                                                    <asp:Image ID="pdficon6" runat="server" ImageUrl="../../SWP_Web/images/pdf.png" Height="25"
                                                        Width="25" Visible="false" /></asp:HyperLink>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClientClick="return  valid();"
                                                OnClick="btnSubmit_Click" class="btn btn-add btn-sm" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger btn-sm"
                                                Style="display: none" data-dismiss="modal" />
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Text input-->
                            </fieldset>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" style="display: none" class="btn btn-danger pull-right" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script>

        $('.panel-title a').click(function () {

            $(this).find('.more-less').toggleClass('fa-minus fa-plus');
        });

   
 
    </script>
</asp:Content>

