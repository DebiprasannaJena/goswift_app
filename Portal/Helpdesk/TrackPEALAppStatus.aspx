<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="TrackPEALAppStatus.aspx.cs" Inherits="Portal_Helpdesk_TrackPEALAppStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    ClientIDMode="Static">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../js/WebValidation.js" type="text/javascript"></script>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Track PEAL Application Status</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>HelpDesk</a></li>
                    <li><a>Track PEAL App Status</a></li>
                </ul>
            </div>
        </div>
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-bd lobidisable">
                                <div class="panel-body">
                                    <div class="search-sec">
                                        <div class="form-group row">
                                            <label class="col-sm-2">
                                                Proposal No.
                                            </label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="txtProposalID" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" CssClass="btn btn btn-add -sm"
                                                    runat="server" Text="Search" OnClientClick="return ValidateApplicationKey();"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="DivDetails" runat="server">
                                        <div class="form-group ">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Proposal Details
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="DivNoRecord" runat="server" class="col-sm-12">
                                            <asp:Label ID="Lbl_Norecord" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div id="Divapplicationdetails" runat="server">

                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Proposal Number</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>

                                                    <label>
                                                        <asp:HyperLink ID="Hypr_ProposalNo" runat="server" Font-Bold="true" CssClass="datalabel" Target="_blank" ToolTip="Click here to view the proposal details "></asp:HyperLink></label>

                                                </div>
                                                <label class="col-sm-2">
                                                    Company Name (Regd.)</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Company_Name" runat="server" CssClass="datalabel" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Constitution of Company</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Constitution_Name" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Project Type</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Project_Type" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Application For</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Application_for" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Current Status</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Current_Status" runat="server" CssClass="datalabel" ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Action Taken By</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Action_Taken_By" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Action Tobe  Taken By</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Action_Tobe_Taken_By" runat="server" CssClass="datalabel" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>


                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Payment Status</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Payment_Status" runat="server" CssClass="datalabel" Font-Bold="true"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Payment Amount</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Payment_Amount" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>


                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Industry Name (PEAL)
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Apply_By" runat="server" CssClass="datalabel" Font-Bold="true"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Payment Date
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Payment_Date" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>


                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    End Of ORTPSA Timeline
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Ortps_Timeline" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    End of Raise Query Date</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Querey_Date" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Land Required from Government
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_LandRequFromGovt" runat="server" CssClass="datalabel" ForeColor="Blue"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    End Of Revert Query Date</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Revert_Query_Date" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Land Allotment Details
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div id="DivNolandrecord" runat="server" class="col-sm-12" visible="false">
                                            <asp:Label ID="Lbl_NolandRecod" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Red" Text="No record found."></asp:Label>
                                        </div>
                                        <div runat="server" id="divLandDetails" class="col-sm-12" visible="false">
                                        </div>--%>

                                        <div class="col-sm-12">
                                            <div class="form-group ">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <p class="text-primary">Application Landing Status (Forward To IDCO)</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdLandDetails" class="table table-bordered table-hover" runat="server"
                                                    AutoGenerateColumns="false" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Proposal Number" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_ProposalNo" runat="server" Text='<%# Eval("vchProposalNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Created  Date" ItemStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_PaymentDate" runat="server" Text='<%# Eval("dtmCreatedOn","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Error Message">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_ErrorMessage" runat="server" Text='<%# Eval("vch_Error_Msg") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Success Message">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_SuccessMessage" runat="server" Text='<%# Eval("vch_success_message") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <span style="color: red; font-weight: 600; font-size: 13px; font-style: italic;">No record(s) found for application landing status(Forward to IDCO). </span>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>

                                            <div class="form-group ">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <p class="text-primary">Land Allotment Progress Status</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridLandIdco" class="table table-bordered table-hover" runat="server"
                                                    AutoGenerateColumns="false" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Proposal Number" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_ProposalNo" runat="server" Text='<%# Eval("vchProposalNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CAF No" ItemStyle-Width="9%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_CAFNo" runat="server" Text='<%# Eval("vchCAFNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IDCO Status" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_IDCOStatus" runat="server" Text='<%# Eval("PIDCOStatus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Process Fee Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_Processfeestatus" runat="server" Text='<%# Eval("vchProcessFeeStatus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Allotment Order Link">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HypLnk_AllotmentOrderLink" ForeColor="Blue" Text='<%#Eval("vchAllotmentOrderLink") %>'
                                                                    runat="server" NavigateUrl='<%#Eval("vchAllotmentOrderLink") %>' Target="_blank" ToolTip="Click here to view land allotment order details"></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Created On" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_AllotmentOderLink" runat="server" Text='<%# Eval("dtmCreatedOn","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <span style="color: red; font-weight: 600; font-size: 13px; font-style: italic;">No record(s) found for land allotment progress status.</span>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        
                                             </div>

                                        <div class="form-group ">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Payment Details
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdPaymentDetails" class="table table-bordered table-hover" runat="server" OnRowDataBound="GrdPaymentDetails_RowDataBound"
                                                AutoGenerateColumns="false" Width="100%" EmptyDataText="No record found.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Proposal Number" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_ProposalNo" runat="server" Text='<%# Eval("vchApplicationNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_OrderNo" runat="server" Text='<%# Eval("vchOrderNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Status" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HdnPaymStatus" runat="server" Value='<%# Eval("intPaymentStatus") %>' />
                                                            <asp:Label ID="Lbl_PaymentStatus" runat="server" Text='<%# Eval("vchPaymentStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Challan Amount" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_ChallanAmount" runat="server" Text='<%# Eval("vchChallanAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Challan No" ItemStyle-Width="12%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_ChallanNo" runat="server" Text='<%# Eval("vchChallanRefid") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Transaction Id" ItemStyle-Width="12%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_BankTranId" runat="server" Text='<%# Eval("vchBankTransid") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank Transaction Status" ItemStyle-Width="12%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_BankTranStatu" runat="server" Text='<%# Eval("vchBankTransStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Status Check">
                                                        <ItemTemplate>
                                                            <asp:Button ID="BtnPaymentTransaction" runat="server" Text="Verify From Treasury" OnClick="BtnPaymentTransaction_Click" CssClass="btn btn-primary btn-sm" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>                                               
                                            </asp:GridView>
                                        </div>

                                        <asp:Label ID="Lbl_Msg_Restful" runat="server"></asp:Label>

                                        <div class="form-group ">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Query Details
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdQuereyDetails" class="table table-bordered table-hover" runat="server" OnRowDataBound="GrdQuereyDetails_RowDataBound"
                                                AutoGenerateColumns="false" Width="100%" EmptyDataText="No record found." DataKeyNames="vchFileName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Proposal Number" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_ProposalNo" runat="server" Text='<%# Eval("vchProposalNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Times" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_NoofTime" runat="server" Text='<%# Eval("intNoOfTimes") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Query Type" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_QueryType" runat="server" Text='<%# Eval("vchQuerytype") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="Hdnstatus" runat="server" Value='<%# Eval("intStatus") %>' />
                                                            <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Remarks" runat="server" Text='<%# Eval("vchRemarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Query Date" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_CreatedOn" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Query Doc " ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HdnQueryFile" runat="server" Value='<%#Eval("vchFileName") %>'></asp:HiddenField>
                                                            <asp:LinkButton ID="LnkBtnQueryFile" runat="server" OnClick="LnkBtnQueryFile_Click"><i class="fa fa-download" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:Label ID="LblQueryFile" runat="server" Visible="false" Text='<%# Eval("vchFileName") %>' ForeColor="Red"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>

                                        <div class="form-group ">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Action Details
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdActionDetails" class="table table-bordered table-hover" runat="server" DataKeyNames="vchFileName,vchPEALCertificate,vchScoreCard" OnRowDataBound="GrdActionDetails_RowDataBound"
                                                AutoGenerateColumns="false" Width="100%" EmptyDataText="No record found.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText=" Sl No" ItemStyle-Width="4%">

                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Proposal Number" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_ProposalNo" runat="server" Text='<%# Eval("vchProposalNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Remarks" runat="server" Text='<%# Eval("vchRemarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="Hdn_Status" runat="server" Value='<%# Eval("intStatus") %>' />
                                                            <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_CreatedOn" runat="server" Text='<%# Eval("dtmUpdatedOn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reference Doc" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnfileval" runat="server" Value='<%#Eval("vchFileName") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprlnkreferdoc" runat="server" class="fa fa-download" aria-hidden="true" />

                                                            <asp:Label ID="lblReferdoc" runat="server" Visible="false" Text='<%# Eval("vchFileName") %>' ForeColor="Red"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Certificate" ItemStyle-Width="8%">
                                                        <ItemTemplate>

                                                            <asp:HiddenField ID="hdnCertif" runat="server" Value='<%#Eval("vchPEALCertificate") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprlnkCertif" runat="server" class="fa fa-download" aria-hidden="true" />

                                                            <asp:Label ID="lblCertif" runat="server" Visible="false" Text='<%# Eval("vchPEALCertificate") %>' ForeColor="Red"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Score Card" ItemStyle-Width="8%">
                                                        <ItemTemplate>

                                                            <asp:HiddenField ID="hdnScoreCard" runat="server" Value='<%#Eval("vchScoreCard") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprlnkscorecard" runat="server" class="fa fa-download" aria-hidden="true" />

                                                            <asp:Label ID="lblScoreCard" runat="server" Visible="false" Text='<%# Eval("vchScoreCard") %>' ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>

                                      <%--  <div class="form-group ">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">AMS Details
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="DivNoRecordAMS" runat="server" class="col-sm-12">
                                            <asp:Label ID="Lbl_NorecordAMS" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div id="DivAMSdetails" runat="server">
                                        <div class="form-group row">
                                                <label class="col-sm-2">
                                                    AMS Status</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_AMSstatus" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Nodal Officer</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Nodalofficer" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>

                                        </div>--%>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
    <script type="text/javascript" language="javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function ValidateApplicationKey() {
            if (!blankFieldValidation('txtProposalID', 'Proposal no. ', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('txtProposalID', 'Proposal no.', projname)) {
                $("#popup_ok").click(function () { $("#txtProposalID").focus(); });
                return false;
            }
        }
    </script>
</asp:Content>


