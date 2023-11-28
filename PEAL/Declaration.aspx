<%--'*******************************************************************************************************************
' File Name         : Declaration.aspx
' Description       : Details of Promoter
' Created by        : Radhika Rani Patri
' Created On        : 03 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Declaration.aspx.cs" Inherits="Declaration" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        @media print
        {
            .noprint
            {
                display: none;
            }
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div">
            <div class="investrs-tab">
                <div class="iconsdiv tab-icondiv">
                    <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                        <i class="fa fa-print"></i></a><a href="javascript:history.back()" title="Back" id="A2"
                            class="pull-right printbtn"><i class="fa fa-chevron-circle-left"></i></a>
                   <%-- <asp:HyperLink title="Save as Pdf" data-toggle="tooltip" class="pull-right printbtn btn-primary"
                        ID="hplPdf" runat="server"><i class="fa fa-file-pdf-o"></i></asp:HyperLink>--%>
                    <a href="javascript:void(0);" title="Pdf" id="pdfbtn" class="pull-right printbtn btn-primary">
                        <i class="fa fa-file-pdf-o"></i></a>
                </div>
                <uc4:pealmenu ID="pealmenu" runat="server" />
            </div>
            <div class="pealdetails">
                <div class="wizard wizard2">
                    <div class="wizard-inner">
                        <div class="connecting-line">
                        </div>
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="backactive"><a href="PromoterDetails.aspx" aria-controls="Company Information"
                                title="Company Information"><span class="round-tab"><i class="glyphicon glyphicon-home">
                                </i></span><small><i class="fa fa-check text-success backcheck" aria-hidden="true"></i>
                                    &nbsp;<b>1.</b> Company Information</small> </a></li>
                            <li class="backactive"><a href="proposeddetails.aspx" aria-controls="Project Information"
                                title="Project Information"><span class="round-tab"><i class="glyphicon glyphicon-pencil">
                                </i></span><small><i class="fa fa-check text-success backcheck" aria-hidden="true"></i>
                                    &nbsp;<b>2.</b> Project Information</small> </a></li>
                            <li role="presentation" class="backactive"><a href="landdetails.aspx" aria-controls="Land and Utility Requirment"
                                title=" Land and Utility Requirment"><span class="round-tab"><i class="glyphicon glyphicon-picture">
                                </i></span><small><i class="fa fa-check text-success backcheck" aria-hidden="true"></i>
                                    &nbsp;<b>3.</b> Land and Utility Requirment</small> </a></li>
                            <li role="presentation" class="active"><a href="Declaration.aspx" data-toggle="tab"
                                aria-controls="Declaration" role="tab" title="Declaration"><span class="round-tab"><i
                                    class="glyphicon glyphicon-ok"></i></span><small><i class="fa fa-check text-success backcheck4"
                                        aria-hidden="true"></i>&nbsp;<b>4.</b> Declaration</a></small> </a></li>
                        </ul>
                    </div>
                    <div class="form-body" id="dvGeneratePDF"  runat="server">
                        <h4 style="margin-top: 5px; color: #000; text-transform: uppercase" class="text-center">
                            Application Preview</h4>
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingOne" >
                                    <h4 class="panel-title">
                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                            aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa  fa-plus">
                                            </i>Company Information</a>
                                    </h4>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <label for="Iname">
                                                        Name of the Company/Enterprise M/s :</label>
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
                                                        Phone Number :</label>
                                                    <asp:Label ID="lblISDPHNo" runat="server"></asp:Label>
                                                    <asp:Label ID="lblPhoneStateCode" runat="server"></asp:Label>
                                                    <asp:Label ID="lblPhoneNo" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="FaxNo">
                                                        Fax Number :</label>
                                                    <asp:Label ID="lblISDFXNo" runat="server"></asp:Label>
                                                    <asp:Label ID="lblFaxNo" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="Email">
                                                        Email Address :</label>
                                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="Pin Code">
                                                        PIN Code :</label>
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
                                                        Name of the Contact Person :</label>
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
                                                        Mobile Number :</label>
                                                    <asp:Label ID="lblISDMOB" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCorMobileNo" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="FaxNo">
                                                        Fax Number :</label>
                                                    <asp:Label ID="lblFaxCordet" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCorFaxNo" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="Email">
                                                        Email Address :</label>
                                                    <asp:Label ID="lblCorEmail" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="Pin Code">
                                                        PIN Code :</label>
                                                    <asp:Label ID="lblCorrPin" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label for="MobileNo">
                                                        Constitution of Company/Enterprise :</label>
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
                                                            Promoter Details</h4>
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
                                                            Partnership Details</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <label for="Partner">
                                                            Number of Partner(s) :</label>
                                                        <asp:Label ID="lblNumberOfPartner" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label for="ManagingPartner">
                                                            Name of the managing Partner :</label>
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
                                                            Board of Directors</h4>
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
                                                    <asp:HyperLink ID="hplnkEdu" runat="server" Target="_blank">
                                                   <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Iname">
                                                        Technical qualification :</label>
                                                    <asp:Label ID="lblTechQual" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnTechQ" runat="server" />
                                                    <asp:HiddenField ID="hdnTecnical" runat="server" />
                                                    <asp:HyperLink ID="hplnkTechQ" runat="server" Target="_blank">
                                                    <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Iname">
                                                        Experience in years :</label>
                                                    <asp:Label ID="lblExperience" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnExperience" runat="server" />
                                                    <asp:HyperLink ID="hplnkExperience" runat="server" Target="_blank">
                                                   <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">
                                                        Entrepreneur Registration Details</h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4" id="DVC1" runat="server">
                                                    <label for="Iname">
                                                        Year of Establishment :</label>
                                                    <asp:Label ID="lblYearIncorp" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4" id="DVC2" runat="server">
                                                    <label for="Iname">
                                                        Place of incorporation :</label>
                                                    <asp:Label ID="lblPlaceIncor" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Iname">
                                                        GSTIN :</label>
                                                    <asp:Label ID="lblGSTIN" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnGstinFile" runat="server" />
                                                    <asp:HyperLink ID="hplnkGstin" runat="server" Target="_blank">
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
                                                <div class="col-sm-4" id="DVC3" runat="server">
                                                    <label for="State">
                                                        Memorandum & Articles of Association :</label>
                                                    <asp:HiddenField ID="hdnMemoFile" runat="server" />
                                                    <asp:HyperLink ID="hplnkMemo" runat="server" Target="_blank">
                                              <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4" id="DVC4" runat="server">
                                                    <label for="Address2">
                                                        Certificate of incorporation /Registration /Partnership Deed :</label>
                                                    <asp:HiddenField ID="hdnCerti" runat="server" />
                                                    <asp:HyperLink ID="hplnkCerti" runat="server" Target="_blank">
                                                 <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Pin Code">
                                                        Project Type :</label>
                                                    <asp:Label ID="lblProjType" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnProjType" runat="server" />
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Pin Code">
                                                        Application For :</label>
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
                                                        Financial Status (INR in Lakhs)</h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="dvx1">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th>
                                                                </th>
                                                                <th align="center" runat="server" id="dvx2">
                                                                    <asp:Label ID="lblFinYear1" runat="server" Text="Financial Year 1"></asp:Label>
                                                                    <asp:HiddenField ID="hdnFinYear1" runat="server" />
                                                                </th>
                                                                <th align="center" runat="server" id="dvx3">
                                                                    <asp:Label ID="lblFinYear2" runat="server" Text="Financial Year 2"></asp:Label>
                                                                    <asp:HiddenField ID="hdnFinYear2" runat="server" />
                                                                </th>
                                                                <th align="center" runat="server" id="dvx4">
                                                                    <asp:Label ID="lblFinYear3" runat="server" Text="Financial Year 3"></asp:Label>
                                                                    <asp:HiddenField ID="hdnFinYear3" runat="server" />
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Annual Turn Over
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx21">
                                                                    <asp:Label ID="lblAnnlCrntYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx31">
                                                                    <asp:Label ID="lblAnnlLastYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx41">
                                                                    <asp:Label ID="lblAnnlPrevToLastYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Profit After Tax
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx22">
                                                                    <asp:Label ID="lblPftBTCrntYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx32">
                                                                    <asp:Label ID="lblPftBTLastYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx42">
                                                                    <asp:Label ID="lblPftBTPrevToLastYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" id="dvReserve">
                                                                <td>
                                                                    Reserve and Surplus
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx23">
                                                                    <asp:Label ID="lblRSCrntyr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx33">
                                                                    <asp:Label ID="lblRSLastYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx43">
                                                                    <asp:Label ID="lblRSPrevTolastYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" id="dvShareCapital">
                                                                <td>
                                                                    Share capital
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx24">
                                                                    <asp:Label ID="lblSCCrntYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx34">
                                                                    <asp:Label ID="lblSCLastYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx44">
                                                                    <asp:Label ID="lblSCPrevToLastYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Net Worth
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx25">
                                                                    <asp:Label ID="lblNWCrntYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx35">
                                                                    <asp:Label ID="lblNWLastYr" runat="server" Text="Annual"></asp:Label>
                                                                </td>
                                                                <td class="text-right" runat="server" id="dvx45">
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
                                                <div class="col-sm-12">
                                                    <label for="AnnualReport">
                                                        <asp:Label ID="lblf1" runat="server" Text="Audited Financial Statements for First Year(financial statements,profit/loss accounts,
                                                    balance sheet)"></asp:Label>
                                                        :</label>
                                                    <asp:HiddenField ID="hdnAudit" runat="server" />
                                                    <asp:HyperLink ID="hplnkAudit" runat="server" Target="_blank">
                                                <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                                <div class="col-sm-12">
                                                    <label for="AnnualReport">
                                                        <asp:Label ID="lblf2" runat="server" Text="Audited Financial Statements for Second Year(financial statements,profit/loss accounts,
                                                    balance sheet)"></asp:Label>:</label>
                                                    <asp:HiddenField ID="hdnFySecond" runat="server" />
                                                    <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                    <asp:HyperLink ID="hplnkFySecond" runat="server" Target="_blank">
                                                <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                                <div class="col-sm-12">
                                                    <label for="AnnualReport">
                                                        <asp:Label ID="lblf3" runat="server" Text="Audited Financial Statements for Third Year(financial statements,profit/loss accounts,
                                                    balance sheet)"></asp:Label>
                                                        :</label>
                                                    <asp:HiddenField ID="hdnFyThird" runat="server" />
                                                    <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                    <asp:HyperLink ID="hplnkFyThird" runat="server" Target="_blank">
                                                <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                                <div class="col-sm-12" id="dvFourthFy" runat="server">
                                                    <label for="NetWorth">
                                                        Income tax return
                                                    </label>
                                                    <asp:HiddenField ID="hdnFyFourth" runat="server" />
                                                    <asp:HyperLink ID="hplnkFyFourth" runat="server" data-toggle="tooltip" title="Download"
                                                        Target="_blank">
                                               <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                                <div class="col-sm-12" id="dvNetWorth" runat="server">
                                                    <label for="NetWorth">
                                                        Net worth certified by CA :</label>
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
                                                    <div class="col-sm-12">
                                                        <h4 class="h4-header">
                                                            Existing industry details</h4>
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
                                                        Raw Material for Production</h4>
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
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingThree" >
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
                                                        Name of the Unit :
                                                    </label>
                                                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:Label ID="lblEin" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnEin" runat="server" />
                                                    <asp:Label ID="lblEINIEM" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="CapitalInvestment">
                                                        Is the project coming under Priority Sector :</label>
                                                    <asp:Label ID="lblProjectComing" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnProjComing" runat="server" />
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
                                                <div class="col-sm-4" style="display: none;">
                                                    <label for="Time">
                                                        Proposed Annual Capacity :
                                                    </label>
                                                    <asp:Label ID="lblPropAnnual" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnAnnualUnit" runat="server" />
                                                    <asp:Label ID="lblAnnualUnit" runat="server"></asp:Label>
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
                                                            <asp:BoundField DataField="vchProductName" HeaderText="Product Name" />
                                                            <asp:BoundField DataField="vchProposedAnnualCapacity" HeaderText="Proposed Annual Capacity" />
                                                            <asp:BoundField DataField="vchProposedUnit" HeaderText="Unit" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">
                                                    Proposed Capital Investment (INR in Lakhs)</h4>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="PM">
                                                        Land Including Land Development :
                                                    </label>
                                                    <asp:Label ID="lblLandincludinglanddevelopment" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="PM">
                                                        Building & Civil Construction :
                                                    </label>
                                                    <asp:Label ID="lblBuildingCivilConstruction" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="PM">
                                                        Plant & Machinery :
                                                    </label>
                                                    <asp:Label ID="lblPlantMachinery" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="Others">
                                                        Others :</label>
                                                    <asp:Label ID="lblOthersProj" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="CapitalInvestment">
                                                        Total Capital Investment :</label>
                                                    <asp:Label ID="lblCapitalInvestment" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Type">
                                                        Pollution Category :</label>
                                                    <asp:Label ID="lblPolCat" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnPolCat" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label for="Type">
                                                        Period to commence commercial production(in months) :</label>
                                                    <asp:Label ID="lblPerCommence" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">
                                                    Project Implementation Schedule</h4>
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
                                                                    Ground Breaking
                                                                </td>
                                                                <td class="text-left">
                                                                    <asp:Label ID="lblGround" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Civil and Structural Completion
                                                                </td>
                                                                <td class="text-left">
                                                                    <asp:Label ID="lblCivilstructural" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Major Equipment Erection
                                                                </td>
                                                                <td class="text-left">
                                                                    <asp:Label ID="lblEquipment" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Start of Commercial Production
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
                                                    <asp:Label ID="lblEIM" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnIndustryEntMemorandum" runat="server" />
                                                    <asp:HyperLink ID="hplnkIndustryEntMemorandum" runat="server" Target="_blank">
                                        <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                                <div class="col-sm-3" style="display: none">
                                                    <label for="Plan">
                                                        Manufacturing Process Flow :</label>
                                                    <asp:HiddenField ID="hdnFileMnfprocess" runat="server" />
                                                    <asp:HyperLink ID="hplnkFileMnfprocess" runat="server" Target="_blank">
                                        <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="Plan">
                                                        Feasibility Report :</label>
                                                    <asp:HiddenField ID="hdnFeasibilityReport" runat="server" />
                                                    <asp:HyperLink ID="hplnkFeasibilityReport" runat="server" Target="_blank">
                                        <i class="fa fa-download" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </div>
                                                <div class="col-sm-4">
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
                                                    Means of Finance for Capital Investment (INR in Lakh)
                                                </h4>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="Capacity Present">
                                                        Equity Contribution :</label>
                                                    <asp:Label ID="lblEquity" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Existing Business Intrest">
                                                        Bank/Institutional Finance :</label>
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
                                                        Foreign Direct Investment (if any) :</label>
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
                                        <div id="dvIRR" runat="server">
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
                                        </div>
                                        <div class="form-group" id="divGroupOfCompany" runat="server">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">
                                                        Group of Company Details</h4>
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
                                                            <asp:TemplateField HeaderText="Net worth of last financial year">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_GC_Net_Worth_G" runat="server" Text='<%# Eval("decNetWorth") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="25%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Document related to net worth">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="Hyp_View_GC_Doc" runat="server" Target="_blank" ToolTip="Click here to view document."><i class="fa fa-cloud-download"></i></asp:HyperLink>
                                                                    <asp:HiddenField ID="Hid_GC_Net_Worth_File_Name_G" runat="server" Value='<%# Eval("vchNetWorthDoc") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="25%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
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
                                                        Proposed contractual employment :</label>
                                                    <asp:Label ID="lblContractualEmp" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">
                                                    Projects at Other Locations</h4>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <label for="ProdInd">
                                                        Does the company have projects at other locations in India? :</label>
                                                    <asp:Label ID="lblProjectsLocation" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="dvLocDetIndia" runat="server">
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
                                        <div id="dvLocDetOutInd" runat="server">
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
                            </div>
                            <%--Land and Utility details--%>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="Div1" >
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
                                                        Proposed Location of Land</h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <label for="Name">
                                                        Land required from government :</label>
                                                    <asp:Label ID="lblLandRequired" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnLandReq" runat="server" />
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
                                        <div id="dvLandReq" runat="server">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <label for="area">
                                                            Whether land is required in IDCO industrial estate :</label>
                                                        <asp:Label ID="lblLandrequiredIDCO" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-4" id="dvIDCOName" runat="server">
                                                        <label for="varea">
                                                            Name of the IDCO industrial estate :</label>
                                                        <asp:Label ID="lblIDCOName" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-4" id="dvLandAcquired" runat="server">
                                                        <label for="varea">
                                                            Whether land to be acquired by IDCO :</label>
                                                        <asp:Label ID="lbllandacquired" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" id="dvL1"  runat="server">
                                            <div class="row">
                                                <label for="varea" class="col-sm-5">
                                                    Project Land Use Statement
                                                </label>
                                                <div class="col-sm-7">
                                                    <span class="colon">:</span><label>
                                                        <asp:HiddenField ID="hdnLandUsestmt" runat="server" />
                                                        <asp:HyperLink ID="hypProjectlandStatement" runat="server" Target="_blank">
                                                   <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" id="dvL2" runat="server">
                                            <div class="row">
                                                <label class="col-sm-5">
                                                    Project Layout Plan
                                                </label>
                                                <div class="col-sm-7">
                                                    <span class="colon">:</span><label>
                                                        <asp:HiddenField ID="hdnLayOutPln" runat="server" />
                                                        <asp:HyperLink ID="hypProjectLaoutPlan" runat="server" Target="_blank">
                                                   <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">
                                                        Power Requirement During Production</h4>
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
                                                <div class="col-sm-1">
                                                    <label for="CheckBox2">
                                                        IPP</label>
                                                </div>
                                                <div class="col-sm-4" id="divLDGRID1">
                                                    <label for="Capacity">
                                                        Independent Power Producer (in KW) :</label>
                                                    <asp:Label ID="LblPowerDemandIPP" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">
                                                        Water Requirement</h4>
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
                                                                    Total Water Requirement (in cusec)
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
                                                        Water required for production (in cusec) :</label>
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
                                                        Waste Water Management</h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label for="area">
                                                        Quantum of recycling of waste water (in cusec) :</label>
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
                        </div>
                        <!-- panel-group -->
                    </div>
                    <div class="form-sec" style="margin-top: 1%;" >
                        <div class="form-header">
                            <span class="mandatoryspan pull-right">( * ) Indicate Mandatory Fields</span>
                            <h2 class="m-t-0 m-b-0">
                                Declaration</h2>
                        </div>
                        <div class="form-body" tabinde="4">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="checkbox" >
                                            <label style="">
                                                <asp:CheckBox ID="check" TabIndex="1" runat="server" />
                                                <span class="cr"><i class="cr-icon fa fa-check"></i></span><b>I hereby declare that
                                                    the particulars and the statements made in this application are true and correct
                                                    to the best of my knowledge and belief and nothing has been concealed or withheld
                                                    therefrom.</b> <span class="text-red">*</span></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <div>
                            <span class="mandatoryspan pull-left">Note :- Application can not be modified once payment
                                is done.</span>
                        </div>
                        <div class="form-footer">
                            <div class="row">
                                <div class="col-sm-12" align="center">
                                    <asp:Button ID="btnBack" TabIndex="2" runat="server" Text="Back" CssClass=" btn btn-warning noprint"
                                        OnClick="btnBack_Click" />
                                    <asp:Button ID="btnSubmit" TabIndex="3" runat="server" Text="Pay Now" class=" btn btn-success noprint"
                                        OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnLater" TabIndex="4" runat="server" Text="Submit & Pay Later" class=" btn btn-success noprint"
                                        OnClick="btnLater_Click" Visible="false" />
                                    <%-- <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#printbtn').click(function () {
                window.print();
            })
            $('#pdfbtn').click(function () {
                window.print();
            })
            $('.backcheck4').hide();
            var Qrvalue = '<%= Request.QueryString["StrPropNo"] %>';
            var sessioncal = '<%=Session["proposalno"] %>';
            //alert(sessioncal);
            if (sessioncal != null) {
                $('.wizard2 .nav-tabs > li').addClass("backactive");
                $('.backcheck').show();

            }
            else if ((Qrvalue != null) && (Qrvalue != "")) {
                $('.wizard2 .nav-tabs > li').addClass("backactive");
                $('.backcheck').show();
            }
            else {
                $('.wizard2 .nav-tabs > li').removeClass("backactive");
                $('.backcheck').hide();

            }
            $('.menuproposal').addClass('active');

            $('#btnSubmit').attr("disabled", "disabled");
            $('#btnSubmit')

            if ($("#check").prop('checked') == true) {
                $('#btnSubmit').removeAttr('disabled');
            }
            else {
                $('#btnSubmit').attr("disabled", "disabled");
            }
            $('#check').click(function () {
                if ($(this).prop('checked') == true) {
                    $('#btnSubmit').removeAttr('disabled');
                }
                else {
                    $('#btnSubmit').attr("disabled", "disabled");
                }
            }); $('#btnSubmit').click(function () {

                $('.backcheck4').show();
            })
        })
      

    </script>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
