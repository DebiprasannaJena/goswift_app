<%--''*******************************************************************************************************************
' File Name             :   PCFormView.aspx
' Description           :     PCFormView
' Created by            :  Ritika Lath
' Created on            : 6th September 2017
' Modification History  :
'   <CR no.>                      <Date>             <Modified by>                <Modification Summary>'        
'                         
' Style sheet           : style.css                                               
' Javscript Functions   : jquery-1.4.2.min.js, loadComponent.js
' *********************************************************************************************************************
--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCFormView.aspx.cs" Inherits="incentives_PCFormView" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
        });                       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
    <div class="registration-div investors-bg">
        <div id="exTab1" class="container">
            <div class="investrs-tab">
                <ul class="nav nav-pills">
                    <li class="menudashboard"><a href="javascript:void(0)"><i class="fa fa-tachometer"></i>
                        Dashboard</a> </li>
                    <li class="menuprofile"><a href="../InvesterProfile.aspx"><i class="fa fa-user"></i>
                        Investor Profile</a> </li>
                    <li class="menuproposal"><a href="../Proposals.aspx"><i class="fa fa-briefcase"></i>
                        Proposals</a> </li>
                    <li class="menuservices"><a href="../DepartmentClearance.aspx"><i class="fa fa-wrench">
                    </i>Services</a> </li>
                    <%--  <li><a href="IncentiveCalculator.aspx">Incentive Calculator</a> </li>--%>
                    <li class="menuincentive"><a href="incentiveoffered.aspx"><i class="fa fa-money"></i>
                        Incentive</a> </li>
                </ul>
            </div>
            <div class="tab-content clearfix">
                <div class="form-sec">
                    <div class="innertabs">
                        <ul class="nav nav-pills pull-right">
                            <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                            <li class="active"><a href="appliedindustrylist.aspx">Apply For incentive</a></li>
                            <li><a href="javscript:void(0);">View Application Status</a></li>
                        </ul>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-header">
                <div class="iconsdiv">
                    <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                        <i class="fa fa-print"></i></a><a href="javascript:void(0);" title="Export to Excel"
                            id="A1" class="pull-right printbtn"><i class="fa fa-file-excel-o"></i></a>
                </div>
                <h2>
                    View PC Form</h2>
            </div>
            <div class="form-body minheight350">
                <div class="search-sec">
                    <div class="form-group ">
                        <div class="row">
                            <label class="col-md-2 col-sm-2">
                                Select Application Type
                            </label>
                            <div class="col-sm-3">
                                <span class="colon">:</span>
                                <asp:DropDownList ID="drpApplicationType" runat="server" class="form-control">
                                    <asp:ListItem Text="-Select Application type-" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group ">
                        <div class="row">
                            <label class="col-md-2 col-sm-2">
                                From Date
                            </label>
                            <div class="col-sm-3">
                                <span class="colon">:</span>
                                <div class="input-group  date datePicker" id="Div10">
                                    <input name="txtTimescheduleforyearofcomm" type="text" id="txtFromDate" class="form-control"
                                        runat="server" readonly="readonly">
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                            </div>
                            <label class="col-md-2 col-sm-2">
                                To Date
                            </label>
                            <div class="col-sm-3">
                                <span class="colon">:</span>
                                <div class="input-group  date datePicker" id="Div1">
                                    <input name="txtTimescheduleforyearofcomm" type="text" id="txtToDate" class="form-control"
                                        runat="server" readonly="readonly">
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-success" OnClick="btnShow_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="table-responsive ">
                        <asp:UpdatePanel ID="up1" runat="server">
                            <ContentTemplate>
                                <div style="text-align: right; margin: 4px;" class="NOPRINT">
                                    <asp:Label ID="lblDetails" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlNoOfRec" ToolTip="Page Size" Width="75px" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRec_SelectedIndexChanged"
                                        CssClass="form-control" Style="display: inline !important">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnPgindex" runat="server" />
                                </div>
                                <asp:GridView ID="grdPcApplication" runat="server" CssClass="table table-bordered bg-white"
                                    AutoGenerateColumns="False" EmptyDataText="No Record(s) Found" CellPadding="4"
                                    GridLines="None" OnRowDataBound="grdPcApplication_RowDataBound" DataKeyNames="vchAppNo"
                                    OnRowCommand="grdPcApplication_RowCommand">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="requestType" HeaderText="Application For" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />
                                        <asp:BoundField DataField="vchCompName" HeaderText="Company Name" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="vchPhNo" HeaderText="Phone No" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="unitCategory" HeaderText="Unit Category" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />
                                        <asp:BoundField DataField="organizationType" HeaderText="Organization Type" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%" />
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="noPrint"
                                            ItemStyle-CssClass="noPrint" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HypEdit" data-toggle="tooltip" ToolTip="Edit" runat="server" Visible="false"
                                                    CssClass="btn btn-success">
                                                                   <i class="fa fa-edit"></i></asp:HyperLink>
                                                <asp:LinkButton CssClass="NOPRINT btn btn-danger " data-toggle="tooltip" ToolTip="Delete"
                                                    ID="lnkDelete" Visible="false" CommandName="D" OnClientClick="javascript:return confirm('Are you sure to delete this record ?');"
                                                    runat="server" CommandArgument='<%#Container.DataItemIndex %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                <asp:HiddenField ID="hdnApplyFlag" runat="server" Value='<%#Eval("intApplyFlag") %>' />
                                                <asp:Label ID="lblApplyFlag" runat="server" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="pull-right">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <ul class="pagination">
                                                <li class='<%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %> '>
                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                                </li>
                                            </ul>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $('.datePicker').datepicker({ dateFormat: 'dd:mm:yyyy', separator: ' @ ', minDate:
    new Date(), autoclose: true
            });
        });
    </script>
    </form>
</body>
</html>
