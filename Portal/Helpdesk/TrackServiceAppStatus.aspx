<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="TrackServiceAppStatus.aspx.cs"
    Inherits="Portal_HelpDesk_TrackServiceAppStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" ClientIDMode="Static">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../js/WebValidation.js" type="text/javascript"></script>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Track Service Application Status</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>HelpDesk</a></li>
                    <li><a>Track Service App Status</a></li>
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
                                                Application Number</label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="txtServiceID" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn btn-add -sm"
                                                    runat="server" Text="Search" OnClientClick="return ValidateApplicationKey();"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="DivDetails" runat="server">
                                        <div class="form-group ">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="h4-header">Application Details
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
                                                    Application Number</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <label>
                                                        <asp:HyperLink ID="Hypr_ApplicationNo" runat="server" Font-Bold="true" CssClass="datalabel" Target="_blank" ToolTip="Click hear to view the application details "></asp:HyperLink></label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Service Name</label>
                                                <div class="col-sm-9">
                                                    <span class="colon">:</span>
                                                    <asp:HiddenField ID="Hdn_SearviceId" runat="server" />
                                                    <asp:Label ID="Lbl_Searvice_Name" runat="server" CssClass="datalabel" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Investor Name</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Investor_Name" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Proposal Id</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Proposal_Id" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Created On</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_created_on" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Current Status</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_current_status" runat="server" CssClass="datalabel" ForeColor="Red"></asp:Label>
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
                                                    <asp:Label ID="Lbl_Action_Tobe_Taken_By" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                    Payment Status</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Payment_Status" runat="server" CssClass="datalabel"></asp:Label>
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
                                                    Industry Name
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_apply_By" runat="server" CssClass="datalabel" Font-Bold="true"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    Payment Date
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Payment_date" runat="server" CssClass="datalabel"></asp:Label>
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
                                                    <asp:Label ID="Lbl_ortps_timeline" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <label class="col-sm-2">
                                                    End of Raise Query Date</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Querey_date" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-2">
                                                </label>
                                                <div class="col-sm-4">
                                                </div>
                                                <label class="col-sm-2">
                                                    End Of Revert Query Date</label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="Lbl_Revert_Query" runat="server" CssClass="datalabel"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
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
                                                AutoGenerateColumns="false" Width="100%" EmptyDataText="No record found .">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Application No" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_ApplicationNo" runat="server" Text='<%# Eval("vchApplicationNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_OrderNo" runat="server" Text='<%# Eval("vchOrderNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Status" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HdnPaymStatus" runat="server" Value='<%# Eval("intPaymentStatus") %>' />
                                                            <asp:Label ID="Lbl_PaymentStatus" runat="server" Text='<%# Eval("VCH_PAYMENTSTATUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Challan Amount" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_ChallanAmount" runat="server" Text='<%# Eval("vchChallanAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Challan No" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_ChallanNo" runat="server" Text='<%# Eval("vchChallanRefid") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Transaction Id" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_BankTranId" runat="server" Text='<%# Eval("vchBankTransid") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Date" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_OrderDate" runat="server" Text='<%# Eval("dtmOrderDate" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Status Check" ItemStyle-Width="14%">
                                                        <ItemTemplate>
                                                            <asp:Button ID="Btn_transaction" runat="server" Text="Verify From Treasury" OnClick="Btn_transaction_Click" CssClass="btn btn-primary btn-sm" />
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
                                                AutoGenerateColumns="false" Width="100%" EmptyDataText="No record found ." DataKeyNames="vchFileName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Application No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_ApplicationNo" runat="server" Text='<%# Eval("VCH_APPLICATION_UNQ_KEY") %>'></asp:Label>
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
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="Hdnstatus" runat="server" Value='<%# Eval("intStatus") %>' />
                                                            <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("VCH_STATUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Remarks" runat="server" Text='<%# Eval("vchRemarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Query Date" ItemStyle-Width="6%">
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
                                            <asp:GridView ID="GrdActionDetails" class="table table-bordered table-hover" runat="server" DataKeyNames="vchApprovalDoc,vchFileName,VCH_INSPECTION_FILENAME,VCH_RESTORATION_FILENAME" OnRowDataBound="GrdActionDetails_RowDataBound"
                                                AutoGenerateColumns="false" Width="100%" EmptyDataText="No record found .">
                                                <Columns>
                                                    <asp:TemplateField HeaderText=" Sl No" ItemStyle-Width="4%">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Application No" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_ApplicationNo" runat="server" Text='<%# Eval("VCH_APPLICATION_UNQ_KEY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Remarks" runat="server" Text='<%# Eval("vchRemarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("intStatus") %>' />
                                                            <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("VCH_STATUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Date" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_CreatedOn" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approval Doc" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnfilevalcert" runat="server" Value='<%#Eval("vchApprovalDoc") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprApprodoc" runat="server" class="fa fa-download" aria-hidden="true" />
                                                            <asp:Label ID="lblapproval" runat="server" Visible="false" Text='<%# Eval("vchApprovalDoc") %>' ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reference Doc" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnfileval" runat="server" Value='<%#Eval("vchFileName") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprReferndoc" runat="server" class="fa fa-download" aria-hidden="true" />
                                                            <asp:Label ID="lblReferdoc" runat="server" Visible="false" Text='<%# Eval("vchFileName") %>' ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inspection Doc" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnInspectionDocu" runat="server" Value='<%#Eval("VCH_INSPECTION_FILENAME") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprInspectdoc" runat="server" class="fa fa-download" aria-hidden="true" />
                                                            <asp:Label ID="lblinspdoc" runat="server" Visible="false" Text='<%# Eval("VCH_INSPECTION_FILENAME") %>' ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Restoration Doc" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnRestorationDocu" runat="server" Value='<%#Eval("VCH_RESTORATION_FILENAME") %>'></asp:HiddenField>
                                                            <asp:HyperLink ID="hprRestordoc" runat="server" class="fa fa-download" aria-hidden="true" />
                                                            <asp:Label ID="lblrestdoc" runat="server" Visible="false" Text='<%# Eval("VCH_RESTORATION_FILENAME") %>' ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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
            if (!blankFieldValidation('txtServiceID', 'Application Key', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('txtServiceID', 'Application Key', projname)) {
                $("#popup_ok").click(function () { $("#txtServiceID").focus(); });
                return false;
            }
        }
    </script>
</asp:Content>
