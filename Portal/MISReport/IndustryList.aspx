<%--'*******************************************************************************************************************
' File Name         : IndustryList.aspx
' Description       : Get Industry List Based On PAN Number, Unit Name and Status
' Created by        : Satyaprakash
' Created On        : 16-04-2019
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="IndustryList.aspx.cs" Inherits="Portal_MISReport_IndustryList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    ClientIDMode="Static">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-tachometer"></i>
            </div>
            <div class="header-title">
                <h1>
                    Industry List</h1>
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="panel-body">
                                    <div class="search-sec">
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-md-2 col-sm-3">
                                                    PAN Number</label>
                                                <div class="col-md-3 col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="Txt_PAN" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <label class="col-md-2 col-sm-3">
                                                    Unit Name
                                                </label>
                                                <div class="col-md-3 col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="Txt_Unit_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-md-2 col-sm-3">
                                                    Status
                                                </label>
                                                <div class="col-md-3 col-sm-3" runat="server" id="st3">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpStatusDet" runat="server">
                                                        <asp:ListItem Text="--Select--" Value="3"></asp:ListItem>
                                                        <asp:ListItem Value="0">Pending</asp:ListItem>
                                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                                        <asp:ListItem Value="2">Rejected</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="col-sm-2">
                                                </label>
                                                <div class="col-sm-4">
                                                    <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btn btn-success"
                                                        OnClick="Btn_Search_Click" ToolTip="Click Here to View Users !!" OnClientClick="return ValidateApplicationKey();" />
                                                    <asp:Button ID="Btn_Reset" runat="server" Text="Reset" CssClass="btn btn-danger"
                                                        OnClick="Btn_Reset_Click" ToolTip="Click here to reset above fields !!" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div align="right">
                                        <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                            Font-Bold="true" OnClick="lbtnAll_Click"></asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <asp:Label ID="lblPaging" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdIndustryList" runat="server" CssClass="table table-bordered table-striped bg-white"
                                            AllowPaging="True" PageSize="10" OnPageIndexChanging="GrdIndustryList_PageIndexChanging"
                                            OnRowDataBound="GrdIndustryList_RowDataBound" AutoGenerateColumns="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsl" runat="server" Text='<%#(GrdIndustryList.PageIndex * GrdIndustryList.PageSize) + (GrdIndustryList.Rows.Count + 1)%>'></asp:Label>
                                                        <asp:HiddenField ID="Hid_Investor_Id" runat="server" Value='<%# Eval("INT_INVESTOR_ID") %>' />
                                                        <%-- <asp:HiddenField ID="Hid_Parent_Id" runat="server" Value='<%# Eval("INT_PARENT_ID") %>' />--%>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Name">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkBtn_Inv_Name" runat="server" ToolTip="Click here to view details !!"
                                                            Font-Bold="true" OnClick="LnkBtn_Inv_Name_Click" Text='<%# Eval("VCH_INV_NAME")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Site Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Site_Location" runat="server" Text='<%# Eval("VCH_SITELOCATION") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PAN Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_PAN" runat="server" Text='<%# Eval("VCH_PAN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Mobile_No" runat="server" Text='<%# Eval("VCH_OFF_MOBILE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="9%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <%--<asp:HiddenField ID="Hid_Approval_Status" runat="server" Value='<%# Eval("INT_APPROVAL_STATUS") %>' />--%>
                                                        <asp:Label ID="Lbl_Approval_Status" runat="server" Text='<%# Eval("VCH_APPROVAL_STATUS") %>'
                                                            Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="7%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rejection Cause">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_REJECTION_CAUSE" runat="server" Text='<%# Eval("VCH_REJECTION_CAUSE") %>'
                                                            Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No Data Found !!
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        function ValidateApplicationKey() {

            var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
            if ($('#Txt_PAN').val() == '' && $('#Txt_Unit_Name').val() == '' && $('#drpStatusDet').val() == '3') {
                jAlert('<strong>Please provide input  at least in one field !!</strong>', projname);
                return false;
            }
        }          
    </script>
</asp:Content>
