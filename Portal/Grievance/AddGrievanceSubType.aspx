<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="AddGrievanceSubType.aspx.cs" Inherits="Portal_Grievance_AddGrievanceSubType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>

    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validateGrievanceSubtype() {

            if (DropDownValidation('ContentPlaceHolder1_ddl_griv_type', '0', 'grievance type', projname) == false) {
                $("#ContentPlaceHolder1_ddl_griv_type").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_Txt_Griv_subtypeName', 'Grievance subtype', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Griv_Name").focus();
                return false;
            };
            if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Griv_subtypeName', 'Grievance subtype', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Griv_Name").focus();
                return false;
            };
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
                <h1>Add Grievance Subtype</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Grievance</a></li>
                    <li><a>Add Grievance Subtype</a></li>
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

                        <asp:UpdatePanel ID="udpDiv" runat="server">
                            <ContentTemplate>
                                <div class="ibox-content">
                                   
                                    <div class="clearfix">
                                    </div>
                                       <div class="search-sec">


                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            Grievance Type <span class="mandetory-y">*</span></label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="ddl_griv_type" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_griv_type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                          
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            Grievance Subtype <span class="mandetory-y">*</span></label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Griv_subtypeName" runat="server" CssClass="form-control"></asp:TextBox>
                                            
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            Active Status <span class="mandetory-y">*</span></label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:RadioButtonList ID="Rbl_griv_status" runat="server" CssClass="radio-box"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem class="radio-inline" Value="1" Selected="True">Active</asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="2">InActive</asp:ListItem>
                                            </asp:RadioButtonList>
                                            
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-4">
                                            <span class="colon">&nbsp;</span>
                                            <asp:Button ID="Btn_Submit" runat="server" Text="Submit" class="btn btn-primary"
                                                OnClick="Btn_Submit_Click" OnClientClick="return validateGrievanceSubtype();" />
                                            <asp:Button ID="Btn_Reset" runat="server" Text="Reset" class="btn btn-danger" OnClick="Btn_Reset_Click" />
                                            <asp:HiddenField ID="Hid_OG_Id" runat="server" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                           </div>
                                </div>

                                <div class="table-responsive">
                                        

                                        <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover" OnRowDataBound="GridView1_RowDataBound"
                                            AutoGenerateColumns="False"    Width="100%" EmptyDataText="No Record(s) Found..." >
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
                                                

                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                    </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Btn_Submit" />
                            </Triggers>
                        </asp:UpdatePanel>
                              </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
