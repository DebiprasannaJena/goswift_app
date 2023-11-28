<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="UploadJSFile.aspx.cs" Inherits="Service_UploadJSFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Console/scripts/ajax.js" type="text/javascript"></script>
    <script src="../Console/scripts/ajax.js" type="text/javascript"></script>
    <script src="../Console/scripts/AjaxScript.js" type="text/javascript"></script>
    <script src="../js/jQuery.alert.js" type="text/javascript"></script>
    <link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Approval Configuration</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Service</a></li><li><a>Upload Js file</a></li></ul>
               </div>
        </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ApprovalConfig.aspx"> 
                              <i class="fa fa-file"></i>Upload Js file</a>  
                           </div>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                            <div class="form-group">
                                <div class="row">
                                 <div class="col-sm-12">
                                 <label>Department</label>
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                 <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt" Width="350px" onselectedindexchanged="ddldept_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                  </ContentTemplate>
                                 </asp:UpdatePanel>
                                 </div>
                                 </div>
                                <div class="row">
                               <div class="col-sm-12">
                                 <label>Service Sector</label>
                                  <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                 <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control ser" 
                                            Width="350px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlService_SelectedIndexChanged"></asp:DropDownList>
                                  </ContentTemplate>
                                  <triggers>
                                    <asp:asyncpostbacktrigger controlid="ddldept"  />
                                    </triggers>
                                 </asp:UpdatePanel>
                                 </div>
                                 </div>
                                <div class="row">
                                 <div class="col-sm-12">
                                 <label>Upload Js File</label>
                                 <asp:FileUpload  ID="JsUpload" CssClass="form-control" runat="server" Width="350px"/>
                                    <div style="margin-left: 15px; float: left">
                                    <span class="mandatory" style="font-weight: bold">(Upload only .js file)</span>
                                   <asp:HiddenField ID="hdnjs" runat="server" />
                                   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                           <asp:LinkButton ID="LnkFileName" runat="server" Text="" OnClick="LnkFileName_OnClick" />
                                           </ContentTemplate>
                                            <triggers>
                                    <asp:asyncpostbacktrigger controlid="LnkFileName"  />
                                    </triggers>
                                 </asp:UpdatePanel>
                                          <%-- <asp:HyperLink ID="LnkFileName" runat="server"></asp:HyperLink>--%>
                                            <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
                                   </div>
                                 </div>
                            </div>
                           </div>
                        </div>
                        <asp:Button ID="btnsave" runat="server" Text="Save" class="btn btn-add btn-sm save" OnClientClick="return Validation123();"  onclick="btnsave_Click"></asp:Button>
                     </div>
                  </div>
               </div>
        </section>
        <!-- /.content -->
    </div>
    <script type="text/javascript">
        $(function () {
            $(".save").click(function () {
                var ddldept = $(".dpt");
                var ddlService = $(".ser");
                var ddlLocation = $(".location");

                if (ddldept.val() == "0") {
                    alert("Please select Department !");
                    return false;
                }

                if (ddlService.val() == "0") {
                    alert("Please select Service !");
                    return false;
                }


                if (!chkSACgrd()) {
                    return false;
                }
                return true;
            });

        });

    </script>
</div>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>
