<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="Parent_User_Swapping.aspx.cs" Inherits="Portal_PAN_Operation_Parent_User_Swapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-tachometer"></i>
            </div>
            <div class="header-title">
                <h1>
                    Main and Subsidiary Unit Swapping</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>PAN Based Registration</a></li></ul>
            </div>
        </div>
        <!-- Main content -->
        <div class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
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
                                        Main Unit Name
                                    </label>
                                    <div class="col-sm-3">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="DrpDwn_Unit_Name" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btn btn-success"
                                            OnClick="Btn_Search_Click" ToolTip="Click Here to View Users !!" />
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div style="display: inline-block; text-align: right; width: 100%">
                                            <asp:LinkButton ID="LbtnAll" runat="server" Visible="false" CssClass="" Text="All" OnClick="LbtnAll_Click"></asp:LinkButton>
                                            
                                            
                                            <asp:Label ID="LblPaging" runat="server"></asp:Label>
                                        </div>
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true" ShowFooter="false" Width="100%" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SlNo">
                                            <ItemTemplate>                                            
                                                  <asp:Label ID="lblsl" runat="server" Text='<%#(GridView1.PageIndex * GridView1.PageSize) + (GridView1.Rows.Count + 1)%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Investor_Name" Text='<%# Eval("VCH_INV_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="25%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Old User Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Old_User_Id" Text='<%# Eval("VCH_INV_USERID_BK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Investor Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Invetsor_Id" Text='<%# Eval("int_investor_id") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="9%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Parent Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Parent_Id" Text='<%# Eval("int_parent_id") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAN">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_PAN" Text='<%# Eval("vch_pan") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="9%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Id">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_New_User_Id" Text='<%# Eval("VCH_INV_USERID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="Btn_Action" runat="server" Text="Make Main Unit" OnClick="Btn_Action_Click"
                                                    CssClass="btn btn-success" OnClientClick="return confirm('Are you sure to make this unit as main unit !!');">
                                                </asp:Button>
                                            </ItemTemplate>
                                            <ItemStyle Width="7%" />
                                        </asp:TemplateField>
                                    </Columns>
                                     <PagerStyle CssClass="pagination-grid no-print" />
                                    <EmptyDataTemplate>
                                        No subsidiary unit found for swapping !!
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle Font-Italic="true" ForeColor="Red" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
