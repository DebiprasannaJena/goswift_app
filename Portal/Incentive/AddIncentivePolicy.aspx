<%--'*******************************************************************************************************************
' File Name         : AddIncentivePolicy.aspx
' Description       : Add Incentive Policy
' Created by        : AMit Sahoo
' Created On        : 13 July 2017
' Modification History:

'      <CR no.>         <Date>                     <Modified by>           <Modification Summary>                  <Instructed By> 
        1             16-Feb-2018               Sushant Kumar Jena       Recoded as per master table            Chinmaya Kumar Samantsinghar                 

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AddIncentivePolicy.aspx.cs" Inherits="Master_AddIncentive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/custom.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <%--<section class="content-header">--%>
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Manage Incentive Policies</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Incentive Policy</a></li><li><a>Add Policy</a></li>
                </ul>
            </div>
            <%--</section>--%>
        </div>
        <div class="content">
            <%--<section class="content">--%>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="AddIncentivePolicy.aspx"><i class="fa fa-plus"></i>Add
                                </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ViewIncentivePolicy.aspx"><i class="fa fa-file"></i>View
                                </a>
                            </div>
                        </div>
                        <%--  <asp:UpdatePanel ID="udpDiv" runat="server">
                            <ContentTemplate>--%>
                        <div class="ibox-content">
                            <br />
                            <div class="form-group">
                                <label class="col-md-2 col-sm-3">
                                    Policy Code</label>
                                <div class="col-md-3 col-sm-3">
                                    <span class="colon">:</span>
                                    <asp:TextBox ID="Txt_Policy_Code" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                   <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" TargetControlID="Txt_Policy_Code"
                                        FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" ValidChars=",-/. ">
                                    </cc1:FilteredTextBoxExtender>--%>
                                    <%--  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender151t1" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                             TargetControlID="Txt_Policy_Code">
                                            </cc1:FilteredTextBoxExtender>--%>
                                    <span class="mandetory">*</span>
                                </div>
                                <label class="col-md-offset-1 col-md-2 col-sm-3">
                                    Policy Name</label>
                                <div class="col-md-3 col-sm-3">
                                    <span class="colon">:</span>
                                    <asp:TextBox ID="Txt_Policy_Name" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox>
                                    <%--  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" ValidChars="/-()\. "
                                                FilterMode="ValidChars" TargetControlID="Txt_Policy_Name">
                                            </cc1:FilteredTextBoxExtender>--%>
                                    <span class="mandetory">*</span>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 col-sm-3">
                                    Policy Effective Date
                                </label>
                                <div class="col-md-3 col-sm-3">
                                    <span class="colon">:</span>
                                    <div class="input-group date datePicker">
                                        <asp:TextBox ID="Txt_Policy_Effect_Date" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                    <span class="mandetory">*</span>
                                </div>
                                <label class="col-md-offset-1 col-md-2 col-sm-3">
                                    Policy Category
                                </label>
                                <div class="col-md-3 col-sm-3">
                                    <span class="colon">:</span>
                                    <asp:DropDownList ID="DrpDwn_Policy_Category" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Parental</asp:ListItem>
                                        <asp:ListItem Value="2">Sectoral</asp:ListItem>
                                        <asp:ListItem Value="3">Other</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="mandetory">*</span>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 col-sm-3">
                                    Sector Name
                                </label>
                                <div class="col-md-3 col-sm-3">
                                    <span class="colon">:</span>
                                    <asp:DropDownList ID="DrpDwn_Sector" CssClass="form-control" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="DrpDwn_Sector_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-offset-1 col-md-2 col-sm-3">
                                    Sub Sector Name
                                </label>
                                <div class="col-md-3 col-sm-3">
                                    <span class="colon">:</span>
                                    <asp:DropDownList ID="DrpDwn_Sub_Sector" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 col-sm-3">
                                    Upload Policy Document</label>
                                <div class="col-sm-6">
                                    <span class="colon">:</span>
                                    <div class="input-group">
                                        <asp:FileUpload ID="FU_Policy_Doc" CssClass="form-control" runat="server" onchange="return validateFile(this);"
                                            ToolTip="Browse File to Upload !!" />
                                        <asp:HiddenField ID="Hid_Policy_Doc_File_Name" runat="server" />
                                        <asp:LinkButton ID="LnkBtn_Upload_Policy_Doc" runat="server" CssClass="input-group-addon bg-green"
                                            OnClick="LnkBtn_Add_Doc_Click" ToolTip="Click Here to Upload File !!"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:LinkButton ID="LnkBtn_Delete_Policy_Doc" runat="server" CssClass="input-group-addon bg-red"
                                            OnClick="LnkBtn_Delete_Doc_Click" Visible="false" ToolTip="Click Here to Delete File !!"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:HyperLink ID="Hyp_View_Policy_Doc" runat="server" Target="_blank" Visible="false"
                                            CssClass="input-group-addon bg-blue" ToolTip="Click Here to View File !!"><i class="fa fa-download"></i></asp:HyperLink>
                                    </div>
                                    <small class="text-danger">(.pdf file only and Max file size 4 MB)</small>
                                    <asp:Label ID="Lbl_Msg_Policy_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                        Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 col-sm-3">
                                    Upload Policy Amendament Document</label>
                                <div class="col-sm-6">
                                    <span class="colon">:</span>
                                    <div class="input-group">
                                        <asp:FileUpload ID="FU_Amendment_Doc" CssClass="form-control" runat="server" onchange="return validateFile(this);"
                                            ToolTip="Browse File to Upload !!" />
                                        <asp:HiddenField ID="Hid_Amend_Doc_File_Name" runat="server" />
                                        <asp:LinkButton ID="LnkBtn_Upload_Amend_Doc" runat="server" CssClass="input-group-addon bg-green"
                                            OnClick="LnkBtn_Add_Doc_Click" ToolTip="Click Here to Upload File !!"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:LinkButton ID="LnkBtn_Delete_Amend_Doc" runat="server" CssClass="input-group-addon bg-red"
                                            OnClick="LnkBtn_Delete_Doc_Click" Visible="false" ToolTip="Click Here to Delete File !!"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:HyperLink ID="Hyp_View_Amend_Doc" runat="server" Target="_blank" Visible="false"
                                            CssClass="input-group-addon bg-blue" ToolTip="Click Here to View File !!"><i class="fa fa-download"></i></asp:HyperLink>
                                    </div>
                                    <small class="text-danger">(.pdf file only and Max file size 4 MB)</small>
                                    <asp:Label ID="Lbl_Msg_Amend_Doc" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                        runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 col-sm-3">
                                    Policy Description</label>
                                <div class=" col-sm-6">
                                    <span class="colon">:</span>
                                    <asp:TextBox ID="Txt_Description" Rows="5" onKeyUp="limitText(this,this.form.count,500);"
                                        TextMode="MultiLine" MaxLength="500" CssClass="form-control" runat="server" onKeyDown="limitText(this,this.form.count,500);"></asp:TextBox>
                                    <a href="#" data-toggle="tooltip" class="fieldinfo" title="It can accept both alphabets and numbers.Special Characters like !  # $ % & ( )  + , - . / : ; < = > ? @ [ \ ] are allowed">
                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><small>Maximum
                                            <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold;
                                                color: red; width: 26px;" type="text" value="500" />
                                            Characters Left</small>
                                            <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                        FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" ValidChars="!# $ % & ( ) + , - . / : ; < = > ? @ [ \ ]  "
                                                        FilterMode="ValidChars" TargetControlID="Txt_Description">
                                                    </cc1:FilteredTextBoxExtender>--%>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <table width="100%" class="table table-bordered">
                                        <tr>
                                            <td width="5%">
                                                Slno
                                            </td>
                                            <td width="20%">
                                                Policy Section No.
                                            </td>
                                            <td>
                                                Policy Section Name
                                            </td>
                                            <td width="10%">
                                                Add More
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                -
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Policy_Section" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_Policy_Sec_Name" CssClass="form-control" MaxLength="250" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LnkBtn_Add_Section" CssClass="btn btn-success btn-sm" runat="server"
                                                    OnClick="LnkBtn_Add_Section_Click" OnClientClick="return validateSection();"
                                                    ToolTip="Click Here to Add"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="Grd_Section" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                        ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Section_No" runat="server" Text='<%# Eval("vchSectionNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="25%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Section_Name" runat="server" Text='<%# Eval("vchSectionName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgBtn_Delete_Section" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                        CommandArgument='<%# Container.DataItemIndex %>' OnClick="ImgBtn_Delete_Section_Click"
                                                        ToolTip="Click Here to Remove" />
                                                </ItemTemplate>
                                                <ItemStyle Width="10%"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-4 ">
                                    <asp:Button ID="Btn_Submit" runat="server" Text="Submit" class="btn btn-primary"
                                        OnClientClick="return validatePolicy();" OnClick="Btn_Submit_Click" />
                                    <asp:Button ID="Btn_Reset" runat="server" Text="Reset" class="btn btn-danger" OnClick="Btn_Reset_Click" />
                                    <asp:HiddenField ID="hidSubmitFlag" runat="server" Value="0" />
                                </div>
                                <div class="clearfix ">
                                </div>
                            </div>
                        </div>
                        <br />
                        <%-- </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Btn_Submit" />
                                <asp:PostBackTrigger ControlID="LnkBtn_Upload_Policy_Doc" />
                                <asp:PostBackTrigger ControlID="LnkBtn_Upload_Amend_Doc" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
            <%--</section>--%>
        </div>
    </div>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function pageLoad() {
            var txtDesc = $("#ContentPlaceHolder1_Txt_Description").val().length;
            $('.inputCss').val(500 - txtDesc);

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
                autoclose: true
            });
        }

        /*--------------------------------------------------------------*/

        function validatePolicy() {

            if (!blankFieldValidation('ContentPlaceHolder1_Txt_Policy_Code', 'Poilcy code', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Policy_Code', 'Poilcy code', projname)) {
                $("#popup_ok").click(function () { $("#ContentPlaceHolder1_Txt_Policy_Code").focus(); });
                return false;
            }
            if (!blankFieldValidation('ContentPlaceHolder1_Txt_Policy_Name', 'Policy name', projname)) {
                $("#Txt_Policy_Name").focus();
                return false;
            };
            if (!WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Policy_Name', 'Poilcy name', projname)) {
                $("#ContentPlaceHolder1_Txt_Policy_Name").focus();
                return false;
            };
            if (!blankFieldValidation('ContentPlaceHolder1_Txt_Policy_Effect_Date', 'Policy effective date', projname)) {
                $("#ContentPlaceHolder1_Txt_Policy_Effect_Date").focus();
                return false;
            };
            if (!DropDownValidation('ContentPlaceHolder1_DrpDwn_Policy_Category', '0', 'Policy category', projname)) {
                $("#popup_ok").click(function () { $("#ContentPlaceHolder1_DrpDwn_Policy_Category").focus(); });
                return false;
            }

            if ($('#ContentPlaceHolder1_DrpDwn_Sector').val() > 0) {
                if (!DropDownValidation('ContentPlaceHolder1_DrpDwn_Sub_Sector', '0', 'sub sector', projname)) {
                    $("#popup_ok").click(function () { $("#ContentPlaceHolder1_DrpDwn_Sub_Sector").focus(); });
                    return false;
                }
            }
            if ($('#ContentPlaceHolder1_DrpDwn_Sub_Sector').val() > 0) {
                if (!DropDownValidation('ContentPlaceHolder1_DrpDwn_Sector', '0', 'sector', projname)) {
                    $("#popup_ok").click(function () { $("#ContentPlaceHolder1_DrpDwn_Sector").focus(); });
                    return false;
                }
            }

            if ($('#ContentPlaceHolder1_Hid_Policy_Doc_File_Name').val() == '') {
                jAlert('<strong>Please Upload Policy Document !!</strong>', projname);
                $("#popup_ok").click(function () { $("#ContentPlaceHolder1_FU_Policy_Doc").focus(); });
                return false;
            }

            if (!blankFieldValidation('ContentPlaceHolder1_Txt_Description', 'Poilcy description', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Description', 'poilcy description', projname)) {
                $("#popup_ok").click(function () { $("#ContentPlaceHolder1_Txt_Description").focus(); });
                return false;
            }

            if ($("#ContentPlaceHolder1_Grd_Section tr").length > 0) {
            }
            else {
                jAlert('<strong>Please Insert Atleast One Record for Policy Section !!</strong>', projname);
                return false;
            }
        }

        /*--------------------------------------------------------------*/

        function validateSection() {

            if (!blankFieldValidation('ContentPlaceHolder1_Txt_Policy_Section', 'Poilcy section', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Policy_Section', 'policy section', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Policy_Section").focus(); });
                return false;
            }

            if (!blankFieldValidation('ContentPlaceHolder1_Txt_Policy_Sec_Name', 'Poilcy section name', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Policy_Sec_Name', 'policy section name', projname)) {
                $("#popup_ok").click(function () { $("#ContentPlaceHolder1_Txt_Policy_Sec_Name").focus(); });
                return false;
            }
        }

        /*--------------------------------------------------------------*/

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }

        /*--------------------------------------------------------------*/
        ////// Alert and Redirect
        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    var linkm = '<%=this.Request.QueryString["linkm"]%>';
                    var linkn = '<%=this.Request.QueryString["linkn"]%>';
                    var btn = '<%=this.Request.QueryString["btn"]%>';
                    var tab = '<%=this.Request.QueryString["tab"]%>';
                    var RandomNo = '<%=this.Session["RandomNo"]%>';

                    location.href = 'ViewIncentivePolicy.aspx?linkm=' + linkm + '&linkn=' + linkn + '&btn=' + btn + '&tab=' + tab + '&ranNum=' + RandomNo + '';
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        /*--------------------------------------------------------------*/

        //        function ConfirmAction(cntr) {
        //            var strValue = $('#' + cntr).val();
        //            if (strValue == 'Update') {

        //                if (!confirm('Are You Sure To Update?')) {
        //                    return false;
        //                }
        //                else {
        //                    return true;
        //                }
        //            }
        //            else {
        //                if (!confirm('Are You Sure To Save?')) {
        //                    return false;
        //                }
        //                else {
        //                    return true;
        //                }
        //            }
        //        }

        function TestFunction(str) {
            alert('hi');
            alert(str);
        }
    </script>
</asp:Content>
