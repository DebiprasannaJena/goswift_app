<%--'*******************************************************************************************************************
' File Name         : View_Investor_Transaction_Details.aspx
' Description       : View Investor Transaction (PEAL/Service/incentive) Details
' Created by        : Sushant Jena
' Created On        : 06-Nov-2019
' Modification History:' <CR no.>       <Date>         <Modified by>        <Modification Summary>      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="View_Investor_Transaction_Details.aspx.cs" Inherits="Portal_PAN_Operation_View_Investor_Transaction_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .subheader
        {
            color: #2389B9;
            font-weight: bold;
            font-family: Verdana;
            font-size: 14px;
        }
        
        .emptytemp
        {
            color: #BD3737F0;
            font-weight: 600;
            font-style: italic;
            font-family: Verdana;
            font-size: 11px;
        }
        
        .hr
        {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 1px;
        }
    </style>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-tachometer"></i>
            </div>
            <div class="header-title">
                <h1>
                    Investor Transaction Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Manage User</a></li>
                    <li><a>User Enquiry</a></li>
                    <li><a>View Details</a></li>
                </ul>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <h4 class="subheader">
                                        PEAL Details
                                    </h4>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div style="overflow: auto; max-height: 250px;">
                                        <asp:GridView ID="GrdPEAL" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="false" Width="100%" OnRowDataBound="GrdPEAL_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SlNo">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label1" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Proposal No">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="Hid_Proposal_No" runat="server" Value='<%# Eval("vchProposalNo") %>' />
                                                        <asp:HyperLink ID="HypLnk_Proposal_No" runat="server" Target="_blank" Text='<%# Eval("vchProposalNo") %>'> </asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Industry Name">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label2" Text='<%# Eval("vchCompName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label3" Text='<%# Eval("vchAddress") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Type">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label4" Text='<%# Eval("vchProjectType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created On">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label5" Text='<%# Eval("dtmCreatedOn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label6" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <span class="emptytemp">No Proposal Details Found... </span>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <hr class="hr" />
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <h4 class="subheader">
                                        Service Details
                                    </h4>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div style="overflow: auto; max-height: 250px;">
                                        <asp:GridView ID="GrdService" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="false" Width="100%" OnRowDataBound="GrdService_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SlNo">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbkl_SlNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Application No">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="Hid_Service_Id" runat="server" Value='<%# Eval("INT_SERVICEID") %>' />
                                                        <asp:HiddenField ID="Hid_Service_App_No" runat="server" Value='<%# Eval("VCH_APPLICATION_UNQ_KEY") %>' />
                                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" Text='<%# Eval("VCH_APPLICATION_UNQ_KEY") %>'> </asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Proposal No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label7" Text='<%# Eval("VCH_PROPOSALID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Investor Name">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label8" Text='<%# Eval("VCH_INVESTOR_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Name">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label9" Text='<%# Eval("VCH_SERVICENAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created On">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label10" Text='<%# Eval("DTM_CREATEDON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label11" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <span class="emptytemp">No Service Details Found... </span>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <hr class="hr" />
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <h4 class="subheader">
                                        Incentive Details
                                    </h4>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div style="overflow: auto; max-height: 250px;">
                                        <asp:GridView ID="GrdIncentive" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SlNo">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbkl_SlNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Application No">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkBtn_View_Application" runat="server" Text='<%# Eval("ApplicationNum") %>'
                                                            ToolTip="Show Incentive details" OnClick="LnkBtn_View_Application_Click"></asp:LinkButton>
                                                        <asp:HiddenField ID="Hid_Inct_Unique_No" runat="server" Value='<%# Eval("INTINCUNQUEID") %>' />
                                                        <asp:HiddenField ID="Hid_Form_Preview_Id" runat="server" Value='<%# Eval("nvchFormPreviewId") %>' />
                                                        <asp:HiddenField ID="Hid_Apply_Flag" runat="server" Value='<%# Eval("BITFLAG") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Proposal No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label12" Text='<%# Eval("VCHPROPOSALNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Incentive Name">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label13" Text='<%# Eval("vchInctName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created On">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label14" Text='<%# Eval("DTMCREATEDON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label15" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <span class="emptytemp">No Incentive Details Found... </span>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                     <hr class="hr" />
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-12">
                                    <h4 class="subheader">
                                        Grievance Details
                                    </h4>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>

                             <div class="form-group">
                                <div class="col-sm-12">
                                    <div style="overflow: auto; max-height: 250px;">
                                        <asp:GridView ID="GrdGrievance" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="false" Width="100%"  OnRowDataBound="GrdGrievance_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SlNo">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbkl_SlNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Application No">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="Hid_Griv_Id" runat="server" Value='<%# Eval("vchGrivId") %>' />
                                                         <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" Text='<%# Eval("vchGrivId") %>'> </asp:HyperLink>                                                       
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Applicant Name">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Label12" Text='<%# Eval("vchApplicantName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>                                               
                                                <asp:TemplateField HeaderText="Grievance Type">
                                                    <ItemTemplate>                                             
                                                        <asp:Label runat="server" ID="Label17" Text='<%# Eval("vchGrivType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Grievance Title">
                                                    <ItemTemplate>                                                   
                                                         <asp:Label runat="server" ID="Label16" Text='<%# Eval("vchGrivTitle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created On">
                                                    <ItemTemplate>                                                   
                                                         <asp:Label runat="server" ID="Label18" Text='<%# Eval("dtmCreatedOn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>                                                       
                                                        <asp:Label runat="server" ID="Label19" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <span class="emptytemp">No Grievance Details Found... </span>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>

                             <div class="form-group">
                                <div class="col-sm-12">
                                     <hr class="hr" />
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-4">
                                    <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn btn-success" OnClick="Btn_Back_Click" />
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
