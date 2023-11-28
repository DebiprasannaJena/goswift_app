<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncentiveLateReason.aspx.cs"
    Inherits="incentives_IncentiveLateReason" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="investerMenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/WebValidation.js"> </script>
    <script type="text/javascript" language="javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validateFile(e) {
            var ids = e.id;
            var fileExtension = ['pdf', 'zip', 'xls', 'doc', 'docx', 'xlsx'];
            if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                jAlert('<strong>Only .pdf/.zip/.xls/.doc/.docx/.xlsx formats are allowed.</strong>', projname);
                $("#" + ids).val(null);
                return false;
            }
            else {
                if ((e.files[0].size > parseInt(4 * 1024 * 1024)) && ($("#" + ids).val() != '')) {

                    jAlert('<strong>File size must be less then 4 MB !! </strong>', projname);
                    $("#" + ids).val(null);
                    //e.preventDefault();
                    return false;
                }
            }
        }

        /*--------------------------------------------------------------------*/

        function validateFileAdd() {
            if (!blankFieldValidation('Txt_Description', ' Description', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_Description', 'Description', projname)) {
                return false;
            }
            if ($('#FileUpload_Document').val() == '') {
                jAlert('<strong>Please Upload Document !!</strong>', projname);
                $('#Txt_Description').focus();
                return false;
            }
        }

        /*--------------------------------------------------------------------*/

        function ValidatePage() {
            if (!blankFieldValidation('Txt_Delay_Reason', 'Reason(s) for Delay in Implementation', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_Delay_Reason', 'Reason(s) for Delay in Implementation', projname)) {
                return false;
            }
            if ($("#Grd_Files tr").length > 0) {
            }
            else {
                jAlert('<strong>Please Add Atleast One Supporting Document !!</strong>', projname);
                return false;
            }

            //            jConfirm('Are You Sure You Want To Proceed?', projname, function (callback) {
            //                if (callback) {
            //                    __doPostBack('Btn_Submit', '');
            //                } else {
            //                    return false;
            //                }
            //            });
        }

        /*--------------------------------------------------------------------*/

        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {

                    location.href = 'incentiveoffered.aspx?';
                    return true;
                }
                else {
                    return false;
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="Btn_Submit" defaultfocus="Txt_Delay_Reason">
    <uc2:header ID="header" runat="server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="registration-div investors-bg">
                <div id="exTab1" class="container">
                    <div class="investrs-tab">
                        <uc4:investerMenu ID="objInvestorMenu" runat="server" />
                    </div>
                    <div class="tab-content clearfix">
                        <div class="form-sec">
                            <div class="form-header">
                                <h4 class="h4-header" style="text-align: center; font-weight: bold;">
                                    Application for Condonation of Delay in Implementation</h4>
                            </div>
                            <div class="form-body">
                                <div class="form-group">
                                    <div class="row">
                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                            Industry Code</label>
                                        <div class="col-sm-6">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Industry_Code" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                            Name of Enterprise/Industrial Unit
                                        </label>
                                        <div class="col-sm-6">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Enterprise_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                            Unit Category
                                        </label>
                                        <div class="col-sm-6">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Unit_Category" runat="server" CssClass="form-control-static"></asp:Label>
                                            <asp:HiddenField ID="Hid_Unit_Category" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                            Unit Type
                                        </label>
                                        <div class="col-sm-6">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Unit_Type" runat="server" CssClass="form-control-static"></asp:Label>
                                            <asp:HiddenField ID="Hid_Unit_Type" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                            Date of FFCI
                                        </label>
                                        <div class="col-sm-6">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_FFCI_Date" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                            Date of Production Commencement
                                        </label>
                                        <div class="col-sm-6">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Prod_Comm_Date" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                            Reason(s) for Delay in Implementation <span style="color: Red;">*</span></label>
                                        <div class="col-sm-6">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Delay_Reason" Rows="5" onKeyUp="limitText(this,this.form.count,500);"
                                                TextMode="MultiLine" MaxLength="500" CssClass="form-control" runat="server" onKeyDown="limitText(this,this.form.count,500);"></asp:TextBox>
                                            <small>Maximum
                                                <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold;
                                                    color: red; width: 26px;" type="text" value="500" />
                                                Characters Left</small>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTxtExt_Dealy_Reason" runat="server" Enabled="True"
                                                FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" ValidChars="!# $ % & ( ) + , - . / : ; = ? @ [ \ ]  "
                                                FilterMode="ValidChars" TargetControlID="Txt_Delay_Reason">
                                            </cc1:FilteredTextBoxExtender>
                                            <a href="#" data-toggle="tooltip" class="fieldinfo" title="It can accept both alphabets and numbers.Special Characters like !  # $ % & ( )  + , - . / : ; = ? @ [ \ ] are allowed">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-12 col-sm-offset-1">
                                            Upload Document(s) In Support Of Delay <span style="color: Red;">*</span>
                                        </label>
                                        <div class="col-sm-10 col-sm-offset-1">
                                            <table class="table table-bordered">
                                                <tr>
                                                    <th width="7%">
                                                        SlNo.
                                                    </th>
                                                    <th>
                                                        Document Description
                                                    </th>
                                                    <th width="35%">
                                                        Upload Document
                                                    </th>
                                                    <th width="8%" style="text-align: center;">
                                                        Add
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        -
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Txt_Description" runat="server" CssClass="form-control" MaxLength="500"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="FileUpload_Document" runat="server" CssClass="form-control" onchange="return validateFile(this);" />
                                                        <small class="text-danger">(.pdf/.xls/.xlsx/.doc/.docx/.zip file only and Max file size
                                                            4 MB)</small>
                                                    </td>
                                                    <td align="center">
                                                        <asp:LinkButton ID="LnkBtn_Add_Doc" CssClass="btn btn-success btn-sm" runat="server"
                                                            OnClick="LnkBtn_Add_Doc_Click" OnClientClick="return validateFileAdd();" ToolTip="Click Here to Add Documents !!"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="Grd_Files" runat="server" CssClass="table table-bordered" DataKeyNames="vchDocDesc"
                                                AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="Grd_Files_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Doc_Desc" runat="server" Text='<%# Eval("vchDocDesc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="Hyp_View_Doc" runat="server" Target="_blank" ToolTip="Click Here to View Uploaded Document !!">View Document</asp:HyperLink>
                                                            <asp:HiddenField ID="Hid_File_Name" runat="server" Value='<%# Eval("vchFileName") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="35%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgBtn_Delete" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                CommandArgument='<%# Container.DataItemIndex %>' OnClick="ImgBtn_Delete_Click"
                                                                ToolTip="Click Here to Remove" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-footer" style="text-align: center;">
                        <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn btn-success"
                            OnClientClick="return ValidatePage()" OnClick="Btn_Submit_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LnkBtn_Add_Doc" />
        </Triggers>
    </asp:UpdatePanel>
    <uc3:footer ID="footer" runat="server" />
    </form>
    <style type="text/css">
        .collapse.in
        {
            display: block;
            height: auto !important;
        }
    </style>
    <script type="text/javascript" language="jscript">

        function pageLoad() {
            var txtDesc = $("#Txt_Delay_Reason").val().length;
            $('.inputCss').val(500 - txtDesc);
        }
        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }
    </script>
</body>
</html>
