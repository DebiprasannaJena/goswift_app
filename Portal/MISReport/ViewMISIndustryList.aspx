<%--'*******************************************************************************************************************
' File Name         : ViewMISIndustryList.aspx
' Description       : Get Industry Related Details Based On InvestorID
' Created by        : Satyaprakash
' Created On        : 16-04-2019
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewMISIndustryList.aspx.cs" Inherits="Portal_MISReport_ViewMISIndustryList" %>

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
                    Industry Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>MIS Report</a></li><li><a>Industry List</a></li>
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
                                <div class="col-sm-10">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Unit_Name" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <label class="col-sm-2">
                                    Applicant Name</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Applicant_Name" runat="server" CssClass="form-control-static"></asp:Label>
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
                            <div class="form-group">
                                <label class="col-sm-2">
                                   Registration Address-1</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Address" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                               
                                <label class="col-sm-2">
                                    Site Location Address-1</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Site_Location" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>                               
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2">
                                   Registration Address-2</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Regd_Address_2" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                   Site Location Address-2</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Sl_Address_2" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>

                               <div class="form-group">
                                <label class="col-sm-2">
                                   Registration Country</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Regd_Country" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                   Site Location Country</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Sl_Country" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>

                             <div class="form-group">
                                <label class="col-sm-2">
                                   Registration State</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Regd_State" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                   Site Location State</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Sl_State" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>

                              <div class="form-group">
                                <label class="col-sm-2">
                                   Registration City</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Regd_City" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                   Site Location City</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Sl_City" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>

                              <div class="form-group">
                                <label class="col-sm-2">
                                   Registration Pincode</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Regd_Pincode" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                   Site Location Pincode</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Sl_Pincode" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>


                            </div>
                              <div class="form-group">
                                <label class="col-sm-2">
                                  Entity Type</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Enity_Type" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                  <div runat="server" id="Div_Cin" visible="false">
                                <label class="col-sm-2">
                                   CIN Number</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_CIN_Number" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                  </div>

                                   <div runat="server" id="Div_Llpin" visible="false">
                                <label class="col-sm-2">
                                   LLPIN Number</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_LLPIN_Number" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                  </div>
                                
                                <div class="clearfix">
                                </div>
                            </div>

                               <div class="form-group">
                                    <div runat="server" id="Div_Cin_Data" visible="false">
                                <label class="col-sm-2">
                                    CIN Data</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                   <asp:LinkButton ID="DownloadLink" runat="server" OnClick="DownloadLink_Click">
                                         <i class="fa fa-download"></i> 
                                     </asp:LinkButton>
                                    <asp:Label ID="Lbl_Cin_Data" runat="server" CssClass="form-control-static" ForeColor="Red"></asp:Label>
                                </div>
                                        </div>
                                    <div runat="server" id="Div_Llpin_Date" visible="false">
                                <label class="col-sm-2">
                                   LLPIN Data
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                     <asp:LinkButton ID="LlpiData_Download" runat="server" OnClick="LlpiData_Download_Click">
                                         <i class="fa fa-download"></i> 
                                     </asp:LinkButton>
                                    <asp:Label ID="Lbl_Llpin_Data" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                        </div>
                                <div class="clearfix">
                                </div>
                            </div>


                            <div class="form-group">
                              <label class="col-sm-2">  
                                    Mobile No</label>
                                <div class="col-sm-10">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Mobile_No" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>                              
                                <div class="clearfix">
                                </div>
                            </div>

                            <hr />
                            
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
                                   Pan Holder Name</label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Pan_Holder_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                </div>
                                <label class="col-sm-2">
                                    Dob
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Dob" runat="server" CssClass="form-control-static"></asp:Label>
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
                                <label class="col-sm-2">
                                    Rejection Cause
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Rejection_Cause" runat="server" CssClass="form-control-static"
                                        Font-Bold="true" ForeColor="Red"></asp:Label>
                                </div>


                                <label class="col-sm-2">
                                    Approval Remark 
                                </label>
                                <div class="col-sm-4">
                                    <span class="colon">:</span>
                                    <asp:Label ID="Lbl_Approval_Remark" runat="server" CssClass="form-control-static"
                                        Font-Bold="true" ForeColor="BlueViolet"></asp:Label>
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
