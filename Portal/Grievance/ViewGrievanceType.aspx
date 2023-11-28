<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewGrievanceType.aspx.cs" Inherits="Portal_Grievance_ViewGrievanceType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/decimalrstr.js" type="text/javascript"></script>
    <style></style>
   
    <style>
        .control-label
        {
            border: 1px solid #b9bdbf;
            padding: 6px 10px;
            border-radius: 2px;
            height: 31px;
            width: 100%;
            background: #f9f9f9;
            display: block;
            margin: 0px;
        }
    </style>
    <style type="text/css" media="all">
        /* fix rtl for demo */
        .chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        
        .chosen-container .chosen-container-single .chosen-single
        {
            width: 100% !important;
        }
        
        .searchbox
        {
            background-color: #def3ff;
            padding: 8px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    View and Modify Grievance Type</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Grievance</a></li>
                    <li><a>View Grievance Type</a></li>
                </ul>
            </div>
        </div>
        <!-- Main content -->
        <div class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="AddGrievanceType.aspx"><i class="fa fa-plus"></i>Add</a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ViewGrievanceType.aspx"><i class="fa fa-file"></i>View</a>
                            </div>
                        </div>
                        <div class="panel-body">
                          
                            <div class="table-responsive">
                                <div align="right">
                                    <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                                        OnClick="lbtnAll_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                </div>


                                <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="intGrivTypeId" PageSize="20"
                                    Width="100%" EmptyDataText="No Record(s) Found..." OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No." ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Grievance Type" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_Applicant_Name" runat="server" Text='<%# Eval("vchGrivName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active Status" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                 <asp:HiddenField ID="Hid_Griev_Type_Status" runat="server" Value='<%# Eval("intGrivStatus") %>' />
                                                <asp:Label ID="Lbl_Griv_stat" runat="server" Text='<%# Eval("intGrivStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:Button ID="btn_griv_edit" runat="server" Text="Edit" CommandArgument='<%# Eval("intGrivTypeId") %>'
                                                    OnClick="btn_griv_edit_Click" CssClass="btn btn-success btn-sm" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-grid no-print" />
                                </asp:GridView>
                            </div>
                            <%--</div>--%>
                        
                        </div>
                    </div>
                </div>
            </div>
            <!-- customer Modal1 -->
            <!-- /.modal -->
            <!-- Modal -->
        </div>
        <!-- /.content -->
    </div>
    <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
</asp:Content>
