<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ManageUserLogin.aspx.cs" Inherits="Portal_SuperAdmin_ManageUserLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Manage User Login</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Admin Privileges</a></li>
                    <li><a>Manage User Login</a></li>
                </ul>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Enter User Name</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="txtuser" runat="server" class="form-control" AutoCompleteType="None" autoComplete="off"></asp:TextBox>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Search Type</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_Search_Type" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">Contains</asp:ListItem>
                                                <asp:ListItem Value="2">Starts With</asp:ListItem>
                                                <asp:ListItem Value="3">Ends With</asp:ListItem>
                                                <asp:ListItem Value="4">Equal</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-success" Text="Search" OnClick="BtnSearch_Click" />
                                            <asp:Button ID="BtnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="BtnReset_Click" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdUser" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="false" EmptyDataText="No Record(s) found...." DataKeyNames="INTUSERID,VCHUSERNAME" Width="60%"
                                            OnPageIndexChanging="GrdUser_PageIndexChanging" AllowPaging="true" PageSize="15" OnRowEditing="GrdUser_RowEditing">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Full Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfname" runat="server" Text='<%#Eval("VCHFULLNAME")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluname" runat="server" Text='<%#Eval("VCHUSERNAME")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Login">
                                                    <ItemTemplate>
                                                        <asp:Button ID="BtnLogin" runat="server" Text="Login" CssClass="btn btn-success" CommandName="edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="GrdUser" />
                                </Triggers>
                            </asp:UpdatePanel>


                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

