<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AMSDashBoard.aspx.cs" Inherits="Portal_Dashboard_AMSDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            var frm_hit = 600;
            var location_detl_ftr = '<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>';

            $('.Feedback').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = '../AMS/MemberCommentsAdd.aspx?ID=' + url + '';
                var headertxt = 'Feedback Details'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });
            $('.FinDoc').click(function () {
                var url = $(this).attr('data');
                var location_detl_body_cnt = '../AMS/FinancialDetailShow.aspx?ID=' + url + '';
                var headertxt = 'Financial Documents'
                openPageModalnHdr(location_detl_body_cnt, location_detl_ftr, frm_hit, headertxt);
            });

        });

        function openPageModalnHdr(page, footer, frm_hit, header) {
            $('#pageModal .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0' scrolling='yes' style='height: 500px !important;'></iframe>");
            $('#pageModal .modal-footer').html(footer);
            if (footer == "") { $('#pageModal .modal-footer').remove(); }
            $('#pageModal').modal();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content-header">
                    <div class="header-icon">
                        <i class="fa fa-tachometer"></i>
                    </div>
                    <div class="header-title">
                        <h1>
                            AMS DashBoard</h1>
                        <ul class="breadcrumb">
                            <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                            <li><a>AMS DashBoard</a></li></ul>
                    </div>
                </section>
                <section class="content">
                    <div class="row">
                        <!-- Form controls -->
                        <div class="col-sm-12" id="divFeedback" visible="false" runat="server">
                            <div class="investordashboard-sec incentive-sec ">
                                <h4>
                                    Pending Agenda Form
                                </h4>
                                <div class="portletcontainer cmdashbordportlet">
                                    <div class="scroll-prtlet">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdFeedback" runat="server" Width="100%" AllowPaging="true" PageSize="10"
                                                AutoGenerateColumns="False" OnPageIndexChanging="grdFeedback_PageIndexChanging"
                                                DataKeyNames="FinDoc,REOPENSTATUS" OnRowDataBound="grdFeedback_RowDataBound"
                                                CssClass="table table-bordered">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsl" runat="server" Text='<%#(grdFeedback.PageIndex * grdFeedback.PageSize) + (grdFeedback.Rows.Count + 1)%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <a href="../AMS/ProjectDetails.aspx?ID=<%# Eval("INTPROJCTID") %>" title="Details"
                                                                target="_blank">
                                                                <%# Eval("VCHPROJCT_NAME")%></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VCHPROJCT_LOCATION" HeaderText="Location" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VCHSECTOR" HeaderText="Sector">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ECATEGORY" HeaderText="Category">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PTYPE" HeaderText="Type">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Financial Documents">
                                                        <ItemTemplate>
                                                            <a href="javascript:void(0)" title="Financial Documents" class="FinDoc" data='<%# Eval("INTPROJCTID") %>'>
                                                                <i class="fa fa-file-text-o"></i></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" CssClass="noPrint" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Feedback">
                                                        <ItemTemplate>
                                                            <a href="javascript:void(0)" title="Put Your Feedback" class="Feedback" data='<%# Eval("INTPROJCTID") %>'>
                                                                <i class="fa fa-comment"></i></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px" CssClass="noPrint" />
                                                        <FooterStyle />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="paging NOPRINT" />
                                                <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                                                    LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
                                            </asp:GridView>
                                            <div align="center">
                                                <asp:Label ID="lblMessage" runat="server" Text="No Pending Application(s) For You!!!"
                                                    Visible="false" CssClass="lblmsg" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12" id="divAgendaRequest" visible="false" runat="server">
                            <div class="investordashboard-sec incentive-sec ">
                                <h4>
                                    New SWP Agenda Request
                                </h4>
                                <div class="portletcontainer cmdashbordportlet">
                                    <div class="scroll-prtlet">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdForwardProj" runat="server" Width="100%" AllowPaging="true"
                                                PageSize="10" AutoGenerateColumns="False" DataKeyNames="INTPROJCTID" OnPageIndexChanging="grdForwardProj_PageIndexChanging"
                                                OnRowCommand="grdForwardProj_RowCommand" CssClass="table table-bordered">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsl" runat="server" Text='<%#(grdForwardProj.PageIndex * grdForwardProj.PageSize) + (grdForwardProj.Rows.Count + 1)%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VCH_UID" HeaderText="SWP Proposal No.">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VCHPROJCT_NAME" HeaderText="Project Name">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <%--   <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <a href="../SingleWindow/ProjectDetails.aspx?ID=<%# Eval("INTPROJCTID") %>" title="Details" target="_blank">
                                                        <%# Eval("VCHPROJCT_NAME")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="VCHSECTOR" HeaderText="Sector">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VCHPROJCT_LOCATION" HeaderText="Location">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <%--<a href="javascript:void(0)" title="Create Project" class="Feedback" data='<%# Eval("INTPROJCTID") %>'>
                                                                <i class="fa fa-pencil"></i></a>--%>
                                                            <asp:LinkButton ID="lnkbtnEdit" CssClass="btn btn-mini btn-success" runat="server"
                                                                CommandName="E" OnClientClick="return confirm(' Are you sure want to Create Project for this New Request')"
                                                                CommandArgument='<%# Eval("INTPROJCTID") %>'>Create</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px" CssClass="noPrint" />
                                                        <FooterStyle />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="paging NOPRINT" />
                                                <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                                                    LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMessage1" runat="server" Text="No SWP Agenda For You!!!" Visible="false"
                                                CssClass="lblmsg" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12" id="divPendingProjStatus" visible="false" runat="server">
                            <div class="investordashboard-sec incentive-sec ">
                                <h4>
                                    Pending Project Status
                                </h4>
                                <div class="portletcontainer cmdashbordportlet">
                                    <div class="scroll-prtlet">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdProjSts" runat="server" Width="100%" AllowPaging="true" PageSize="10"
                                                AutoGenerateColumns="False" DataKeyNames="INTPROJCTID" OnPageIndexChanging="grdProjSts_PageIndexChanging"
                                                CssClass="table table-bordered">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsl" runat="server" Text='<%#(grdProjSts.PageIndex * grdProjSts.PageSize) + (grdProjSts.Rows.Count + 1)%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company Name">
                                                        <ItemTemplate>
                                                            <a href="../SingleWindow/ProjectDetails.aspx?ID=<%# Eval("INTPROJCTID") %>" title="Details"
                                                                target="_blank">
                                                                <%# Eval("VCHPROJCT_NAME")%></a>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VCHPROJECT_TITLE" HeaderText="Project Title">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DTMAPPLICATION_EBIZ" HeaderText="Date of Application"
                                                        DataFormatString="{0:dd/MM/yyyy}">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <PagerStyle CssClass="paging NOPRINT" />
                                                <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                                                    LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
                                            </asp:GridView>
                                            <div align="center">
                                                <asp:Label ID="lblMsgSts" runat="server" Text="No Pending Projects found!!!" Visible="false"
                                                    CssClass="lblmsg" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="pageModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content modal-primary ">
                <div class="modal-header bg-red">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Enter Your Feedback
                    </h4>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
