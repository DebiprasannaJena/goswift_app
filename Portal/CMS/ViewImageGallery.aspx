<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewImageGallery.aspx.cs" Inherits="Miscellaneous_ViewImageGallery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            max-width: 750px;
            max-height: 400px;
        }
        
        .img
        {
            max-height: 500px;
            max-width: 100%;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../js/Validator.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function chkAllCheckbox(obj) {
            var gv = document.getElementById('<%=gvGallery.ClientID %>');
            for (var i = 0; i < gv.all.length; i++) {
                var node = gv.all[i];
                node.checked = obj.checked;
            }
        }
        function UnsetAll(e) {
            var eState = e.checked;
            if (eState == false)
                document.getElementById("gvGallery_ctl02_Checkbox1").checked = false;
        }
    </script>
    <%--End Code for Select all checkbox--%>
    <%--Code for Validate all checkbox for delete--%>
    <script type="text/javascript">
        function CheckAuthenticate() {
            for (var i = 0; i < document.forms[0].elements.length; i++) {
                if (document.forms[0].elements[i].checked == true) {
                    if (confirm(" Are you sure you want to delete the record ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }

            jAlert("Please select a record to delete !");
            return false;
        }

    </script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
        <div class="header-icon">
            <i class="fa fa-dashboard"></i>
        </div>
        <div class="header-title">
            <h1>
                Manage Gallery</h1>
            <ul class="breadcrumb">
                <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                <li><a>Gallery</a></li><li><a>View Gallery</a></li></ul>
        </div>
         </section>
        <!-- Main content -->
        <section class="content">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-bd lobidisable">
                    <div class="panel-heading">
                        <div class="btn-group buttonlist">
                            <a class="btn btn-add " href="AddImageGallery.aspx"><i class="fa fa-plus"></i>Add
                            </a>
                        </div>
                        <div class="btn-group buttonlist">
                            <a class="btn btn-add " href="ViewImageGallery.aspx"><i class="fa fa-file"></i>View
                            </a>
                        </div>
                    </div>
                    <div class="panel-body">
                        <span style="text-align: right; float: right; padding-right: 5px; height: 21px;">
                            <asp:LinkButton ID="lbtnAll" runat="server" CssClass="more" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>
                            <asp:Label ID="lblPaging" runat="server" Text=""></asp:Label>
                        </span>
                        <asp:GridView ID="gvGallery" runat="server" class="table table-bordered table-hover"
                            AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" CellPadding="4"
                            GridLines="None" DataKeyNames="intImageId" PageSize="10" AllowPaging="true" OnRowEditing="gvGallery_RowEditing"
                            OnPageIndexChanging="gvGallery_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-CssClass="noPrint" ItemStyle-CssClass="noPrint">
                                    <%-- <HeaderTemplate>
                                            <input name="cbAll" value="cbAll" type="checkbox" onclick="SelectAll(cbAll,'gvGallery','ContentPlaceHolder1')" />
                                        </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItem" runat="server" onclick="return deSelectHeader(cbAll,'gvGallery','ContentPlaceHolder1')" />
                                    </ItemTemplate>
                                     <ItemStyle Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image Name">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_Img_Name_G" runat="server" Text='<%# Bind("vchImgDescription")%>'></asp:Label>
                                          <asp:HiddenField ID="Hid_Image_File_Name" runat="server" Value='<%# Bind("vchImage")%>'></asp:HiddenField>
                                    </ItemTemplate>                                  
                                </asp:TemplateField>                                 
                                <asp:TemplateField HeaderText="Small Image">
                                    <ItemTemplate>                                  
                                        <asp:LinkButton ID="LnkBtn_View_Image_Small" runat="server" OnClick="LnkBtn_View_Image_Small_Click" ToolTip="Click Here to View Image !!"><i class="fa fa-download"></i></asp:LinkButton>
                                    </ItemTemplate>
                                      <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Medium Image">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkBtn_View_Image_Medium" runat="server" OnClick="LnkBtn_View_Image_Medium_Click" ToolTip="Click Here to View Image !!"
                                          ><i class="fa fa-download"></i></asp:LinkButton>
                                    </ItemTemplate>
                                      <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Big Image">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkBtn_View_Image_Big" runat="server" OnClick="LnkBtn_View_Image_Big_Click" ToolTip="Click Here to View Image !!"
                                             ><i class="fa fa-download"></i></asp:LinkButton>
                                    </ItemTemplate>
                                      <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAction" class="btn btn-add btn-sm" runat="server" CommandName="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                        <asp:HiddenField ID="hdn" runat="server" Value='<%#Eval("intImageId")%>'></asp:HiddenField>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="NOPRINT" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="NOPRINT" Width="5%" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="paging noPrint" HorizontalAlign="Right" />
                        </asp:GridView>
                        <asp:Button runat="server" Text="Delete" ID="btnDelete" CssClass="btn btn-danger"
                            OnClick="btnDelete_Click"></asp:Button>
                        <asp:Label ID="lblMessage" runat="server" Text="No Records found" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
         </section>
        <asp:HiddenField ID="Hid_Pop" runat="server" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
            TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="ImgBtn_Close">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
            <div id="undertakingipr2015">
                <div>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header bg-purpul">
                            <div class="row">
                                <h4 class="modal-title">
                                    <div class="col-sm-10 text-left">
                                        <asp:Label ID="Lbl_Img_Desc" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <asp:ImageButton ID="ImgBtn_Close" runat="server" ImageUrl="~/images/cancel-square.png"
                                            ToolTip="Close" />
                                    </div>
                                </h4>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="listdiv">
                                <center>
                                    <asp:Image ID="Img_View" runat="server" CssClass="img" />
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
