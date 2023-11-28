<%--'*******************************************************************************************************************
' File Name         : ManageWeeklyMail.aspx
' Description       : Manage Weekly Mail
' Created by        : Sushant Kumar Jena
' Created On        : 30-Mar-2018
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ManageWeeklyMail.aspx.cs" Inherits="Portal_Dashboard_ManageWeeklyMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*---------------------------------------------------------*/

        function pageLoad() {
            CheckUncheckGrid();
        }

        /*---------------------------------------------------------*/
        function checkvalidation() {
            if (!DropDownValidation('ContentPlaceHolder1_DrpDwn_Enable_Status', '0', 'enable mail', projname)) {
                $("#popup_ok").click(function () { $("#ContentPlaceHolder1_DrpDwn_Enable_Status").focus(); });
                return false;
            }
            var checkedCheckboxes = $("#<%=GridView1.ClientID%> input[id*='chkSelectSingle']:checkbox:checked").size();
            if (checkedCheckboxes > 0) {
                jConfirm('Are you sure you want to update [?]', projname, function (callback) {
                    if (callback) {
                        __doPostBack("<%= Btn_Update.UniqueID %>", "");

                    } else {
                        return false;
                    }
                });
                return false;
            }
            else {
                jAlert("<strong>Please select atleast one record to update !!</strong>", projname);
                return false;
            }
        }

        /*---------------------------------------------------------*/

        function validateSendMail() {
            jConfirm('Are you sure you want to send mail [?]', projname, function (callback) {
                if (callback) {
                    __doPostBack("<%= Btn_Manual_Mail.UniqueID %>", "");

                } else {
                    return false;
                }
            });
            return false;
        }

        /*---------------------------------------------------------*/

        function validateMailAdd() {

            if (!blankFieldValidation('ContentPlaceHolder1_Txt_Mail_Id', 'Mail Id', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Mail_Id', 'Mail Id', projname)) {
                $("#popup_ok").click(function () { $("#ContentPlaceHolder1_Txt_Mail_Id").focus(); });
                return false;
            }
            if (!ValidateEmail($("#ContentPlaceHolder1_Txt_Mail_Id").val())) {
                jAlert('<strong>Please enter valid email address</strong>', projname);
                $("#popup_ok").click(function () { $("#ContentPlaceHolder1_Txt_Mail_Id").focus(); });
                return false;
            }
        }

        /*---------------------------------------------------------*/
    </script>
    <style type="text/css">
        .overlayContent
        {
            z-index: 99;
            margin: 250px auto;
            width: 90px;
            height: 90px;
        }
        .overlay
        {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #aaa;
            filter: alpha(opacity=50);
            opacity: 0.6;
        }
    </style>
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
                    Manage Weekly Mail</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Mail Configuration</a></li><li><a>Manage Weekly Mail</a></li>
                </ul>
            </div>
            <%--</section>--%>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ManageWeeklyMail.aspx"><i class="fa fa-plus"></i>Manage
                                    Weekly Mail </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ManageCcBccMail.aspx"><i class="fa fa-file"></i>Manage
                                    Cc & Bcc Mail </a>
                            </div>
                        </div>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <div class="overlay">
                                    <div class="overlayContent">
                                        <img alt="" src="../../images/basicloader.gif" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="panel-body">
                                    <div class="search-sec">
                                        <div class="form-group">
                                            <label class="col-md-2 col-sm-3">
                                                Designation</label>
                                            <div class="col-md-3 col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Designation" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-offset-1 col-md-2 col-sm-3">
                                            </label>
                                            <div class="col-md-3 col-sm-3">
                                                <asp:Button ID="Btn_Manual_Mail" runat="server" Text="Send Mail Manually" CssClass="btn btn-danger"
                                                    OnClick="Btn_Manual_Mail_Click" OnClientClick="return validateSendMail();" ToolTip="Click Here to Send Weekly Mail Manually !!" />
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 col-sm-3">
                                                Mail Status</label>
                                            <div class="col-md-3 col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Mail_Status" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-offset-1 col-md-2 col-sm-3">
                                            </label>
                                            <div class="col-md-3 col-sm-3">
                                                <asp:Button ID="Btn_View_Internal_Mail" runat="server" Text="Manage Internal Mail"
                                                    CssClass="btn btn-warning" OnClick="Btn_View_Internal_Mail_Click" ToolTip="Click Here to Manage Internal User !!" />
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 col-sm-3">
                                            </label>
                                            <div class="col-md-3 col-sm-3">
                                                <asp:Button ID="Btn_Search" runat="server" Text="Search" OnClick="Btn_Search_Click"
                                                    CssClass="btn btn-success" ToolTip="Click Here to View Users !!" />
                                            </div>
                                            <div class=" col-sm-7">
                                                <asp:Label ID="Lbl_Internal_Mail_Status" runat="server"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 col-sm-3">
                                            </label>
                                            <div class="col-md-3 col-sm-9">
                                                <asp:Label ID="Lbl_Msg" runat="server" CssClass="text-danger"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <asp:GridView ID="Grd_Mail_Config" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                            OnRowDataBound="Grd_Mail_Config_RowDataBound" OnRowCancelingEdit="Grd_Mail_Config_RowCancelingEdit"
                                            OnRowEditing="Grd_Mail_Config_RowEditing" OnRowUpdating="Grd_Mail_Config_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Spam Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Spam_Mode" runat="server" Text='<%# Eval("vchSpamMode") %>' Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="Btn_Update_Spam_Mode" runat="server" Text="" CssClass="btn btn-success"
                                                            OnClick="Btn_Update_Spam_Mode_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Spam Text">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Spam_Text" runat="server" Text='<%# Eval("vchSpamText") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="Txt_Spam_Text" runat="server" Text='<%# Eval("vchSpamText") %>'
                                                            CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="LnkBtn_Edit_Spam_Text" CommandName="Edit">Edit</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton runat="server" ID="LnkBtn_Update_Spam_Text" CommandName="update">Update</asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LnkBtn_Cancel_Spam_Text" CommandName="cancel">Cancel</asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemStyle Width="12%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div id="Div1" runat="server">
                                        <div class="search-sec bg-seagreen">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-md-2 col-sm-3">
                                                        Enable Mail</label>
                                                    <div class="col-md-3 col-sm-9">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="DrpDwn_Enable_Status" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                            <asp:ListItem Value="N">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                                GridLines="None" OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Check All" class="noPrint" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelectSingle" runat="server" ToolTip="Check" class="noPrint RowCheck" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SlNo">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="Hid_User_Id" runat="server" Value='<%# Eval("intUserId") %>' />
                                                            <asp:Label ID="Lbl_User_Name" runat="server" Text='<%# Eval("vchUserName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <%--<ItemStyle Width="17%" />--%>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mail Enable Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Mail_Status" runat="server" Text='<%# Eval("chToBeMail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cc Enable Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Cc_Enable_Status" runat="server" Text='<%# Eval("chCcEnableStatus") %>'></asp:Label>
                                                            <asp:HiddenField ID="Hid_Cc_Mail_Ids" runat="server" Value='<%# Eval("vchCcMailId") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bcc Enable Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Bcc_Enable_Status" runat="server" Text='<%# Eval("chBccEnableStatus") %>'></asp:Label>
                                                            <asp:HiddenField ID="Hid_Bcc_Mail_Ids" runat="server" Value='<%# Eval("vchBccMailId") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Full Name & Email Id">
                                                        <ItemTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <th width="20%">
                                                                        Name
                                                                    </th>
                                                                    <td width="2%">
                                                                        :
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_Full_Name" runat="server" Text='<%# Eval("vchFullName") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>
                                                                        Email Id
                                                                    </th>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_Email_Id" runat="server" Text='<%# Eval("vchEmail") %>' ForeColor="#49bed8"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <%-- <ItemStyle Width="25%" />--%>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Email Id">
                                                        <ItemTemplate>
                                                            
                                                        </ItemTemplate>
                                                         <ItemStyle Width="25%" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Domain Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Domain_Name" runat="server" Text='<%# Eval("vchDomainUName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-3 col-sm-9">
                                                    <asp:Button ID="Btn_Update" runat="server" Text="Update" CssClass="btn btn-warning"
                                                        OnClientClick="return checkvalidation();" OnClick="Btn_Update_Click" ToolTip="Click Here to Update Records !!" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                    <div id="DivInternalUser" runat="server">
                                        <div class="search-sec bg-seablue">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-md-2 col-sm-3">
                                                        Add New Mail Id</label>
                                                    <div class="col-md-3 col-sm-3">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="Txt_Mail_Id" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-3">
                                                        <asp:Button ID="Btn_Add" runat="server" Text="Add" CssClass="btn btn-success" OnClientClick="return validateMailAdd();"
                                                            OnClick="Btn_Add_Click" />
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="Grd_Internal_User" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                                GridLines="None" OnRowDataBound="Grd_Internal_User_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SlNo">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="Hid_Serial_No" runat="server" Value='<%# Eval("intSlNo") %>' />
                                                            <asp:Label ID="Lbl_Mail_Id" runat="server" Text='<%# Eval("vchMailId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Mail_Status_Internal" runat="server" Text='<%# Eval("chStatus") %>'
                                                                Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="Btn_Action" runat="server" OnClick="Btn_Action_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LnkBtn_Delete" CssClass="btn btn-danger btn-sm" OnClick="LnkBtn_Delete_Click"
                                                                runat="server" ToolTip="Remove" OnClientClick="return confirm('Are you sure you want to delete this mail [?]');"><i class="fa fa-trash"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div style="height: 20px; font-weight: bold; font-size: 13px; color: Red">
                                                        No Internal Mail Id(s) Found !!
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="form-footer">
                <div class="row">
                    <div class="col-sm-12">
                        <h4 style="color: #777;">
                            Notes:-</h4>
                        <div class="listdiv">
                            <ol>
                                <li>Mail will be sent to those users whose mail enables status is Yes <b>(Y)</b>.</li>
                                <li>If any <b>ACTIVE</b> internal mail id(s) is/are present then mail will be sent to
                                    internal mail id(s) only.</li>
                                <li>Cc and Bcc mail will be sent to those users whose respective enable status is Yes
                                    <b>(Y)</b> and respective mail id is not blank.</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
