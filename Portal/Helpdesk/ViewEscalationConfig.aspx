<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewEscalationConfig.aspx.cs" Inherits="Portal_HelpDesk_ViewEscalationConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                autoclose: true,
                format: 'dd-M-yyyy'
            });

            $("a").click(function (event) {
                debugger;
                var href = $(this).attr('href');
                //$(this).attr('href', '#');
                var Filename = href.split('/');
                if (Filename[4] != undefined) {
                    if (Filename[4].indexOf('.pdf') > -1) {
                        event.preventDefault();
                    }
                };
            });

            $('.ddluser').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });

            $('.btnsubmit').click(function () {
                if ($(this).closest('tr').find('.ddlsts').val() == "0") {
                    jAlert('<strong>Please Select Status  !</strong>', 'SWP');
                    $(this).closest('tr').find('.ddlsts').focus();
                    return false;
                }
                if ($(this).closest('tr').find('.remark').val() == "") {
                    jAlert('<strong>Please Select Remark  !</strong>', 'SWP');
                    $(this).closest('tr').find('.remark').focus();
                    return false;
                }
            });
        });
    </script>
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
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Add Take Action</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>HelpDesk</a></li><li><a>Helpdesk View</a></li></ul>
            </div>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="HDIssueEscalationConfig.aspx"><i class="fa fa-plus"></i>
                                    Add</a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ViewEscalationConfig.aspx"><i class="fa fa-file"></i>View</a>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="search-sec">
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            Type</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlType" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-2">
                                            Category</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control dpt" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label class="col-sm-2">
                                            Sub-Category</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control dpt">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnShow" runat="server" Text="Search" class="btn btn-add btn-sm"
                                                OnClick="btnSubmit_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div align="right">
                                <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"></asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:Label ID="lblPaging" runat="server"></asp:Label>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gvEscalation" class="table table-bordered table-hover" runat="server"
                                    OnPageIndexChanging="gvEscalation_PageIndexChanging" DataKeyNames="int_SubcategoryId"
                                    ShowFooter="true" Width="100%" EmptyDataText="No Record(s) Found..." PageSize="10"
                                    OnRowEditing="gvEscalation_RowEditing" AutoGenerateColumns="False" OnRowDataBound="gvEscalation_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <HeaderTemplate>
                                                Sl No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsl" runat="server" Text='<%#(gvEscalation.PageIndex * gvEscalation.PageSize) + (gvEscalation.Rows.Count + 1)%>'></asp:Label>
                                                <%--<%# Container.DataItemIndex + 1 %>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%">
                                            <HeaderTemplate>
                                                Category
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob2" Text='<%#Eval("vch_CategoryName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%">
                                            <HeaderTemplate>
                                                Sub Category
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmob" Text='<%#Eval("vch_SubCategoryName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <HeaderTemplate>
                                                Level of Escalation
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDays" Text='<%#Eval("int_EscLevel") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkAns" runat="server" ToolTip="Details" NavigateUrl=<%#"javascript:my_window=window.open('EscalationDetailsPopup.aspx?SubId=" + DataBinder.Eval(Container.DataItem,"int_SubcategoryId") + "','my_window','width=700,height=600,menubar=yes,scrollbars=yes,left=30,top=30');my_window.focus()"%>>
                                                    <asp:Image ID="ImgView" AlternateText="View FAQ Details" runat="server" ImageUrl="../Images/detail.png" />
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="NOPRINT" />
                                            <HeaderStyle CssClass="NOPRINT" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkEdit" runat="server" ToolTip="Edit" CssClass="fa fa-pencil">
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-grid no-print" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- customer Modal1 -->
            <!-- /.modal -->
            <!-- Modal -->
        </section>
        <!-- /.content -->
    </div>
    <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
    </div>
</asp:Content>
