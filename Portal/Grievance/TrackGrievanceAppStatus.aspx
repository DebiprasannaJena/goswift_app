<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="TrackGrievanceAppStatus.aspx.cs" Inherits="Portal_Helpdesk_TrackGrievanceAppStatus" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" ClientIDMode="Static">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../js/WebValidation.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        $(document).ready(function () {

            $('#BtnSearch').click(function () {
                if ($('#Txt_Griv_Id').val() == '' && $('#Txt_Mobile_No').val() == '' && $('#Txt_Company_Name').val() == '' && $('#Txt_Email_Id').val() == '') {
                    jAlert('<strong>Please enter at least one field !!</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_Griv_Id").focus(); });
                    return false;
                }
            });

        });

    </script>

    <div class="content-wrapper">
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Track Grievance Status</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>HelpDesk</a></li>
                    <li><a>Track Grievance Status</a></li>
                </ul>
            </div>
        </div>
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-bd lobidisable">
                                <div class="panel-body">
                                    <div class="search-sec">
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Grievance Id</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="Txt_Griv_Id" CssClass="form-control" runat="server" MaxLength="50" AutoCompleteType="None" autoComplete="Off"></asp:TextBox>
                                                </div>
                                                <label class="col-sm-2">
                                                    Company Name</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="Txt_Company_Name" CssClass="form-control" runat="server" MaxLength="100" AutoCompleteType="None" autoComplete="Off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Mobile No</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="Txt_Mobile_No" CssClass="form-control" runat="server" MaxLength="10" AutoCompleteType="None" autoComplete="Off"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                        TargetControlID="Txt_Mobile_No" FilterMode="ValidChars" FilterType="Numbers"
                                                        ValidChars="0-9">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                                <label class="col-sm-2">
                                                    Email Id</label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="Txt_Email_Id" CssClass="form-control" runat="server" MaxLength="50" AutoCompleteType="None" autoComplete="Off"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-7">
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:Button ID="BtnSearch" runat="server" Text="Search" class="btn btn-add" OnClick="BtnSearch_Click"></asp:Button>
                                                    <asp:Button ID="BtnReset" runat="server" Text="Reset" class="btn btn-danger" OnClick="BtnReset_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <div align="right">
                                            <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"></asp:LinkButton>
                                            &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                        </div>
                                        <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="False" AllowPaging="True" Width="100%" EmptyDataText="No Record(s) Found..."
                                            DataKeyNames="vchGrivId" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="3%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grievance Id" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HypLnk_Griv_Id" ForeColor="Blue" Text='<%#Eval("vchGrivId") %>'
                                                            runat="server" ToolTip="Click here to view grievance details"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name Of Company" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Company_Name" runat="server" Text='<%# Eval("VCH_INV_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Applicant Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Applicant_Name" runat="server" Text='<%# Eval("vchApplicantName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Mobile_No" runat="server" Text='<%# Eval("vchMobileNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Id" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Email_Id" runat="server" Text='<%# Eval("vchEmail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="13%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Industry Type" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Industry_Type" runat="server" Text='<%# Eval("intIndustryCategory") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Investment Level" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="Hid_Invest_Level" runat="server" Value='<%# Eval("intInvestLevel") %>' />
                                                <asp:Label ID="Lbl_Invest_Level" runat="server" Text='<%# Eval("vchInvestLevel") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="11%"></ItemStyle>
                                        </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField HeaderText="District Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_District_Name" runat="server" Text='<%# Eval("vchDistrictName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Grievance Type" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Griv_Type" runat="server" Text='<%# Eval("vchGrivType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="11%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grievance Sub Type" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Griv_Sub_Type" runat="server" Text='<%# Eval("vchGrivSubType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="11%"></ItemStyle>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Grievance Title" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Griv_Title" runat="server" Text='<%# Eval("vchGrivTitle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Apply Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Apply_Date" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="Hid_Status" runat="server" Value='<%# Eval("intStatus") %>' />
                                                        <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("vchStatusName")  %>' Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Action Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Action_Date" runat="server" Text='<%# Eval("dtmActionDate" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="6%"></ItemStyle>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Red" Font-Italic="true" Font-Bold="true" />
                                            <PagerStyle CssClass="pagination-grid no-print" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

