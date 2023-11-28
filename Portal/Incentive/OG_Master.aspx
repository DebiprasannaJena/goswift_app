<%--'*******************************************************************************************************************
' File Name         : OG_Master.aspx
' Description       : Add Operational Guidelines (OG) Master
' Created by        : Sushant Kumar Jena
' Created On        : 07th Sept 2017
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="OG_Master.aspx.cs" Inherits="Portal_Incentive_OG_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validateOGMaster() {

            if (DropDownValidation('ContentPlaceHolder1_DrpDwn_Policy_Name', '0', 'policy name', projname) == false) {
                $("#ContentPlaceHolder1_DrpDwn_Policy_Name").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_Txt_OG_Name', 'OG name', projname) == false) {
                $("#ContentPlaceHolder1_Txt_OG_Name").focus();
                return false;
            };
            if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_OG_Name', 'OG name', projname) == false) {
                $("#ContentPlaceHolder1_Txt_OG_Name").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_Txt_Effect_Date', 'Effective date', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Effect_Date").focus();
                return false;
            };
            if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Effect_Date', 'Effective date', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Effect_Date").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_Txt_OG_Desc', 'OG description', projname) == false) {
                $("#ContentPlaceHolder1_Txt_OG_Desc").focus();
                return false;
            };
            if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_OG_Desc', 'OG description', projname) == false) {
                $("#ContentPlaceHolder1_Txt_OG_Desc").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_FileUpload_OG_Doc', 'OG Document', projname) == false) {
                $("#ContentPlaceHolder1_FileUpload_OG_Doc").focus();
                return false;
            };
            if ($('#Hid_OG_Doc_File_Name').val() == '') {
                jAlert('<strong>Please Upload OG Document !!</strong>', projname);
                $("#popup_ok").click(function () { $("#FU_OG_Doc").focus(); });
                return false;
            }

            //return ConfirmAction('ContentPlaceHolder1_Btn_Submit');
        }

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
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

                    location.href = 'View_OG_Master.aspx?linkm=' + linkm + '&linkn=' + linkn + '&btn=' + btn + '&tab=' + tab + '&ranNum=' + RandomNo + '';
                    return true;
                }
                else {
                    return false;
                }
            });
        }  
    </script>
    <script type="text/javascript">

        function pageLoad() {
            $('.date-picker').datepicker({
                format: 'dd-M-yyyy',
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
            $('.datePicker').datepicker({
                format: 'dd-M-yyyy',
                changeMonth: true,
                changeYear: true,
                autoclose: true,
                clearBtn: true
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
                    Manage OG Master</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Incentive Policy</a></li><li><a>Add OG</a></li></ul>
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
                                <a class="btn btn-add " href="OG_Master.aspx"><i class="fa fa-plus"></i>Add </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="View_OG_Master.aspx"><i class="fa fa-file"></i>View
                                </a>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="udpDiv" runat="server">
                            <ContentTemplate>
                                <div>
                                    <div class="ibox-content">
                                        <br />
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 col-sm-3">
                                                Policy Name</label>
                                            <div class="col-md-3 col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Policy_Name" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DrpDwn_Policy_Name_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <span class="mandetory">*</span>
                                            </div>
                                            <label class="col-md-offset-1 col-md-2 col-sm-3">
                                                Section Name
                                            </label>
                                            <div class="col-md-3 col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Section_Name" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 col-sm-3">
                                                OG Name</label>
                                            <div class="col-md-3 col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="Txt_OG_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                            </div>
                                            <label class="col-md-offset-1 col-md-2 col-sm-3">
                                                OG Effective Date</label>
                                            <div class="col-md-3 col-sm-3">
                                                <div class="input-group date datePicker">
                                                    <asp:TextBox ID="Txt_Effect_Date" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                </div>
                                                <span class="mandetory">*</span>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 col-sm-3">
                                                OG Description</label>
                                            <div class="col-md-3 col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="Txt_OG_Desc" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                    Rows="5" MaxLength="500" onKeyDown="limitText(this,this.form.count,500);" onKeyUp="limitText(this,this.form.count,500);"></asp:TextBox>
                                                <div class="chrtr_lmt" style="text-align: right">
                                                    <small>Maximum
                                                        <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold;
                                                            color: red; width: 26px;" type="text" value="500" />
                                                        Characters Left</small>
                                                </div>
                                                <span class="mandetory">*</span>
                                            </div>
                                            <label class="col-md-offset-1 col-md-2 col-sm-3">
                                            </label>
                                            <div class="col-md-3 col-sm-3">
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 col-sm-3">
                                                Upload OG Document
                                            </label>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <div class="input-group">
                                                    <asp:FileUpload ID="FU_OG_Doc" CssClass="form-control" runat="server" onchange="return validateFile(this);"
                                                        ToolTip="Browse File to Upload !!" />
                                                    <asp:HiddenField ID="Hid_OG_Doc_File_Name" runat="server" />
                                                    <asp:LinkButton ID="LnkBtn_Upload_OG_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                        OnClick="LnkBtn_Add_Doc_Click" ToolTip="Click Here to Upload File !!"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="LnkBtn_Delete_OG_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                        OnClick="LnkBtn_Delete_Doc_Click" Visible="false" ToolTip="Click Here to Delete File !!"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                    <asp:HyperLink ID="Hyp_View_OG_Doc" runat="server" Target="_blank" Visible="false"
                                                        CssClass="input-group-addon bg-blue" ToolTip="Click Here to View File !!"><i class="fa fa-download"></i></asp:HyperLink>
                                                </div>
                                                <small class="text-danger">(.pdf file only and Max file size 4 MB)</small>
                                                <asp:Label ID="Lbl_Msg_OG_Doc" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                    runat="server" Text="Document Uploaded Successfully"></asp:Label>
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
                                                        OnClick="Btn_Submit_Click" OnClientClick="return validateOGMaster();" />
                                                    <asp:Button ID="Btn_Reset" runat="server" Text="Reset" class="btn btn-danger" OnClick="Btn_Reset_Click" />
                                                    <asp:HiddenField ID="Hid_OG_Id" runat="server" />
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Btn_Submit" />
                                <asp:PostBackTrigger ControlID="LnkBtn_Upload_OG_Doc" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                    </div>
                </div>
            </div>
            <%--</section>--%>
        </div>
    </div>
</asp:Content>
