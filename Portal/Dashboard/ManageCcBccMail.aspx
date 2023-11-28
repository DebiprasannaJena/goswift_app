<%--'*******************************************************************************************************************
' File Name         : ManageCcBccMail.aspx
' Description       : Configure Cc and Bcc Mail for Weekly Tracker
' Created by        : Sushant Kumar Jena
' Created On        : 30-Mar-2018
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ManageCcBccMail.aspx.cs" Inherits="Portal_Dashboard_ManageCcBccMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validateCcBccUpdate() {

            var cc = $('#ContentPlaceHolder1_DrpDwn_Cc_Enable_Status').val();
            if (cc == 'Y') {
                if (!blankFieldValidation('ContentPlaceHolder1_Txt_Cc_Mail_Id', 'Cc mail id(s)', projname)) {
                    return false;
                }
            }

            var bcc = $('#ContentPlaceHolder1_DrpDwn_Bcc_Enable_Status').val();
            if (bcc == 'Y') {
                if (!blankFieldValidation('ContentPlaceHolder1_Txt_Bcc_Mail_Id', 'Bcc mail id(s)', projname)) {
                    return false;
                }
            }

            jConfirm('Are you sure you want to update Cc and Bcc mail [?]', projname, function (callback) {
                if (callback) {
                    __doPostBack("<%= Btn_Update.UniqueID %>", "");

                } else {
                    return false;
                }
            });
            return false;
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <div class="content-header">
                    <%--<section class="content-header">--%>
                    <div class="header-icon">
                        <i class="fa fa-dashboard"></i>
                    </div>
                    <div class="header-title">
                        <h1>Manage Cc and Bcc Mail</h1>
                        <ul class="breadcrumb">
                            <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                            <li><a>Mail Configuration</a></li>
                            <li><a>Manage Cc and Bcc Mail</a></li>
                        </ul>
                    </div>
                    <%--</section>--%>
                </div>
                <%-- <span style="padding-left: 2px; font-family: Cambria Math; border-color: Gray;">
                </span>--%>
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
                                <div class="panel-body">
                                    <div class="search-sec">
                                        <div class="form-group">
                                            <label class="col-md-2 col-sm-3">
                                                Select Designation</label>
                                            <div class="col-md-3 col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Designation" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-md-offset-1 col-md-2 col-sm-3">
                                                Enter Test Mail Id
                                            </label>
                                            <div class="col-md-3 col-sm-3">
                                                <asp:TextBox ID="Txt_Test_Mail_Id" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                <asp:Button ID="Btn_Test_Mail" runat="server" Text="Test Mail" CssClass="btn btn-primary"
                                                    OnClick="Btn_Test_Mail_Click" OnClientClick="return confirm ('Are you sure');" />
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
                                            <label class="col-md-offset-1 col-md-2 col-sm-3">
                                            </label>
                                            <div class="col-md-3 col-sm-3">
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
                                    <div id="Div1" runat="server">

                                        <table style="width: 100%;" class="table table-bordered table-hover">
                                            <tr>
                                                <td style="width: 49%; background-color: #e1e6ef; padding: 8px;">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <div class="row">
                                                                        <label class="col-md-3">Cc Mail Id(s)</label>
                                                                        <div class="col-md-8">
                                                                            <span class="colon">:</span>
                                                                            <asp:TextBox ID="Txt_Cc_Mail_Id" runat="server" CssClass="form-control" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                                                                            <small class="text-danger">[To enter multiple mail ids use comma(,)]</small>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <div class="row">
                                                                        <label class="col-md-3">
                                                                            Cc Enable Status
                                                                        </label>
                                                                        <div class="col-md-8">
                                                                            <span class="colon">:</span>
                                                                            <asp:DropDownList ID="DrpDwn_Cc_Enable_Status" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <div class="row">
                                                                        <div class="col-md-3"></div>
                                                                        <div class="col-md-9">
                                                                            <asp:Button ID="Btn_Update" runat="server" Text="Update All Cc Details" CssClass="btn btn-danger" OnClick="Btn_Update_Click" ToolTip="Click Here to Update Cc Records !!" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 1%"></td>
                                                <td style="width: 49%; background-color: #b0c9c9; padding: 8px;">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <div class="row">
                                                                        <label class=" col-md-3">
                                                                            Bcc Mail Id(s)
                                                                        </label>
                                                                        <div class="col-md-8">
                                                                            <span class="colon">:</span>
                                                                            <asp:TextBox ID="Txt_Bcc_Mail_Id" runat="server" CssClass="form-control" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                                                                            <small class="text-danger">[To enter multiple mail ids use comma(,)]</small>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <div class="row">
                                                                        <label class=" col-md-3">
                                                                            Bcc Enable Status
                                                                        </label>
                                                                        <div class="col-md-8">
                                                                            <span class="colon">:</span>
                                                                            <asp:DropDownList ID="DrpDwn_Bcc_Enable_Status" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <div class="row">
                                                                        <div class="col-md-3"></div>
                                                                        <div class="col-md-3">
                                                                            <asp:Button ID="Btn_UpdateBcc" runat="server" Text="Update All Bcc Details" CssClass="btn btn-danger" OnClick="Btn_UpdateBcc_Click" ToolTip="Click Here to Update Bcc Records !!" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>







                                        <%--              <div class="search-sec bg-darkorange">
                                            <div class="form-group">
                                                <div class="row">
                                                    
                                                  
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                            </div>
                                            
                                           
                                        </div>--%>


                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                                GridLines="None" OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SlNo">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="Hid_User_Id" runat="server" Value='<%# Eval("intUserId") %>' />
                                                            <asp:HiddenField ID="Hid_Designation_Id" runat="server" Value='<%# Eval("intDesignationId") %>' />
                                                            <table width="100%">
                                                                <tr>
                                                                    <th width="25%">User
                                                                    </th>
                                                                    <td width="2%">:
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_User_Name" runat="server" Text='<%# Eval("vchUserName") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Email Id
                                                                    </th>
                                                                    <td>:
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Lbl_Email_Id" runat="server" Text='<%# Eval("vchEmail") %>' ForeColor="#49bed8"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Email Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Email_Id" runat="server" Text='<%# Eval("vchEmail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Mail Enable Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Mail_Status" runat="server" Text='<%# Eval("chToBeMail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cc Enable Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Cc_Enable_Status" runat="server" Text='<%# Eval("chCcEnableStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>

                                                            <asp:DropDownList ID="DrpDwn_Cc_Enable_Sta" runat="server" CssClass="form-control" SelectedValue='<%# Bind("chCcEnableStatus") %>'>
                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                <asp:ListItem Value="N">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>

                                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bcc Enable Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Bcc_Enable_Status" runat="server" Text='<%# Eval("chBccEnableStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>

                                                            <asp:DropDownList ID="DrpDwn_Bcc_Enable_Status" runat="server" CssClass="form-control" SelectedValue='<%# Eval("chBccEnableStatus") %>'>
                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                <asp:ListItem Value="N">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cc Mail Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Cc_Mail_Id" runat="server" Text='<%# Eval("vchCcMailId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="Txt_Cc_Mail_Id" runat="server" Text='<%# Eval("vchCcMailId") %>'
                                                                CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bcc Mail Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Bcc_Mail_Id" runat="server" Text='<%# Eval("vchBccMailId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="Txt_Bcc_Mail_Id" runat="server" Text='<%# Eval("vchBccMailId") %>'
                                                                CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" ID="Btn_Edit_Spam_Text" CommandName="Edit" Text="Edit" CssClass="btn-exp" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Button runat="server" ID="Btn_Update_Spam_Text" CommandName="update" Text="Update" CssClass="btn-success" />
                                                            <asp:Button runat="server" ID="Btn_Cancel_Spam_Text" CommandName="cancel" Text="Cancel" CssClass="btn-danger" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
