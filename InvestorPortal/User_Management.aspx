<%--'*******************************************************************************************************************
' File Name         : User_Management.aspx
' Description       : User approval and set permission for child users.
' Created by        : Sushant Kumar Jena
' Created On        : 09-Aug-2018
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Management.aspx.cs"
    Inherits="InvestorPortal_User_Management" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/animate.css" rel="stylesheet" type="text/css" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('.menumanage').addClass('active');
        })

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function checkvalidation() {
            debugger;
            var checkedCheckboxes = $("#<%=GrdPermission.ClientID%> input[id*='ChkBx_Select_User']:checkbox:checked").size();
            if (checkedCheckboxes > 0) {
                jConfirm('Are you sure to provide permission ?', projname, function (callback) {
                    if (callback) {
                        __doPostBack("<%= Btn_Submit.UniqueID %>", "");

                    } else {
                        return false;
                    }
                });
                return false;
            }
            else {
                jAlert("<strong>Please select atleast one record to provide permission !!</strong>", projname);
                return false;
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
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:pealmenu ID="pealmenu" runat="server" />
                    </div>
                    <div class="form-sec">
                        <div class="form-header">
                            <h2>
                                 Assign Permission
                            </h2>
                        </div>
                        <div class="form-body">
                            <div class="table-responsive" id="divGrd" style="margin-top: 15px;">
                                <asp:GridView ID="GrdChildUser" runat="server" CssClass="table table-bordered table-striped bg-white"
                                    AutoGenerateColumns="false" OnRowDataBound="GrdChildUser_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SlNo">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_SlNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Investor Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Investor_Name" runat="server" Text='<%# Eval("VCH_INV_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site Location">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Site_Location" runat="server" Text='<%# Eval("VCH_SITELOCATION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Id">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_User_Id" runat="server" Text='<%# Eval("VCH_INV_USERID") %>'></asp:Label>
                                                <asp:HiddenField ID="Hid_Investor_Id" runat="server" Value='<%# Eval("INT_INVESTOR_ID") %>' />
                                                <asp:HiddenField ID="Hid_Parent_Id" runat="server" Value='<%# Eval("INT_PARENT_ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email Id">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Email_Id" runat="server" Text='<%# Eval("VCH_EMAIL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Mobile_No" runat="server" Text='<%# Eval("VCH_OFF_MOBILE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAN">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_PAN" runat="server" Text='<%# Eval("VCH_PAN") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="9%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EIN/IEM">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_EIN_IEM" runat="server" Text='<%# Eval("VCH_EIN_IEM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="Hid_Approval_Status" runat="server" Value='<%# Eval("INT_APPROVAL_STATUS") %>' />
                                                <asp:Label ID="Lbl_Approval_Status" runat="server" Text='<%# Eval("VCH_APPROVAL_STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="7%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="Btn_Action" runat="server" Text="Assign" Width="80px" OnClick="Btn_Action_Click" />
                                            </ItemTemplate>
                                            <ItemStyle Width="8%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No child user found !!
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <asp:HiddenField ID="Hid_Pop" runat="server" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                                TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="Btn_Close">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
                                <div id="undertakingipr2015">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-header bg-purpul">
                                                <h4 class="modal-title">
                                                    Assign permission to view other unit(s) details</h4>
                                            </div>
                                            <div class="modal-body">
                                                <p>
                                                    Permission to be given for below Units.
                                                </p>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-sm-2">
                                                            Unit Name</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_Investor_Name_Assign" runat="server" CssClass="form-control-static"></asp:Label>
                                                            <asp:HiddenField ID="Hid_Investor_Id_Assign" runat="server" />
                                                        </div>
                                                        <label class="col-sm-2">
                                                            User Id</label>
                                                        <div class="col-sm-4">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="Lbl_User_Id_Assign" runat="server" CssClass="form-control-static"
                                                                ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <p>
                                                    <hr />
                                                    Set Permission
                                                </p>
                                                <div class="table-responsive" id="div1" style="margin-top: 15px;">
                                                    <asp:GridView ID="GrdPermission" runat="server" CssClass="table table-bordered table-striped bg-white"
                                                        AutoGenerateColumns="false" OnRowDataBound="GrdPermission_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChkBx_Select_User" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SlNo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_SlNo_2" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Investor Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Investor_Name_2" runat="server" Text='<%# Eval("VCH_INV_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="User Id">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_User_Id_2" runat="server" Text='<%# Eval("VCH_INV_USERID") %>'></asp:Label>
                                                                    <asp:HiddenField ID="Hid_Investor_Id_2" runat="server" Value='<%# Eval("INT_INVESTOR_ID") %>' />
                                                                    <asp:HiddenField ID="Hid_Parent_Id_2" runat="server" Value='<%# Eval("INT_PARENT_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="13%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email Id">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Email_Id_2" runat="server" Text='<%# Eval("VCH_EMAIL") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15%" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Permission Type">
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList ID="RadBtn_Permission_Type" runat="server" RepeatDirection="Horizontal"
                                                                        CssClass="radio-inline">
                                                                        <asp:ListItem Value="1">Read</asp:ListItem>
                                                                        <asp:ListItem Value="2">Write</asp:ListItem>
                                                                        <asp:ListItem Value="3" Selected="True">Deny</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No approved other child unit(s) found !!
                                                        </EmptyDataTemplate>
                                                        <EmptyDataRowStyle ForeColor="Red" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:Button ID="Btn_Submit" runat="server" Text="Submit" OnClick="Btn_Submit_Click"
                                                            class="btn btn-success" ToolTip="Click Here to Proceed" OnClientClick="return checkvalidation();" />
                                                        <asp:Button ID="Btn_Close" runat="server" Text="Close" class="btn btn-danger" ToolTip="Click Here to Close Window" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
