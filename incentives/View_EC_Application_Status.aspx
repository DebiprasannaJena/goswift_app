<%--'*******************************************************************************************************************
' File Name         : View_EC_Application_Status.aspx
' Description       : To View Empowered Committee Application Status and Details
' Created by        : Sushant Kumar Jena
' Created On        : 13th Dec 2017
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_EC_Application_Status.aspx.cs"
    Inherits="incentives_View_EC_Application_Status" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View EC Application</title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
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
            <div id="Div1" class="container">
                <uc2:header ID="header" runat="server" />
                <div class="registration-div investors-bg">
                    <div id="exTab1" class="">
                        <div class="investrs-tab">
                            <uc5:pealmenu ID="ineste" runat="server" />
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-sec">
                                    <div class="innertabs">
                                        <ul class="nav nav-pills pull-right">
                                            <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                            <li class="active"><a href="View_EC_Application_Status.aspx">View EC Application Status</a></li>
                                            <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                                        </ul>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-header">
                                        <h2>
                                            Status of Application for Condonation of Delay in Implementation
                                        </h2>
                                    </div>
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="details-section">
                                                    <asp:GridView ID="Grd_Application" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                                        DataKeyNames="vchUnitCat" OnRowDataBound="Grd_Application_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Enterprise Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Enterprise_Name_G" runat="server" Text='<%# Eval("vchEnterpriseName") %>'></asp:Label>
                                                                    <asp:HiddenField ID="Hid_Sl_No" runat="server" Value='<%# Eval("intDelayId") %>' />
                                                                    <asp:HiddenField ID="Hid_Industry_Code" runat="server" Value='<%# Eval("vchIndustryCode") %>' />
                                                                    <asp:HiddenField ID="Hid_Reason" runat="server" Value='<%# Eval("vchReason") %>' />
                                                                    <asp:HiddenField ID="Hid_EC_Letter" runat="server" Value='<%# Eval("vchECLetter") %>' />
                                                                    <asp:HiddenField ID="Hid_Remark" runat="server" Value='<%# Eval("vchRemark") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Unit_Cat_G" runat="server" Text='<%# Eval("vchUnitCat") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="9%"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Applied On">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Created_On_G" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="8%"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approval Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Approval_Date_G" runat="server" Text='<%# Eval("dtmApprovalDate" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Allowed Months">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Time_Allowed_G" runat="server" Text='<%# Eval("intTimeAllowed") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_Status_G" runat="server" Text='<%# Eval("vchStatus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="7%"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LnkBtn_Details" runat="server" OnClick="LnkBtn_Details_Click"
                                                                        ToolTip="Click Here to View Details">View Details</asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No Record Found
                                                        </EmptyDataTemplate>
                                                        <EmptyDataRowStyle ForeColor="Red" />
                                                    </asp:GridView>
                                                    <br />
                                                    <div id="Div_Details" runat="server" style="border: 1px solid #dedede;">
                                                        <h2 style="text-align: center; border-bottom: 1px solid #dedede;">
                                                            Application Details for Condonation of Delay in Implementation</h2>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Industry Code
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Industry_Code" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-2 ">
                                                                    Status
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Status" runat="server" Font-Bold="true" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3 ">
                                                                    Name of Enterprise/Industrial Unit
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Enterprise_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-2 ">
                                                                    Approval Date
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Approval_Date" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Unit Category
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Unit_Cat" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-2">
                                                                    Time Period Allowed
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Time_Allowed" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Applied On
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Created_On" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-2">
                                                                    EC Letter
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <div class="input-group">
                                                                        <asp:Label ID="Lbl_EC_Letter" runat="server" CssClass="form-control-static"></asp:Label>
                                                                        <asp:HyperLink ID="Hy_EC_Letter" runat="server" class="btn btn-info" ToolTip="Click Here to View EC Letter !!"
                                                                            Target="_blank"><i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Unit Type
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Unit_Type" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-2">
                                                                    Remarks
                                                                </label>
                                                                <div class="col-sm-3">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Remark" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of FFCI
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_FFCI_Date" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                                <label for="Iname" class="col-sm-2">
                                                                </label>
                                                                <div class="col-sm-3">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Date of Production Commencement
                                                                </label>
                                                                <div class="col-sm-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Prod_Comm" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-3">
                                                                    Reason(s) for Delay in Implementation
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <span class="colon">:</span>
                                                                    <asp:Label ID="Lbl_Delay_Reason" runat="server" CssClass="form-control-static"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <h2 style="text-align: center; border-bottom: 1px solid #dedede; border-top: 1px solid #dedede;">
                                                            Supporting Document List</h2>
                                                        <asp:GridView ID="Grd_Document" runat="server" class="table table-bordered table-hover"
                                                            DataKeyNames="vchDocDesc" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="Grd_Document_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SlNo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="7%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Document Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_Doc_Desc" runat="server" Text='<%# Eval("vchDocDesc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="View Document">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="Hyp_View_Doc" runat="server" Target="_blank" ToolTip="Click Here to View Uploaded Document !!">View Document</asp:HyperLink>
                                                                        <asp:HiddenField ID="Hid_File_Name" runat="server" Value='<%# Eval("vchFileName") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="35%"></ItemStyle>
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
