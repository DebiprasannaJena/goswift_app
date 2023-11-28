<%--'*******************************************************************************************************************
' File Name         : ViewInvestorDetails.aspx
' Description       : View details of Investor data
' Created by        : AMit Sahoo
' Created On        : 12 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="Sector_Manage.aspx.cs"
    Inherits="Incentive_Sector_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    ClientIDMode="Static">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../js/WebValidation.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <%-- <section class="content-header">--%>
                <div class="content-header">
                    <div class="header-icon">
                        <i class="fa fa-dashboard"></i>
                    </div>
                    <div class="header-title">
                        <h1>
                            Manage Sector</h1>
                        <ul class="breadcrumb">
                            <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                            <li><a>Manage Sector</a></li><li><a>Sector</a></li></ul>
                    </div>
                </div>
                <%-- </section>--%>
                <!-- Main content -->
                <%--       <section class="content">--%>
                <div class="content">
                    <div class="row">
                        <!-- Form controls -->
                        <div class="col-sm-12">
                            <div class="panel panel-bd lobidisable">
                                <div class="panel-heading">
                                    <div class="btn-group buttonlist">
                                        <a class="btn btn-add " href="Sector_Manage.aspx"><i class="fa fa-plus"></i>Add</a>
                                    </div>
                                    <div class="btn-group buttonlist">
                                        <a class="btn btn-add " href="SectorView.aspx"><i class="fa fa-file"></i>View</a>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Policy Name</label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList CssClass="form-control" ID="DrpDwn_Policy_Name" runat="server">
                                                </asp:DropDownList>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Tag Sector</label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList CssClass="form-control" ID="DrpDwn_Sector" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DrpDwn_Sector_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Sub Sector
                                            </label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList CssClass="form-control" ID="DrpDwn_Sub_Sector" runat="server">
                                                </asp:DropDownList>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Is Sectorial Policy
                                            </label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:CheckBox ID="ChkBx_Sectorial_Policy" Style="margin-top: 2px;" runat="server">
                                                </asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Is Priority IPR
                                            </label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:CheckBox ID="ChkBx_Priority_IPR" Style="margin-top: 2px;" runat="server"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Derived Sector</label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="Txt_Description" TextMode="MultiLine" onKeyUp="limitText(this,this.form.count,500);"
                                                    MaxLength="500" onKeyDown="limitText(this,this.form.count,500);" CssClass="form-control"
                                                    Height="80px" runat="server"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass=" btn btn-success"
                                                    Width="80" OnClick="Btn_Submit_Click" OnClientClick="return validateSectorMaster();" />
                                                <asp:Button ID="Btn_Reset" runat="server" Text="Reset" OnClick="Btn_Reset_Click"
                                                    CssClass=" btn btn-warning" Width="80" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--    </section>--%>
                <!-- /.content -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

        function validateSectorMaster() {
            if (!DropDownValidation('DrpDwn_Policy_Name', '0', 'policy name', projname)) {
                $("#popup_ok").click(function () { $("#DrpDwn_Policy_Name").focus(); });
                return false;
            }
            if (!DropDownValidation('DrpDwn_Sector', '0', 'sector name', projname)) {
                $("#popup_ok").click(function () { $("#DrpDwn_Sector").focus(); });
                return false;
            }
            if (!DropDownValidation('DrpDwn_Sub_Sector', '0', 'sub sector name', projname)) {
                $("#popup_ok").click(function () { $("#DrpDwn_Sub_Sector").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_Description', 'Derived sector', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_Description', 'derived sector', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Description").focus(); });
                return false;
            }
        }


        /*----------------------------------------------------*/
        ////// Alert and Redirect
        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    var linkm = '<%=this.Request.QueryString["linkm"]%>';
                    var linkn = '<%=this.Request.QueryString["linkn"]%>';
                    var btn = '<%=this.Request.QueryString["btn"]%>';
                    var tab = '<%=this.Request.QueryString["tab"]%>';
                    var RandomNo = '<%=this.Session["RandomNo"]%>';

                    location.href = 'SectorView.aspx?linkm=' + linkm + '&linkn=' + linkn + '&btn=' + btn + '&tab=' + tab + '&ranNum=' + RandomNo + '';
                    return true;
                }
                else {
                    return false;
                }
            });
        }  
    </script>
</asp:Content>
