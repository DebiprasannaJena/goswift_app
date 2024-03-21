<%--'*******************************************************************************************************************
' File Name         : View_Investor_Regd_Details.aspx
' Description       : View Investor Registration Details
' Created by        : Sushant Jena
' Created On        : 11-Dec-2018
' Modification History:' <CR no.>       <Date>         <Modified by>        <Modification Summary>      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="View_Investor_Regd_Details.aspx.cs" Inherits="Portal_PAN_Operation_View_Investor_Regd_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-tachometer"></i>
            </div>
            <div class="header-title">
                <h1>
                    Investor Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Manage User</a></li><li><a>View User Details</a></li>
                </ul>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Unit Name</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Unit_Name" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    Industry Type 
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label runat="server" ID="Lbl_Industry_Type" CssClass="form-control-static" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Applicant Name</label>
                                <div class="col-sm-10">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Applicant_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Address</label>
                                <div class="col-sm-10">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Address" runat="server" CssClass="form-control-static"></asp:Label>
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
                                    <asp:Label ID="Lbl_Email_Id" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Site Location</label>
                                <div class="col-sm-10">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Site_Location" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Investment Level</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Investment_Level" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    District
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_District" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">
                                    PAN</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_PAN" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    Block
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Block" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">
                                    <asp:Label ID="Lbl_Doc_Type" runat="server"></asp:Label>
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_EIN_IEM_No" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    Sector
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Sector" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Document</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" ToolTip="Click here to view document !"><i class="fa fa-download"></i></asp:HyperLink>
                                    <asp:Label ID="Lbl_Doc_Text" runat="server" CssClass="form-control-static" ForeColor="Red"></asp:Label>
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="LnkBtn_Download_Doc" runat="server" OnClick="LnkBtn_Download_Doc_Click"
                                        ToolTip="Click here to download document from AIM portal.">Click here to download</asp:LinkButton>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                </div>
                                <label class="col-sm-2">
                                    Sub Sector
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Sub_Sector" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Unit Type
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Unit_Type" runat="server" CssClass="form-control-static" Font-Bold="true"
                                        ForeColor="Red"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    GSTIN
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_GSTIN" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                     
                            <hr />
                            <div class="form-group">
                                <label class="col-sm-2">
                                    User Id
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_User_Id" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    User Level
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_User_Level" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Alternate User Id
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Alias_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    OTP Status
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_OTP_Status" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Parent Unit Name
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Parent_Unit_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    Approval Status
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Approval_Status" runat="server" CssClass="form-control-static"
                                        Font-Bold="true"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <%--Satya added--%>
                                <label class="col-sm-2">
                                    Registration Date
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_RegDate" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    Approve Date
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_AppDate" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <%--Satya added--%>

                                 <label class="col-sm-2" runat="server" id="Lbl_ApprovalName">
                                    Approved By 
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_AppBy" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <label class="col-sm-3">
                                    Approval Required (1st Level)
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_App_Req" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3">
                                    Approval Status (1st Level)
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Approval_Status_1st_Level" runat="server" CssClass="form-control-static"
                                        Font-Bold="true"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3">
                                    Approval Date (1st Level)
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Approval_Date_1st_Level" runat="server" CssClass="form-control-static"
                                        Font-Bold="true"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Registration Source
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Regd_Source" runat="server" CssClass="form-control-static" Font-Bold="true"
                                        ForeColor="Red"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    Investor SWS Id
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_SWS_Id" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Redirection URL Sent Status
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Redirection_Url_Sent_Status" runat="server" CssClass="form-control-static"
                                        Font-Bold="true"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    Redirection URL
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Redirection_Url" runat="server" CssClass="form-control-static"
                                        ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <hr />
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
