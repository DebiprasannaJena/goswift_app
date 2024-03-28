<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProposalDetails.aspx.cs"
    Inherits="ProposalDetails" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<%@ Register Src="../includes/PealMenu.ascx" TagName="PealMenu" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link rel="stylesheet" type="text/css" href="../css/custom.css" />
 

    <script type="text/javascript">
        $(document).ready(function () {
            $("#printbtn").click(function () {
                window.print();
            });
            $('.menuproposal').addClass('active');
           
        })
    </script>



    <style type="text/css">
        .panel-group .panel {
            border-radius: 0;
            box-shadow: none;
            border-color: #EEEEEE;
        }

        .panel-title {
            font-size: 14px;
        }

            .panel-title > a {
                display: block;
                padding: 15px;
                text-decoration: none;
            }

        .more-less {
            float: right;
            color: #212121;
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

        @media print {
            body {
                font-size: 12px;
            }

            .row {
                margin-right: 0px;
                margin-left: 0px;
            }

            .panel-body .h4-header {
                background: #b7b7b7 !important;
                padding: 5px 0px;
                margin: 0px;
            }

            .panel-body {
                padding: 10px;
            }

            .form-body .form-group {
                margin-bottom: 6px;
            }

            .col-sm-4 {
                width: 30%;
            }

            .col-sm-3 {
                width: 25%;
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
                width: 75%;
            }

            .col-sm-12 {
                width: 100%;
            }

            .navbar-header p {
                display: none;
            }

            .form-header {
                padding: 0px -20px 0px;
            }

            .form-sec h2 {
                margin: 5px -15px;
                text-align: center;
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

            .panel-title a {
                padding: 4px 10px !important;
            }

            .colon {
                font-weight: 500;
            }

            .form-body .form-group {
                margin-bottom: 0px;
            }

            span {
                font-size: 12px !important;
            }

            .amountdetails .col-sm-3 {
                width: 50%;
            }

            .amountdetails .col-sm-8 {
                width: 45%;
            }

            .watergroup .col-sm-4 {
                width: 50%;
            }
        }

         
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <uc2:header ID="header" runat="server" />
         <div class="container">   
        <div class="container wrapper">
            <div class="registration-div investors-bg">  
                 <div class="investrs-tab">                      
                     <uc5:PealMenu ID="PealMenu1" runat="server" />
                </div>
                   <div class="iconsdiv tab-icondiv">
                        <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                            <i class="fa fa-print"></i></a>
                   
                       <%-- <a href="javascript:void(0)" title="PDF" id="A1"  class="pull-right printbtn"><i class="fa fa-file-pdf-o"></i></a>--%>
                        <a href="javascript:history.back()" title="Back" id="A2" class="pull-right printbtn">
                            <i class="fa fa-chevron-circle-left"></i></a>
                    </div>
                <div class="form-sec">  
                   
                    <div class="form-header">
                        <h2>Proposal Details</h2>
                    </div>
                    <div class="form-body">
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
                                                <asp:Label ID="Lbl_Proposal_No" runat="server" Font-Bold="true" CssClass="form-control-static"></asp:Label>
                                            </div>
                                            <label for="Iname" class="col-sm-2">
                                                Date of Application
                                            </label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Lbl_Application_Date" runat="server" Font-Bold="true" CssClass="form-control-static"></asp:Label>
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
                                            aria-expanded="true" aria-controls="collapseOne"><i class="more-less fa  fa-minus"></i>Company Information</a>
                                    </h4>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label for="Iname">
                                                        Name of the Company/Enterprise M/s
                                                    </label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="lblCompName" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                <label class="col-sm-2">
                                                    Address
                                                </label>
                                                <div class="col-sm-8">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblAddress" runat="server"> </asp:Label></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Country
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span><label>
                                                        <asp:Label ID="lblCountry" runat="server"></asp:Label></label>
                                                </div>
                                                <label class="col-sm-2">
                                                    State
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblState" runat="server"></asp:Label></label>
                                                </div>
                                                <label class="col-sm-2">
                                                    City
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span><label>
                                                        <asp:Label ID="lblCity" runat="server"></asp:Label></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Phone Number
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span><label><asp:Label ID="lblISDPHNo" runat="server"></asp:Label>
                                                        <asp:Label ID="lblPhoneStateCode" runat="server"></asp:Label>
                                                        <asp:Label ID="lblPhoneNo" runat="server"></asp:Label></label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Fax Number</label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblISDFXNo" runat="server"></asp:Label>
                                                        <asp:Label ID="lblFaxNo" runat="server"></asp:Label>
                                                    </label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Email Address</label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblEmail" runat="server"></asp:Label></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    PIN Code
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblPin" runat="server"></asp:Label></label>
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
                                                <label for="Iname" class="col-sm-2 padding-right-0">
                                                    Name of the Contact Person
                                                </label>
                                                <div class="col-sm-9">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblContactPerson" runat="server"> </asp:Label></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Address
                                                </label>
                                                <div class="col-sm-10">
                                                    <span class="colon">:</span><label>
                                                        <asp:Label ID="lblCorAdd" runat="server"> </asp:Label></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Country</label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblCorCountry" runat="server"></asp:Label></label>
                                                </div>
                                                <label class="col-sm-2">
                                                    State
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblCorState" runat="server"></asp:Label></label>
                                                </div>
                                                <label class="col-sm-2">
                                                    City
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblCorCity" runat="server"></asp:Label></label>
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
                                                    <label>
                                                        <asp:Label ID="lblISDMOB" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCorMobileNo" runat="server"></asp:Label>
                                                    </label>
                                                </div>
                                                <label for="FaxNo" class="col-sm-2">
                                                    Fax Number
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblFaxCordet" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCorFaxNo" runat="server"></asp:Label>
                                                    </label>
                                                </div>
                                                <label for="Email" class="col-sm-2">
                                                    Email Address
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblCorEmail" runat="server"></asp:Label></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Pin Code" class="col-sm-2">
                                                    PIN Code
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblCorrPin" runat="server"></asp:Label>
                                                    </label>
                                                </div>
                                                <label for="MobileNo" class="col-sm-2">
                                                    Constitution of Company/Enterprise
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblOtheConstituition" runat="server"></asp:Label>
                                                    </label>
                                                </div>
                                                <div class="col-sm-6" id="dvConstothers" runat="server">
                                                    <label for="FaxNo">
                                                        Others (Please specify)
                                                    </label>
                                                    <label>
                                                        :<asp:Label ID="lblOthers" runat="server"></asp:Label></label>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
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
                                                    <div class="col-sm-12">
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
                                                        <h4 class="h4-header">Partnership Details</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Partner" class="col-sm-2">
                                                        Number of Partner(s)
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblNumberOfPartner" runat="server"></asp:Label></label>
                                                    </div>
                                                    <label for="Partner" class="col-sm-2">
                                                        Name of the managing Partner</label>
                                                    <div class="col-sm-4">
                                                        <label for="ManagingPartner">
                                                            :</label>
                                                        <asp:Label ID="lblManagPartner" runat="server"></asp:Label>
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
                                                                    <ItemStyle Width="40px" />
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
                                                <label for="Iname" class="col-sm-2">
                                                    Educational qualification
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label id="lblLinkEdu" runat="server">
                                                        <asp:Label ID="lblEduQualif" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnQ" runat="server" />
                                                        <asp:HiddenField ID="hdnEdu" runat="server" />
                                                        <asp:HyperLink ID="hplnkEdu" runat="server" data-toggle="tooltip" title="Download"
                                                            Target="_blank">
                                                            <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                    <label id="lblEduLabel" runat="server">-NA-</label>
                                                </div>
                                                <label for="Iname" class="col-sm-2">
                                                    Technical qualification
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label id="lblLinkTechq" runat="server">
                                                        <asp:Label ID="lblTechQual" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnTechQ" runat="server" />
                                                        <asp:HiddenField ID="hdnTecnical" runat="server" />
                                                        <asp:HyperLink ID="hplnkTechQ" runat="server" data-toggle="tooltip" title="Download"
                                                            Target="_blank">
                                                            <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                    <label id="lblTechqLabel" runat="server">-NA-</label>
                                                </div>
                                                <label for="Iname" class="col-sm-2">
                                                    Experience in years
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label id ="lblLinkExperience" runat="server">
                                                        <asp:Label ID="lblExperience" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnExperience" runat="server" />
                                                        <asp:HyperLink ID="hplnkExperience" runat="server" data-toggle="tooltip" title="Download"
                                                            Target="_blank">
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
                                                        <label>
                                                            <asp:Label ID="lblYearIncorp" runat="server"></asp:Label>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div id="DVC2" runat="server">
                                                    <label class="col-sm-2">
                                                        Place of incorporation
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblPlaceIncor" runat="server"></asp:Label></label>
                                                    </div>
                                                </div>
                                                <label class="col-sm-2">
                                                    GSTIN
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="lblGSTIN" runat="server"></asp:Label>
                                                    <label id="lblGstLink" runat="server">                                                       
                                                        <asp:HiddenField ID="hdnGstinFile" runat="server" />
                                                        <asp:HyperLink ID="hplnkGstin" runat="server" data-toggle="tooltip" title="Download"
                                                            Target="_blank">
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
                                                    <label id="lblPanLink" runat="server">
                                                        <asp:HiddenField ID="hdnPanFile" runat="server" />
                                                        <asp:HyperLink ID="hplnkPan" runat="server" Target="_blank">
                                                        <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                    <label id="lblpanLabel" runat="server">
                                                        -NA-
                                                    </label>

                                                </div>
                                                <div>
                                                    <label for="State" class="col-sm-2">
                                                        Memorandum & Articles of Association
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <div id="DVC3" runat="server">
                                                            <label id="lblMemoLink" runat="server">
                                                                <asp:HiddenField ID="hdnMemoFile" runat="server" />
                                                                <asp:HyperLink ID="hplnkMemo" runat="server" data-toggle="tooltip" title="Download"
                                                                    Target="_blank">
                                                                        <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                                </asp:HyperLink>
                                                            </label>
                                                             
                                                        </div>
                                                        <label id="lblMemoLabel" runat="server">-NA-</label>
                                                    </div>
                                                </div>
                                                <label for="Address2" class="col-sm-2">
                                                    Certificate of incorporation / Registration / Partnership Deed
                                                </label>
                                                <div class="col-sm-2">
                                                    <div>
                                                        <span class="colon">:</span>
                                                        <label id="lblLinkCert" runat="server">
                                                            <asp:HiddenField ID="hdnCerti" runat="server" />
                                                            <asp:HyperLink ID="hplnkCerti" runat="server" Target="_blank">
                                                                <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
                                                        </label>
                                                        <label id="lblCertLabel" runat="server">-NA-</label>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Pin Code" class="col-sm-2">
                                                    Project Type
                                                </label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblProjType" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnProjType" runat="server" />
                                                    </label>
                                                </div>
                                                <label for="Pin Code" class="col-sm-2">
                                                    Application For
                                                </label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblApplicationFor" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnApplicationFor" runat="server" />
                                                    </label>
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
                                        <div class="form-group" runat="server" id="dvx1">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th></th>
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
                                                                <td>Annual Turn Over
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
                                                                <td>Profit After Tax
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
                                                                <td>Reserve and Surplus
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
                                                                <td>Share capital
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
                                                                <td>Net Worth
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
                                                <label for="AnnualReport" class="col-sm-8">
                                                    <asp:Label ID="lblf1" runat="server" Text="Audited Financial Statements for First Year(financial statements,profit/loss accounts,
                                                    balance sheet)"></asp:Label>
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label id="lblLinkAudit" runat="server">
                                                        <asp:HiddenField ID="hdnAudit" runat="server" />
                                                        <asp:HyperLink ID="hplnkAudit" runat="server" data-toggle="tooltip" title="Download"
                                                            Target="_blank">
                                                            <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                    <label id="lblAuditLabel" runat="server">-NA-</label>
                                                </div>
                                                <label for="AnnualReport" class="col-sm-8">
                                                    <asp:Label ID="lblf2" runat="server" Text="Audited Financial Statements for Second Year(financial statements,profit/loss accounts,
                                                    balance sheet)"></asp:Label>
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label id ="lblLinkFySecond" runat="server">
                                                        <asp:HiddenField ID="hdnFySecond" runat="server" />                                                      
                                                        <asp:HyperLink ID="hplnkFySecond" runat="server" data-toggle="tooltip" title="Download"
                                                            Target="_blank">
                                                            <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                    <label id="lblFySecondLabel" runat="server">-NA-</label>
                                                </div>
                                                <label for="AnnualReport" class="col-sm-8">
                                                    <asp:Label ID="lblf3" runat="server" Text="Audited Financial Statements for Third Year(financial statements,profit/loss accounts,
                                                    balance sheet)"></asp:Label>
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label id ="lblLinkFyThird" runat="server">
                                                        <asp:HiddenField ID="hdnFyThird" runat="server" />                                                       
                                                        <asp:HyperLink ID="hplnkFyThird" runat="server" data-toggle="tooltip" title="Download"
                                                            Target="_blank">
                                                            <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                    <label id="lblFyThirdLabel" runat="server">-NA-</label>
                                                </div>
                                                <div id="dvFourthFy" runat="server">
                                                    <label for="NetWorth" class="col-sm-8">
                                                        Income tax return
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <label id="lblLinkFyFourth" runat="server">
                                                            <asp:HiddenField ID="hdnFyFourth" runat="server" />
                                                            <asp:HyperLink ID="hplnkFyFourth" runat="server" data-toggle="tooltip" title="Download"
                                                                Target="_blank">
                                                                <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
                                                        </label>
                                                         <label id="lblFyFourthLabel" runat="server">-NA-</label>
                                                    </div>
                                                </div>
                                                <div id="dvNetWorth" runat="server">
                                                    <label for="NetWorth" class="col-sm-8">
                                                        Net worth certified by CA
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <label id="lblLinkNetWorth" runat="server">
                                                            <asp:HiddenField ID="hdnNetWorth" runat="server" />
                                                            <asp:HyperLink ID="hplnkNetWorth" runat="server" data-toggle="tooltip" title="Download"
                                                                Target="_blank">
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
                                                        <h4 class="h4-header">Existing industry details</h4>
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
                                                    <h4 class="h4-header">Raw material for production</h4>
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
                                            <i class="more-less fa  fa-plus"></i>Project Information </a>
                                    </h4>
                                </div>
                                <div id="IndustryInformation" class="panel-collapse collapse amountdetails" role="tabpanel"
                                    aria-labelledby="headingThree">
                                    <div  id="divIndustryInfo" class="panel-body" runat="server">
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Time" class="col-sm-2">
                                                    Name of the Unit
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblUnit" runat="server"></asp:Label></label>
                                                </div>
                                                <%-- <label for="Time">
                                                        EIN/IEM/IL :
                                                    </label>--%>
                                                <label class="col-sm-2">
                                                    <asp:Label ID="lblEin" runat="server"></asp:Label></label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:HiddenField ID="hdnEin" runat="server" />
                                                        <asp:Label ID="lblEINIEM" runat="server"></asp:Label>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Time" class="col-sm-2">
                                                    Sector of activity
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblSectorActivity" runat="server"></asp:Label></label>
                                                </div>
                                                <label for="Time" class="col-sm-2">
                                                    Sub sector
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblSubSect" runat="server"></asp:Label></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="CapitalInvestment" class="col-sm-2">
                                                    Is the project coming under Priority Sector
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblProjectComing" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnProjComing" runat="server" />
                                                    </label>
                                                </div>
                                                <label for="Time" class="col-sm-2" style="display: none;">
                                                    Proposed Annual Capacity
                                                </label>
                                                <div class="col-sm-4" style="display: none;">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblPropAnnual" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnAnnualUnit" runat="server" />
                                                        <asp:Label ID="lblAnnualUnit" runat="server"></asp:Label></label>
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
                                                <label for="PM" class="col-sm-3">
                                                    Land including land development
                                                </label>
                                                <div class="col-sm-8">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblLandincludinglanddevelopment" runat="server"></asp:Label></label>
                                                </div>
                                                <label class="col-sm-3">
                                                    Building & Civil Construction
                                                </label>
                                                <div class="col-sm-8">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblBuildingCivilConstruction" runat="server"></asp:Label></label>
                                                </div>
                                                <label for="PM" class="col-sm-3">
                                                    Plant & Machinery
                                                </label>
                                                <div class="col-sm-8">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblPlantMachinery" runat="server"></asp:Label>
                                                    </label>
                                                </div>
                                                <%--<div class="col-sm-4">
                                               <label for="Others">
                                                    Preoperative expenses :</label>
                                                <asp:Label ID="Label53" runat="server" Text="85.00000"></asp:Label>
                                            </div>--%>
                                                <div>
                                                    <label for="Others" class="col-sm-3">
                                                        Others
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblOthersProj" runat="server"></asp:Label>
                                                        </label>
                                                    </div>
                                                    <label for="CapitalInvestment" class="col-sm-3">
                                                        Total Capital Investment
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblCapitalInvestment" runat="server"></asp:Label>
                                                    </div>
                                                    <label for="Type" class="col-sm-3">
                                                        Pollution Category
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblPolCat" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hdnPolCat" runat="server" />
                                                        </label>
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
                                                    <label for="Capacity Present" class="col-sm-3">
                                                        Equity Contribution
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblEquity" runat="server"></asp:Label>
                                                        </label>
                                                    </div>
                                                    <label for="Existing Business Intrest" class="col-sm-3">
                                                        Bank/Institutional Finance
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblBankFin" runat="server"></asp:Label>
                                                        </label>
                                                    </div>
                                                    <label for="Existing Business Intrest" class="col-sm-3">
                                                        Total
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblTotal" runat="server"></asp:Label></label>
                                                    </div>
                                                    <label for="ProdInd" class="col-sm-3">
                                                        Foreign Direct Investment (if any)
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblFDI" runat="server" Text="NA"></asp:Label>
                                                        </label>
                                                    </div>
                                                    <label for="ProdInd" class="col-sm-3">
                                                        In case of any other source of finance, please upload relevant document
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <label id="lblLinkOtherFin" runat="server">
                                                            <asp:HiddenField ID="hdnOtherFin" runat="server" />
                                                            <asp:HyperLink ID="hplnkOtherFin" runat="server" Target="_blank">
                                                                <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
                                                        </label>
                                                         <label id="lblOtherFinLabel" runat="server">-NA-</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Type" class="col-sm-3">
                                                        Period to commence commercial production(in months) :</label>
                                                    <div class="col-sm-8">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblPerCommence" runat="server"></asp:Label>
                                                        </label>
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
                                                                    <th align="center">Activities
                                                                    </th>
                                                                    <th align="center">Months(Zero date starts from acquisition /allotment of land)
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
                                                        <asp:Label ID="lblEIM" runat="server"></asp:Label>
                                                        <label id="lblLinkEIM" runat="server">
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
                                                        <label id="lblLinkFeasibilityReport" runat="server">
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
                                                        <label id="lblLinkBoardResolution" runat="server">
                                                            <asp:HiddenField ID="hdnBoardResolution" runat="server" />
                                                            <asp:HyperLink ID="hplnkBoardResolution" runat="server" Target="_blank">
                                                                <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                            </asp:HyperLink>
                                                        </label>
                                                         <label id="lblBoardResolutionLabel" runat="server">-NA-</label>
                                                    </div>
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
                                                    <h4 class="h4-header">Employment Potential</h4>
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
                                                    <h4 class="h4-header">Projects at Other Locations</h4>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="ProdInd" class="col-sm-4">
                                                        Does the company have projects at other locations in India?</label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblProjectsLocation" runat="server"></asp:Label></label>
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
                                                        Is there any unit outside India </label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblUnitOutSide" runat="server"></asp:Label></label>
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
                                
                                     <div id="divIdIndustryNoRecord" class="panel-body" runat="server">
                                          
                                            <span> No Record(s) Found... </span>
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
                                <div id="ProposedSiteDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree" >
                                    <div class="panel-body" id ="divProposedLand" runat="server">
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
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span><label>
                                                        <asp:Label ID="lblLandRequired" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hdnLandReq" runat="server" />
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="area" class="col-sm-2">
                                                    District
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span><label>
                                                        <asp:Label ID="lblDistrictLand" runat="server"></asp:Label></label>
                                                </div>
                                                <label for="varea" class="col-sm-2">
                                                    Block
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:Label ID="lblBlockLand" runat="server"></asp:Label></label>
                                                </div>
                                                <label for="varea" class="col-sm-2">
                                                    Extent of land required (In acre)
                                                <%--<asp:Label ID="LblLandUnit" runat="server" Font-Bold="true"></asp:Label>--%>
                                                </label>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span><label>
                                                        <asp:Label ID="lblExtentLandReq" runat="server"></asp:Label></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="dvLandReq" runat="server">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="area" class="col-sm-4">
                                                        Whether land is required in IDCO industrial estate</label>
                                                    <div class="col-sm-2">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblLandrequiredIDCO" runat="server"></asp:Label></label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvIDCOName" runat="server">
                                                <div class="row">
                                                    <label for="varea" class="col-sm-4">
                                                        Name of the IDCO industrial estate</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lblIDCOName" runat="server"></asp:Label></label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvLandAcquired" runat="server">
                                                <div class="row">
                                                    <label for="varea" class="col-sm-4">
                                                        Whether land to be acquired by IDCO</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <label>
                                                            <asp:Label ID="lbllandacquired" runat="server"></asp:Label></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" id="dvL1" runat="server">
                                            <div class="row">
                                                <label for="varea" class="col-sm-4">
                                                    Project Land Use Statement
                                                </label>
                                                <div class="col-sm-7">
                                                    <span class="colon">:</span>
                                                    <label id="lblLinkProjectlandStatement" runat="server">
                                                        <asp:HiddenField ID="hdnLandUsestmt" runat="server" />
                                                        <asp:HyperLink ID="hypProjectlandStatement" runat="server" Target="_blank">
                                                        <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                    <label id="lblProjectlandStatementLablel" runat="server">-NA-</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" id="dvL2" runat="server">
                                            <div class="row">
                                                <label class="col-sm-4">
                                                    Project Layout Plan
                                                </label>
                                                <div class="col-sm-7">
                                                    <span class="colon">:</span>
                                                    <label id="lblLinkProjectLaoutPlan" runat="server">
                                                        <asp:HiddenField ID="hdnLayOutPln" runat="server" />
                                                        <asp:HyperLink ID="hypProjectLaoutPlan" runat="server" Target="_blank">
                                                            <i class="fa fa-cloud-download" aria-hidden="true"></i>
                                                        </asp:HyperLink>
                                                    </label>
                                                    <lable id="lblProjectLaoutPlanLabel" runat="server">-NA-</lable>

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
                                                <div class="col-sm-1">
                                                    <label for="CheckBox2">
                                                        GRID</label>
                                                </div>
                                                <div class="col-sm-3" id="divLDGRID">
                                                    <label for="Capacity">
                                                        Power demand from GRID (in KW)
                                                    </label>

                                                </div>
                                                <div class="col-sm-2">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="lblLoadGrid" runat="server" CssClass="form-control-static"></asp:Label>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-1">
                                                    <label for="CheckBox2">
                                                        CPP</label>
                                                </div>
                                                <div class="col-sm-3" id="div6">
                                                    <label for="Capacity">
                                                        Power drawal from CPP (in KW)
                                                    </label>
                                                </div>
                                                <div class="col-sm-1">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="lblLoadCPP" runat="server" CssClass="form-control-static"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="ICapacity">
                                                        Capacity of the CPP Plant (in KW)</label>
                                                </div>
                                                <div class="col-sm-1">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="lblCPPCapacity" runat="server" CssClass="form-control-static"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-1">
                                                    <label for="CheckBox2">
                                                        IPP</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="Capacity">
                                                        Independent Power Producer (in KW)
                                                    </label>
                                                </div>
                                                <div class="col-sm-1">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="LblPowerProducerIpp" runat="server" CssClass="form-control-static"></asp:Label>
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
                                                                <td>Total water requirement (in cusec)
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
                                        <div class="form-group watergroup">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label for="Type3">
                                                        Water required for production (in cusec)</label>
                                                </div>
                                                <div class="col-sm-1">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="lblWaterReq" runat="server" CssClass="form-control-static"></asp:Label>
                                                </div>

                                                <div class="col-sm-3">
                                                    <label for="Type2">
                                                        Sources of water for Production </label>
                                                   
                                                </div>
                                                 <div class="col-sm-5">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblSurface" runat="server" CssClass="form-control-static"></asp:Label>
                                                        <%--<asp:Label ID="lblIdcoSupply" runat="server"></asp:Label>
                                                    <asp:Label ID="lblRainWtrHarvesting" runat="server"></asp:Label>
                                                    <asp:Label ID="lblother" runat="server"></asp:Label>
                                                    <asp:Label ID="lblsourceOther" runat="server"></asp:Label>--%>
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
                                                    <label id="lblLinkWaterFile" runat="server">
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
                                                    <label id="lblLinkHazardousFile" runat="server">
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
                                    <div id="divProposedSiteDetailsNoRecoord" class="panel-body" runat="server">
                                          
                                            <span> No Record(s) Found... </span>
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
                            <%--Land Recommendation  Details--%>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="Div3">
                                    <h4 class="panel-title">
                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                            href="#DivLandRecom" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus"></i>Land Recommendation  Details</a>
                                    </h4>
                                </div>
                                <div id="DivLandRecom" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                    <div runat="server" id="DivLand">
                                        <div id="divLanRecomBody" class="panel-body" runat="server">
                                           <%-- <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">--%>
                                                        <div class="form-body">
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Name" class="col-sm-4">
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
                                                                    <label for="Name" class="col-sm-4">
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
                                                    <%--</div>
                                                </div>
                                            </div>--%>
                                        </div>
                                    
                                        <div id="divLanRecomNoValue" class="panel-body" runat="server">
                                          
                                            <span> No Record(s) Found... </span>
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
                            <div class="panel panel-default">
                                <asp:Repeater ID="rptCustomers" runat="server" OnItemDataBound="rptCustomers_ItemDataBound">
                                    <HeaderTemplate>
                                        <table style="width: 100%;">
                                            <%--<tr>
                                                <td colspan="2">
                                                </td>
                                            </tr>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading" role="tab" id="Div3">
                                                                    <h4 class="panel-title">
                                                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                                            href='<%# "#"+Eval("INT_SERVICEID")%>' aria-expanded="false" aria-controls="collapseThree">
                                                                            <i class="more-less fa  fa-plus"></i>
                                                                            <%#Eval("VCH_SERVICENAME")%></a>
                                                                    </h4>
                                                                </div>
                                                                <div id='<%#Eval("INT_SERVICEID") %>' class="panel-collapse collapse" role="tabpanel"
                                                                    aria-labelledby="headingThree">
                                                                    <div class="panel-body">
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <div class="col-sm-5">
                                                                                    <div class="form-group">
                                                                                        <div class="row">
                                                                                            <label for="area" class="col-sm-5">
                                                                                                Application No.
                                                                                            </label>
                                                                                            <div class="col-sm-7">
                                                                                                <span class="colon">:</span><label>
                                                                                                    <asp:Label ID="lblApplicationKey" Text='<%#Eval("VCH_APPLICATION_UNQ_KEY") %>' runat="server"></asp:Label>
                                                                                                    <asp:HiddenField ID="lblDeptRemarks" Value='<%#Eval("INT_SERVICEID") %>' runat="server" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row" id="DvServiceContent" runat="server">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="1">&nbsp;
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <br />
                            
                        </div>
                       
                        <!-- panel-group -->
                    </div>
                </div>
            </div>
        </div>
             </div>
       
        <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
