<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AddGrievanceType.aspx.cs" Inherits="Portal_Grievance_AddGrievanceType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validateGrievanceType() {

            if (blankFieldValidation('ContentPlaceHolder1_Txt_Griv_Name', 'Grievance type', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Griv_Name").focus();
                return false;
            };
            if (WhiteSpaceValidation1st('ContentPlaceHolder1_Txt_Griv_Name', 'Grievance type', projname) == false) {
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
                <h1>Add Grievance Type</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Grievance</a></li>
                    <li><a>Add Grievance Type</a></li>
                </ul>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="AddGrievanceType.aspx"><i class="fa fa-plus"></i>Add
                                </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ViewGrievanceType.aspx"><i class="fa fa-file"></i>View
                                </a>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="udpDiv" runat="server">
                            <ContentTemplate>
                                <div class="ibox-content">
                                    <br />
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            Grievance Type <span class="mandetory-y">*</span></label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Griv_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                            
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
                                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                <asp:ListItem Value="2">InActive</asp:ListItem>
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
                                                OnClick="Btn_Submit_Click" OnClientClick="return validateGrievanceType();" />
                                            <asp:Button ID="Btn_Reset" runat="server" Text="Reset" class="btn btn-danger" OnClick="Btn_Reset_Click" />
                                            <asp:HiddenField ID="Hid_OG_Id" runat="server" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Btn_Submit" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
