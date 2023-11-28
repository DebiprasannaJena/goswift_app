<%--'*******************************************************************************************************************
' File Name         : Incentive_Name_Master.aspx
' Description       : Add Incentive Name Master
' Created by        : Sushant Kumar Jena
' Created On        : 09th Sept 2017
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="Incentive_Name_Master.aspx.cs" Inherits="Portal_Incentive_Incentive_Name_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        function numeralsOnly(evt, tt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                //alert("Enter Numerals only in this Field!");
                //tt.value="";              
                return false;
            }
            return true;
        }

        /*----------------------------------------------------------------------*/

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
        $(window).load(function () {

            $('#ContentPlaceHolder1_Btn_Submit').click(function () {
                if (DropDownValidation('ContentPlaceHolder1_DrpDwn_OG_Name', '0', 'OG Name', projname) == false) {
                    $("#ContentPlaceHolder1_DrpDwn_OG_Name").focus();
                    return false;
                };

                if (blankFieldValidation('ContentPlaceHolder1_Txt_Incentive_Name', 'Incentive Name', projname) == false) {
                    $("#ContentPlaceHolder1_Txt_Incentive_Name").focus();
                    return false;
                };
                if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Incentive_Name', 'Incentive Name', projname) == false) {
                    $("#ContentPlaceHolder1_Txt_Incentive_Name").focus();
                    return false;
                };
                if (DropDownValidation('ContentPlaceHolder1_DrpDwn_Disburse_Type', '0', 'Disbursement Type', projname) == false) {
                    $("#ContentPlaceHolder1_DrpDwn_Disburse_Type").focus();
                    return false;
                };

                if (DropDownValidation('ContentPlaceHolder1_DrpDwn_Avail', '0', 'Availability Type', projname) == false) {
                    $("#ContentPlaceHolder1_DrpDwn_Avail").focus();
                    return false;
                };
                if (DropDownValidation('ContentPlaceHolder1_DrpDwn_Nature', '0', 'Nature', projname) == false) {
                    $("#ContentPlaceHolder1_DrpDwn_Nature").focus();
                    return false;
                };

                if (blankFieldValidation('ContentPlaceHolder1_Txt_Time_Frame', 'Time Frame', projname) == false) {
                    $("#ContentPlaceHolder1_Txt_Time_Frame").focus();
                    return false;
                };
                if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Time_Frame', 'Time Frame', projname) == false) {
                    $("#ContentPlaceHolder1_Txt_Time_Frame").focus();
                    return false;
                };

                if (DropDownValidation('ContentPlaceHolder1_DrpDwn_Periodicity', '0', 'Periodicity', projname) == false) {
                    $("#ContentPlaceHolder1_DrpDwn_Periodicity").focus();
                    return false;
                };

                if (blankFieldValidation('ContentPlaceHolder1_Txt_Max_Limit', 'Maximum Limit for Applying Incentive', projname) == false) {
                    $("#ContentPlaceHolder1_Txt_Max_Limit").focus();
                    return false;
                };
                if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Max_Limit', 'Maximum Limit for Applying Incentive', projname) == false) {
                    $("#ContentPlaceHolder1_Txt_Max_Limit").focus();
                    return false;
                };

                return ConfirmAction('ContentPlaceHolder1_Btn_Submit');
            });
        });

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

                    location.href = 'View_Incentive_Name_Master.aspx?linkm=' + linkm + '&linkn=' + linkn + '&btn=' + btn + '&tab=' + tab + '&ranNum=' + RandomNo + '';
                    return true;
                }
                else {
                    return false;
                }
            });
        }  

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <%-- <section class="content-header">--%>
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Incentive Name Master</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Incentive Policy</a></li><li><a>Add Incentive</a></li></ul>
            </div>
        </div>
        <%-- </section>--%>
        <div class="content">
            <%--<section class="content">--%>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="Incentive_Name_Master.aspx"><i class="fa fa-plus"></i>
                                    Add </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="View_Incentive_Name_Master.aspx"><i class="fa fa-file">
                                </i>View </a>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="udpDiv" runat="server">
                            <ContentTemplate>
                                <div class="ibox-content">
                                    <br />
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-sm-3">
                                            Incentive Name
                                        </label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Incentive_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <label class="col-md-offset-1 col-md-2 col-sm-3">
                                            OG Name
                                        </label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_OG_Name" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-sm-3">
                                            Nature
                                        </label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_Nature" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <label class="col-md-offset-1 col-md-2 col-sm-3">
                                            Time Frame</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Time_Frame" runat="server" CssClass="form-control" onkeypress="return numeralsOnly(event,this);"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-sm-3">
                                            Disburement Type</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_Disburse_Type" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <label class="col-md-offset-1 col-md-2 col-sm-3">
                                            Periodicity (In Months)</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_Periodicity" runat="server" CssClass="form-control">
                                                <asp:ListItem>ANNUAL</asp:ListItem>
                                                <asp:ListItem>ONETIME</asp:ListItem>
                                            </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-sm-3">
                                            Availibility</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_Avail" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <label class="col-md-offset-1 col-md-2 col-sm-3">
                                            Maximum Limit for Applying Incentive (In Months)</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Max_Limit" runat="server" CssClass="form-control" onkeypress="return numeralsOnly(event,this);"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-sm-3">
                                            Maximum Limit for Applying Incentive for Priority Unit (In Months)</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Max_Limit_Priority" runat="server" CssClass="form-control" onkeypress="return numeralsOnly(event,this);"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <label class="col-md-offset-1 col-md-2 col-sm-3">
                                            Maximum Limit for Applying Incentive for Pioneer Unit (In Months)</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Max_Limit_Pioneer" runat="server" CssClass="form-control" onkeypress="return numeralsOnly(event,this);"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-sm-3">
                                            Is Provisional</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:RadioButtonList ID="RadBtn_Is_Provisional" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <label class="col-md-offset-1 col-md-2 col-sm-3">
                                            Incentive Short Code</label>
                                        <div class="col-md-3 col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Short_Code" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="mandetory">*</span>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2 col-sm-offset-1">
                                            </label>
                                            <div class="col-sm-4">
                                                <asp:Button ID="Btn_Submit" runat="server" Text="Submit" class="btn btn-primary"
                                                    OnClick="Btn_Submit_Click" />
                                                <asp:Button ID="Btn_Reset" runat="server" Text="Reset" class="btn btn-danger" OnClick="Btn_Reset_Click" />
                                                <asp:HiddenField ID="Hid_Inct_Id" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                    </div>
                </div>
            </div>
            <%--</section>--%>
        </div>
    </div>
</asp:Content>
