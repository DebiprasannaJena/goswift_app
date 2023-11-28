<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="GetFileNameListFromDirectory.aspx.cs" Inherits="Portal_SuperAdmin_GetFileNameListFromDirectory" %>

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
                <h1>Get File Name From Directory</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Admin Privileges</a></li>
                    <li><a>Get File Name From Directory</a></li>
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
                                        <div class="col-sm-6">
                                            <h4 class="h4-header">Search File from Virtual Directory</h4>
                                        </div>
                                        <div class="col-sm-6">
                                            <h4 class="h4-header">Search File from Physical Directory</h4>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Select File Directory Path (Virtual)
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DdlVirtualDirectoryPath" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">~/Document</asp:ListItem>
                                                <asp:ListItem Value="2">~/Document/RegdDoc</asp:ListItem>
                                                <asp:ListItem Value="3">~/Enclosure</asp:ListItem>
                                                <asp:ListItem Value="4">~/Download</asp:ListItem>
                                                <asp:ListItem Value="5">~/Logs/ErrorLog</asp:ListItem>
                                                <asp:ListItem Value="6">~/Logs/ReqResLog</asp:ListItem>
                                                <asp:ListItem Value="7">~/Images</asp:ListItem>
                                                <asp:ListItem Value="8">~/Logo</asp:ListItem>
                                                <asp:ListItem Value="9">~/Portal/Images</asp:ListItem>
                                                <asp:ListItem Value="10">~/Portal/ApprovalDocs</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Select File Directory Path (Physical)
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DdlPhysicalDirectoryPath" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">D:\WWWROOT\SWPGOO\SWP_Service\Logs\ErrorLog</asp:ListItem>
                                                <asp:ListItem Value="2">D:\WWWROOT\SWPGOO\SWP_Service\Logs\ReqResLog</asp:ListItem>
                                                <asp:ListItem Value="3">C:\WEBROOT\SWPGOO\SWPAPP_Service\Logs\ErrorLog</asp:ListItem>
                                                <asp:ListItem Value="4">C:\WEBROOT\SWPGOO\SWPAPP_Service\Logs\ReqResLog</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-4">
                                            <strong>OR</strong>
                                        </div>
                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-4">
                                            <strong>OR</strong>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Enter File Directory Path (Virtual)
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="TxtVirtualDirectoryPath" runat="server" placeholder="e.g.- ~/Portal/ApprovalDocs/" AutoCompleteType="None" autoComplete="off" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Enter File Directory Path (Physical)
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="TxtPhysicalDirectoryPath" runat="server" placeholder="e.g.- D:\New folder\Downloads" AutoCompleteType="None" autoComplete="off" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Enter Pattern Name
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="TxtVirtualFilePattern" runat="server" CssClass="form-control" placeholder="Enter file pattern name."></asp:TextBox>
                                        </div>

                                        <div class="col-sm-2">
                                            <label for="User">
                                                Enter Pattern Name
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="TxtPhysicalFilePattern" runat="server" CssClass="form-control" placeholder="Enter file pattern name."></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-lg-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="BtnSearchVirtual" runat="server" CssClass="btn btn-success" Text="Search" OnClick="BtnSearchVirtual_Click" />
                                            <asp:Button ID="BtnResetVirtual" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="BtnResetVirtual_Click" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="BtnSearchPhysical" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="BtnSearchPhysical_Click" />
                                            <asp:Button ID="BtnResetPhysical" runat="server" Text="Reset" CssClass="btn btn-warning" OnClick="BtnResetPhysical_Click" />
                                        </div>
                                        <div class="clearfix">
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="User">
                                                </label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <asp:Label runat="server" ID="Lbl_Error" CssClass="text-danger"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <asp:Label ID="LblTotal" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>

                                    <div class="table-responsive">

                                        <asp:GridView ID="GrdVirtual" runat="server" class="table table-bordered table-hover" EmptyDataText="No Record(s) found...." Width="100%" AutoGenerateColumns="false" OnRowDataBound="GrdVirtual_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SlNo" ItemStyle-Width="4%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSlNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ViewDocs">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HlDisplay" runat="server" Target="_blank" ToolTip="Click here to view the document">View</asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <pre> <asp:Label runat="server" ID="LblName" Text='<%#Eval("Name")%>'></asp:Label></pre>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Directory Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDirectoryName" runat="server" Text='<%#Eval("DirectoryName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="FullName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblFullName" runat="server" Text='<%#Eval("FullName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Extention">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblFileType" runat="server" Text='<%#Eval("Extension")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CreationTime">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDateTime" runat="server" Text='<%#Eval("CreationTime")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Length">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLen" runat="server" Text='<%#Eval("Length")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="IsReadOnly">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblIsReadOnly" runat="server" Text='<%#Eval("IsReadOnly")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Exists">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblExists" runat="server" Text='<%#Eval("Exists")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CreationTimeUtc">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblCreaionTime" runat="server" Text='<%#Eval("CreationTimeUtc")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LastAccessTime">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblAcessTime" runat="server" Text='<%#Eval("LastAccessTime")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LastAccessTimeUtc">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblAcessTiemUtc" runat="server" Text='<%#Eval("LastAccessTimeUtc")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LastWriteTime">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLastWriteTime" runat="server" Text='<%#Eval("LastWriteTime")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LastWriteTimeUtc">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLastWriteTimeUtc" runat="server" Text='<%#Eval("LastWriteTimeUtc")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <%-- This Gridview is to show the files available in physical path --%>

                                        <asp:GridView ID="GrdPhysical" runat="server" class="table table-bordered table-hover" EmptyDataText="No Record(s) found...." GridLines="Vertical" Width="100%" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SlNo" InsertVisible="False" Visible="true" ItemStyle-Width="4%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSlNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Download" InsertVisible="False" Visible="true" ItemStyle-Width="4%">
                                                    <ItemTemplate>
                                                        <asp:Button ID="BtnDownload" runat="server" Text="Download" OnClick="BtnDownload_Click" CssClass="btn btn-primary btn-sm" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <pre> <asp:Label runat="server" ID="LblName1" Text='<%#Eval("Name")%>'></asp:Label></pre>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DirectoryName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDirectoryName1" runat="server" Text='<%#Eval("DirectoryName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="FullName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblFullName1" runat="server" Text='<%#Eval("FullName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Extention">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblExtension1" runat="server" Text='<%#Eval("Extension")%>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CreationTime">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDateTime1" runat="server" Text='<%#Eval("CreationTime")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Length">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLen1" runat="server" Text='<%#Eval("Length")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="IsReadOnly">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblIsReadOnly1" runat="server" Text='<%#Eval("IsReadOnly")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Exists">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblExists1" runat="server" Text='<%#Eval("Exists")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CreationTimeUtc">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblCreaionTime1" runat="server" Text='<%#Eval("CreationTimeUtc")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LastAccessTime">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblAcessTime1" runat="server" Text='<%#Eval("LastAccessTime")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LastAccessTimeUtc">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblAcessTiemUtc1" runat="server" Text='<%#Eval("LastAccessTimeUtc")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LastWriteTime">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLastWriteTime1" runat="server" Text='<%#Eval("LastWriteTime")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LastWriteTimeUtc">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblLastWriteTimeUtc1" runat="server" Text='<%#Eval("LastWriteTimeUtc")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>