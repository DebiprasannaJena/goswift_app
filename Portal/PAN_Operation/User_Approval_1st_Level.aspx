<%@ Page Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true"
    CodeFile="User_Approval_1st_Level.aspx.cs" Inherits="Portal_PAN_Operation_User_Approval_1st_Level" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*----------------------------------------------------------------*/

        function confirmApproval(LnkBtn_Approve) {
            debugger;
            var ss = LnkBtn_Approve.id;
            var pp = $('#' + ss).attr('href');
            jConfirm('Are you sure to approve this user ?', projname, function (callback) {
                if (callback) {
                    location.href = pp;
                    return true;

                } else {
                    return false;
                }
            });
            return false;
        }

        /*----------------------------------------------------------------*/

        function confirmReject(LnkBtn_Reject) {
            //debugger;
            var ss = LnkBtn_Reject.id;
            var pp = $('#' + ss).attr('href');
            jConfirm('Are you sure to reject this user ?', projname, function (callback) {
                if (callback) {
                    location.href = pp;
                    return true;

                } else {
                    return false;
                }
            });
            return false;
        }

        /*----------------------------------------------------------------*/

        function checkvalidation() {
            debugger;
            if (blankFieldValidation('ContentPlaceHolder1_Txt_Rejection_Cause', 'Rejection cause', projname) == false) {
                return false;
            }
        }

        /*----------------------------------------------------------------*/

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }

    </script>
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 900px;
            height: 550px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-tachometer"></i>
            </div>
            <div class="header-title">
                <h1>
                    1st Level User Approval (For MSME)</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Manage User</a></li><li><a>Approve User</a></li>
                </ul>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-success" href="User_Approval_1st_Level.aspx"><i class="fa fa-file">
                                </i>1st Level </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="User_Approval.aspx"><i class="fa fa-file"></i>Final Level
                                </a>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="search-sec">
                                <div class="form-group">
                                    <label class="col-sm-2">
                                        PAN</label>
                                    <div class="col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_PAN" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-2">
                                        Unit Name
                                    </label>
                                    <div class="col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_Unit_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btn btn-success"
                                            OnClick="Btn_Search_Click" ToolTip="Click Here to View Users !!" />
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="GrdChildUser" runat="server" CssClass="table table-bordered table-striped bg-white"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SlNo">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_SlNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkBtn_Inv_Name" runat="server" ToolTip="Click here to view details !!"
                                                    OnClick="LnkBtn_Inv_Name_Click" Text='<%# Eval("VCH_INV_NAME")%>'></asp:LinkButton>
                                                <%-- <asp:Label ID="Lbl_Investor_Name" runat="server" Text='<%# Eval("VCH_INV_NAME") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site Location">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Site_Location" runat="server" Text='<%# Eval("VCH_SITELOCATION") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Id">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_User_Id" runat="server" Text='<%# Eval("VCH_INV_USERID") %>'></asp:Label>
                                                <asp:HiddenField ID="Hid_Investor_Id" runat="server" Value='<%# Eval("INT_INVESTOR_ID") %>' />
                                                <asp:HiddenField ID="Hid_Parent_Id" runat="server" Value='<%# Eval("INT_PARENT_ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Details">
                                            <ItemTemplate>
                                                <table width="99%">
                                                    <tr>
                                                        <td width="25%">
                                                            Email Id
                                                        </td>
                                                        <td width="3%">
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Lbl_Email_Id" runat="server" Text='<%# Eval("VCH_EMAIL") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Mobile No
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Lbl_Mobile_No" runat="server" Text='<%# Eval("VCH_OFF_MOBILE") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PAN
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Lbl_PAN" runat="server" Text='<%# Eval("VCH_PAN") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            EIN/IEM
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Lbl_EIN_IEM" runat="server" Text='<%# Eval("VCH_EIN_IEM") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Regd. Date
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Lbl_Regd_Date" runat="server" Text='<%# Eval("DTM_CREATED_ON" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="Hid_Approval_Status" runat="server" Value='<%# Eval("INT_APPROVAL_STATUS") %>' />
                                                <asp:Label ID="Lbl_Approval_Status" runat="server" Text='<%# Eval("VCH_APPROVAL_STATUS") %>'
                                                    ForeColor="Red"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="7%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkBtn_Approve" runat="server" OnClick="LnkBtn_Approve_Click"
                                                    OnClientClick="return confirmApproval(this);" CssClass="btn btn-success" ToolTip="Click Here to Approve this User !!">Approve</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="7%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reject">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkBtn_Reject" runat="server" OnClick="LnkBtn_Reject_Click" OnClientClick="return confirmReject(this);"
                                                    CssClass="btn btn-danger" ToolTip="Click Here to Reject this User !!">Reject</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="7%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No investor found for approval !!
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="Hid_Pop" runat="server" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
            TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="Btn_Cancel">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
            <div id="undertakingipr2015">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header bg-purpul">
                            <h4 class="modal-title">
                                Reject Unit</h4>
                        </div>
                        <div class="modal-body">
                            <h4>
                                Unit Details.</h4>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Unit Name</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Investor_Name_Reject" runat="server" CssClass="form-control-static"
                                            Font-Bold="true"></asp:Label>
                                        <asp:HiddenField ID="Hid_Investor_Id_Reject" runat="server" />
                                    </div>
                                    <label class="col-sm-2">
                                        User Id</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_User_Id_Reject" runat="server" CssClass="form-control-static"
                                            Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Rejection Cause</label>
                                    <div class="col-sm-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_Rejection_Cause" runat="server" TextMode="MultiLine" MaxLength="250"
                                            CssClass="form-control" onKeyUp="limitText(this,this.form.count,250);" onKeyDown="limitText(this,this.form.count,250);"></asp:TextBox>
                                        <small>Maximum
                                            <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold;
                                                color: red; width: 26px;" type="text" value="250" />
                                            Characters Left</small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-sm-9" style="text-align: left;">
                                    <span style="color: Red; font-family: Verdana;">N.B.:- If you reject this unit,then
                                        It will be no longer available.</span>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="Btn_Submit" runat="server" Text="Submit" OnClick="Btn_Submit_Click"
                                        class="btn btn-success" ToolTip="Click Here to Proceed" OnClientClick="return checkvalidation();" />
                                    <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" class="btn btn-danger" ToolTip="Click Here to Cancel the Rejection !!" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
