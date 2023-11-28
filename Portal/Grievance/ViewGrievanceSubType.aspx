<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="ViewGrievanceSubType.aspx.cs" Inherits="Portal_Grievance_ViewGrievanceSubType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validateGrievanceSubtype() {

            if (blankFieldValidation('ContentPlaceHolder1_Txt_Griv_Name', 'Grievance Subtype Name', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Griv_Name").focus();
                return false;
            };
            if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Griv_Name', 'Grievance Subtype Name', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Griv_Name").focus();
                return false;
            };
            if (DropDownValidation('ddl_griv_type', '0', 'Grievance Type', projname) == false) {
                $("#popup_ok").click(function () { $("#ddlGrievanceType").focus(); });
                return false;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>View and Modify Grievance Subtype</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Grievance</a></li>
                    <li><a>View Grievance Subtype</a></li>
                </ul>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="AddGrievanceSubType.aspx"><i class="fa fa-plus"></i>Add
                                </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ViewGrievanceSubType.aspx"><i class="fa fa-file"></i>View
                                </a>
                            </div>
                        </div>

                        <div class="panel-body">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                                    <div class="search-sec">

                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Grievance Type</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="ddl_griv_type" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <span class="mandetory">*</span>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">&nbsp;</span>
                                                    <asp:Button ID="Btn_Search" runat="server" Text="Search" class="btn btn-primary" OnClick="Btn_Search_Click"
                                                        OnClientClick="return validateGrievanceSubtype();" />
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="table-responsive">
                                        <div align="right">
                                            <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                                OnClick="lbtnAll_Click"></asp:LinkButton>
                                            <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                        </div>

                                        <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" EmptyDataText="No Record(s) Found..."
                                            DataKeyNames="intGrivSubTypeId" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="3%"></ItemStyle>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Grievance Type" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="Hid_Griev_Type" runat="server" Value='<%# Eval("intGrivTypeId") %>' />
                                                        <asp:Label ID="Lbl_Griev_Type" runat="server" Text='<%# Eval("vchGrivName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grievance Sub Type" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="Hid_Griev_SubType" runat="server" Value='<%# Eval("intGrivSubTypeId") %>' />
                                                        <asp:Label ID="Lbl_Griev_Sub_Type" runat="server" Text='<%# Eval("vchGrivSubType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Active Status" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="Hid_Griev_SubType_Status" runat="server" Value='<%# Eval("intGrivActiveStatus") %>' />
                                                        <asp:Label ID="Lbl_Griv_stat" runat="server" Text='<%# Eval("intGrivActiveStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btn_griv_edit" runat="server" Text="Edit" CommandArgument='<%# Eval("intGrivSubTypeId") %>'
                                                            OnClick="btn_griv_edit_Click" CssClass="btn btn-success btn-sm" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

