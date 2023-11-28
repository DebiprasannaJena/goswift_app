<%--'*******************************************************************************************************************
' File Name         : ViewApplicationStatus.aspx
' Description       : View Application Status by User
' Created by        : Sushant Kumar Jena
' Created On        : 13th Sept 2017
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     
    1                           10th Oct 2017            Pranay Kumar         Implementation of Query Management 
'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewApplicationStatus.aspx.cs"
    Inherits="incentives_ViewApplicationStatus" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {
            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
        });

        function pageLoad() {
            $('.menuincentive').addClass('active');
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc2:header ID="header" runat="server" />
            <div id="Div1" class="container wrapper">
                <div class="registration-div investors-bg">
                    <div id="exTab1" class="">
                        <div class="investrs-tab">
                            <uc5:pealmenu ID="ineste" runat="server" />
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-sec">
                                    <div class="innertabs m-b-10">
                                        <ul class="nav nav-pills pull-right">
                                            <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                            <li><a href="View_EC_Application_Status.aspx">View EC Application Status</a></li>
                                            <li class="active"><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                                        </ul>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-header">
                                        <%--  <div class="iconsdiv">
                                            <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                                <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                                                    id="A1" class="pull-right printbtn"><i class="fa fa-file-excel-o"></i></a>
                                            <a href="javascript:void(0);" title="Delete" id="A2" class="pull-right printbtn"><i
                                                class="fa fa-trash-o"></i></a>
                                        </div>--%>
                                        <h2>
                                            View Application Status
                                        </h2>
                                    </div>
                                    <div class="form-body">
                                        <div class="form-group" id="divUnitName" runat="server">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Select Unit Name
                                                </label>
                                                <div class="col-sm-8">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="DrpDwn_Investor_Unit" runat="server" CssClass="form-control"
                                                        AutoPostBack="true" OnSelectedIndexChanged="DrpDwn_Investor_Unit_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Select Incentive Name
                                                </label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="DrpDwn_Inct_Name" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="col-sm-2">
                                                    Select Status
                                                </label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="DrpDwn_Status" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Scrutiny</asp:ListItem>
                                                        <asp:ListItem Value="2">Approved</asp:ListItem>
                                                        <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                        <asp:ListItem Value="4">Provisional Scrutiny</asp:ListItem>
                                                        <asp:ListItem Value="5">Provisional Approved</asp:ListItem>
                                                        <asp:ListItem Value="6">Provisional Rejected</asp:ListItem>
                                                        <asp:ListItem Value="7">Approved & Disbursed</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Enter Application Number
                                                </label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox ID="Txt_App_No" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <span class="apply">
                                                        <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btn btn-success"
                                                            OnClick="Btn_Search_Click" />
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="details-section">
                                                    <asp:GridView ID="Grd_Application" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                                        DataKeyNames="INTINCUNQUEID,strQueryStatus" OnRowDataBound="Grd_Application_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL#">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Application No">
                                                                <ItemTemplate>
                                                                    <%-- <asp:Label ID="Lbl_App_No" runat="server" Text='<%# Eval("strApplicationNum") %>'></asp:Label>--%>
                                                                    <asp:HyperLink ID="HypLnk_App_No" runat="server" Target="_blank" Text='<%# Eval("strApplicationNum") %>'
                                                                        ToolTip="Click to View Application Details"></asp:HyperLink>
                                                                    <asp:HiddenField ID="Hid_Form_Preview_Id" runat="server" Value='<%# Eval("strFormPreviewId") %>' />
                                                                    <asp:HiddenField ID="Hid_Unique_Id" runat="server" Value='<%# Eval("INTINCUNQUEID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="12%"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Unit_Name" runat="server" Text='<%# Eval("strUnitName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15%"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Incentive Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Inct_Name" runat="server" Text='<%# Eval("strInctName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Applied On">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Created_On" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="9%"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("strStatus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15%"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <div class="input-group">
                                                                        <asp:HiddenField ID="Hid_Sanction_File_Name" runat="server" Value='<%# Eval("strSanFileName") %>' />
                                                                        <asp:HyperLink ID="Hy_Sanction_Doc" runat="server" CssClass="input-group-addon bg-blue"
                                                                            ToolTip="Click to View Document" Target="_blank" NavigateUrl='<%# "../Portal/Incentive/Sanctionorder/"+ Eval("strSanFileName") %>'><i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="3%" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <%--Added By Pranay Kumar for Addition of Query Details on 11-OCT-2017--%>
                                                            <asp:TemplateField HeaderText="View Query Detail">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="hypQueryDtls" runat="server"></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="3%" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <%--Ended By Pranay Kumar for Addition of Query Details on 11-OCT-2017--%>
                                                            <asp:TemplateField HeaderText="View Disburse Detail" HeaderStyle-CssClass="noPrint"
                                                                FooterStyle-CssClass="noPrint">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnDisbursedDtls" runat="server" class="btn btn-success btn-sm"
                                                                        data-toggle="modal" data-target='<%# "#D" +Eval("INTINCUNQUEID")%>' ToolTip="Click Here to View Disburse Details"></asp:LinkButton>
                                                                    <asp:HiddenField ID="hdnDisburedId" runat="server" Value='<%# Eval("INTINCUNQUEID")%>'>
                                                                    </asp:HiddenField>
                                                                    <div class="modal fade" id='<%# "D"+Eval("INTINCUNQUEID")%>' tabindex="-1" role="dialog"
                                                                        aria-hidden="true">
                                                                        <div class="modal-dialog modal-lg">
                                                                            <div class="modal-content">
                                                                                <div class="modal-header modal-header-primary">
                                                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                                        ×</button>
                                                                                </div>
                                                                                <div class="modal-body">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panel panel-bd ">
                                                                                                <div class="panel-body">
                                                                                                    <div id="Div13" class="form-group" runat="server">
                                                                                                        <div id="DisbursedList" runat="server">
                                                                                                        </div>
                                                                                                        <div class="clearfix">
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="modal-footer">
                                                                                    <button type="button" style="display: none" class="btn btn-danger pull-right" data-dismiss="modal">
                                                                                        Close</button>
                                                                                </div>
                                                                            </div>
                                                                            <!-- /.modal-content -->
                                                                        </div>
                                                                        <!-- /.modal-dialog -->
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="3%" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Record Found
                                                        </EmptyDataTemplate>
                                                        <EmptyDataRowStyle ForeColor="Red" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--  <div id="myModal3" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">          
            <div class="modal-content">
                <div class="modal-body text-center">
                    <div class="form-group">
                        <label>
                            Revert Query</label>
                        <button type="button" class="btn btn-primary" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
