<%--'*******************************************************************************************************************
' File Name         : ViewInvestorDetails.aspx
' Description       : View details of Investor data
' Created by        : AMit Sahoo
' Created On        : 12 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="InvestorDetails.aspx.cs" Inherits="Investor_InvestorDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Investor</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Manage Investor</a></li><li><a>Investor</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <%--<div class="panel-heading">
                           <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="AddServiceMaster.aspx"> 
                              <i class="fa fa-plus"></i>  Add List </a>  
                           </div>
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ViewServiceMaster.aspx"> 
                              <i class="fa fa-file"></i> View List </a>  
                           </div>
                           
                        </div>--%>
                        <div class="panel-body">
                     <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label for="fname">
                                                    Industry Name <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtIName" CssClass="form-control" runat="server">Vedanta Aluminium</asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="mname">
                                                    District <span class="text-red">*</span></label>
                                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                     <asp:ListItem Text="Jharsuguda" Value="1" Selected="True"> </asp:ListItem>
                                                    <asp:ListItem Text="Jajpur" Value="2"> </asp:ListItem>
                                                    <asp:ListItem Text="Jagatsinghpur" Value="3"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="lname">
                                                    Block <span class="text-red">*</span></label>
                                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                     <asp:ListItem Text="Lakhanpur" Value="1" Selected="True"></asp:ListItem>
                                                     <asp:ListItem Text="Sukinda" Value="2"></asp:ListItem>
                                                     <asp:ListItem Text="Ersama" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label for="email">
                                                    Email Id <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server">amit@vedanta.com</asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="cemail">
                                                    Category <span class="text-red">*</span></label>
                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                                 <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="MSME" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Large" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="dob">
                                                    GST <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtGST" CssClass="form-control" runat="server">AST1523</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label for="Address">
                                                    PAN <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtPAN" CssClass="form-control" runat="server" MaxLength="400">DLTPS54376</asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="country">
                                                    Type Of Registration <span class="text-red">*</span></label>
                                                <asp:DropDownList ID="ddlRegistration" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">IEM Number</asp:ListItem>
                                                    <asp:ListItem Value="2">EM-II Number</asp:ListItem>
                                                    <asp:ListItem Value="3">Udyog Adhaar No.</asp:ListItem>
                                                    <asp:ListItem Value="4">EIN Number</asp:ListItem>
                                                    <asp:ListItem Value="5">Production Certificate No.</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="State">
                                                    Regd No. <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtRegdNo" CssClass="form-control" runat="server">7845634</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label for="pincode">
                                                    Contact Person <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtContactPerson" CssClass="form-control" runat="server">751013</asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="Fax">
                                                    Office Telephone No.</label>
                                                <asp:TextBox ID="txtTelNo" CssClass="form-control" runat="server">06758256785</asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="Mobile">
                                                    Mobile No. <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server">9861098613</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label for="ConfirmMobile">
                                                    Promotor AADHAR No. <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtAadharNo" CssClass="form-control" runat="server">1345678907654326</asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="Mobile">
                                                    Correspondent Address <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" TextMode="MultiLine">Jharsuguda</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                            <div class="form-group">
                                <div class="row">
                                   
                                    <div class="col-sm-12" align="center">
                            <asp:Button ID="Button1" runat="server" Text="Submit" 
                                            CssClass=" btn btn-success" Width="80" onclick="btnSubmit_Click"
                                            />
                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                            CssClass=" btn btn-warning" Width="80" onclick="btnCancel_Click"
                                            />
                        </div>
                            </div>
                        </div>
                     </div>
                  </div>
               </div>
               
            </section>
        <!-- /.content -->
    </div>
</asp:Content>
