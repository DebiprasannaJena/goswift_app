<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ViewAdminProposal.aspx.cs" Inherits="Portal_Proposal_ViewAdminProposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function clear() {
            document.getElementById('ContentPlaceHolder1_ddlDistrict').value = "0";
            document.getElementById('ContentPlaceHolder1_ddlBlock').value = "0";
        }
        function setvaluesOfrow(flu) {
           
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 1
            clear();

            var de = document.getElementById('ContentPlaceHolder1_gvService_hdnTextVal_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnproposalno').value = document.getElementById('ContentPlaceHolder1_gvService_hdnTextVal_' + rows).value;
            document.getElementById('ContentPlaceHolder1_hdnCreted').value = document.getElementById('ContentPlaceHolder1_gvService_hdnCretedBy_' + rows).value;
        }

        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#-,./;\'';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
            }
            var k;
            k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
            if (k != 13 && k != 8 && k != 0) {
                if ((e.ctrlKey == false) && (e.altKey == false)) {
                    return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
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
                Proposals</h1>
            <ul class="breadcrumb">
                <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                <li><a>Proposal</a></li><li><a>View</a></li></ul>
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
                            <a class="btn btn-add " href="ViewProposal.aspx"><i class="fa fa-plus"></i>Take Action</a>
                        </div>
                        <%-- <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="demoitems.aspx"> 
                              <i class="fa fa-file"></i> View List </a>  
                           </div>--%>
                    </div>
    <div class="panel-body">
        <div align="right">
            <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"
                OnClick="lbtnAll_Click"></asp:LinkButton>
            &nbsp;&nbsp;
            <asp:Label ID="lblPaging" runat="server"></asp:Label>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server"
                AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvService_PageIndexChanging"
                OnRowDataBound="gvService_RowDataBound" DataKeyNames="strProposalNo,strStatus,intDistId,intBlockId"
                Width="100%" EmptyDataText="No Record(s) Found..." PageSize="10">
                <Columns>
                     <asp:BoundField HeaderText="SlNo." ItemStyle-Width="4%" />
                    <asp:TemplateField HeaderText="Proposal No">
                        <ItemTemplate>
                            <asp:HyperLink ID="hypLink" runat="server" NavigateUrl="ProposalDetails.aspx" Text='<%# Eval("strProposalNo") %>'></asp:HyperLink>
                            <asp:HiddenField ID="hdnTextVal" runat="server" Value='<%# Eval("strProposalNo")%>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="hdnDist" runat="server" Value='<%# Eval("intDistId")%>'></asp:HiddenField>
                            <asp:HiddenField ID="hdnBlok" runat="server" Value='<%# Eval("intBlockId")%>'></asp:HiddenField>
                            <asp:HiddenField ID="hdnCretedBy" Value='<%# Eval("intCreatedBy")%>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="11%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="decAmount" HeaderText="Industry Type" ItemStyle-Width="11%" />
                    <asp:BoundField DataField="strRemarks" HeaderText="Name Of Company/Enterprises" />
                    <asp:BoundField DataField="strAppliedDistBlock" HeaderText="Applied District/Block"
                        ItemStyle-Width="15%" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            Recommend District/Block
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EDT" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                class="btn btn-xs btn-primary" AlternateText="VIEW" ToolTip="Click To Edit" data-toggle="modal"
                                data-target="#myModalDistBlock" OnClientClick="setvaluesOfrow(this);">  <i class="fa fa-pencil" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="13%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="dtmCreatedOn" HeaderText="Application Date" ItemStyle-Width="13%" />
                    <asp:BoundField DataField="strStatus" HeaderText="Status" ItemStyle-Width="10%" />
                </Columns>
                <PagerStyle CssClass="pagination-grid no-print" />
            </asp:GridView>
        </div>
    </div>
    </div> </div> </div>
    <!-- District Block Modal Popup -->
    <div class="modal fade" id="myModalDistBlock" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header modal-header-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h4 class="modal-title">
                        <i class="fa fa-user m-r-5"></i>Recommended District/Block</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-4">
                                <label for="District">
                                    District <span class="text-red">*</span></label><br />
                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnproposalno" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdnCreted" runat="server"></asp:HiddenField>
                            </div>
                            <div class="col-sm-4">
                                <label for="Block">
                                    Block <span class="text-red">*</span></label><br />
                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="clearfix">
                    </div>
                    <asp:HiddenField ID="hdnDistrict" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnBlock" runat="server"></asp:HiddenField>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnEditDist" class="btn btn-add btn-sm" runat="server" Text="Update"
                        OnClick="btnEditDist_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->
    </section>
        <!-- /.content -->
    </div>
</asp:Content>
