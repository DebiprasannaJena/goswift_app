<%--'*******************************************************************************************************************
' File Name         : DepartmentClearance.aspx
' Description       : Show the clearance details of Investors from various departments
' Created by        : AMit Sahoo
' Created On        : 30th June 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartmentClearance.aspx.cs"
    Inherits="DepartmentClearance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuservices').addClass('active');
            $("#BtnApplyMultiple").click(function () {
                var chkboxrowcount = $("#<%=GrdInternalService.ClientID%> input[id*='ChkBxSelect']:checkbox:checked").size();
                if (chkboxrowcount == 0) {
                    jAlert("<strong>Please select at least one check box to proceed !</strong>");
                    return false;
                }
                return true;
            });
        })
    </script>
    <style type="text/css">
        .search-sec {
            padding: 20px 20px 10px;
        }

        .bindlabel {
            border: 1px solid #cccccc;
            padding: 3px 10px;
            margin-top: 0px !important;
        }
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #fbfbfb;
            border: 3px solid #AC183E;
            margin: 0px;
        }

            .modalPopup .mhead {
                padding: 5px 5px;
                border-bottom: 1px solid #ccc;
                background: #AC183E;
                color: #fff;
            }

                .modalPopup .mhead h4 {
                    display: inline-block;
                }

                .modalPopup .mhead a {
                    float: right;
                    color: #fff;
                    text-decoration: none;
                }

            .modalPopup .mbody {
                padding: 30px 15px;
            }

            .modalPopup .mFooter {
                padding: 15px;
                text-align: right;
                border-top: 1px solid #e5e5e5;
            }


        .radiodiv {
            padding: 10px 0px 20px;
        }

        .Confdiv {
            padding: 25px 120px 20px;
        }

        #PanelIdco h4 {
            font-size: 17px;
            font-weight: bold;
            padding-bottom: 12px;
        }

        .radio-inline label {
            display: inline-block;
            padding-right: 20px;
            padding-left: 12px;
        }

        .reglogin {
            padding: 25px;
        }

            .reglogin p {
                text-align: justify;
            }

            .reglogin a {
                color: #0088cc;
                text-decoration: none;
            }

                .reglogin a:hover {
                    color: #159f45;
                }

        .popBox {
            position: absolute;
            -webkit-box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            -moz-box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            box-shadow: 0px 2px 7px 0px rgba(50, 50, 50, 0.65);
            background: #fffdef;
            padding: 8px;
            border: 1px solid #ddd;
            width: 93%;
            left: 15px;
            font-size: 14px !important;
        }

        #pop-up {
            top: 120px;
        }

        #pop-up1 {
            top: 120px;
        }

        #pop-up2 {
            top: 100px;
        }

        .row {
            margin-left: -15px;
            margin-right: 0;
        }

        .navbar-inverse {
            background-color: none !important;
            border-color: none !important;
        }

        .portlet-sec {
            margin: 16px 15px 8px;
            padding: 5px;
            border-radius: 2px;
        }

            .portlet-sec h3 {
                text-transform: uppercase;
                font-size: 20px;
            }

                .portlet-sec h3 span {
                    font-weight: 600;
                    color: #ac2d00;
                    padding: 0px 4px;
                }

        #info {
            font-size: 18px;
            color: #555;
            text-align: center;
            margin-bottom: 25px;
        }

        a {
            color: #074E8C;
        }

        .scrollbar {
            margin-left: 30px;
            float: left;
            height: 300px;
            width: 65px;
            background: #F5F5F5;
            overflow-y: scroll;
            margin-bottom: 25px;
        }

        .force-overflow {
            min-height: 450px;
        }

        #wrapper {
            text-align: center;
            width: 500px;
            margin: auto;
        }

        .style-4::-webkit-scrollbar-track {
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
            background-color: #F5F5F5;
        }

        .style-4::-webkit-scrollbar {
            width: 10px;
            background-color: #F5F5F5;
        }

        .style-4::-webkit-scrollbar-thumb {
            background-color: #a5a0a0;
            border: none;
        }

        .table-height {
            max-height: 322px !important;
            padding: 11px 0;
        }

        .mt-0 {
            margin-top: 0 !important;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="registration-div investors-bg">
                <div class="">
                    <div id="exTab1">
                        <div class="investrs-tab">
                            <uc4:investoemenu ID="ineste" runat="server" />
                        </div>
                        <div class="form-sec">
                            <div class="form-header">
                                <a href="ApplicationDetails.aspx" title="Application Details" class="pull-right proposalbtn">Application Details</a>
                                <a href="DepartmentClearance.aspx" title="Drafted Proposals" class="pull-right proposalbtn active">Apply Service</a>
                                <a href="DraftedServices.aspx" title="Drafted Services" class="pull-right proposalbtn">Draft Services</a>
                                <h2>Apply Service
                                </h2>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="form-body">
                                        <div class="form-group">
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Unit Name
                                                </label>
                                                <div class="col-sm-4">
                                                    <span class="colon">:</span>
                                                    <asp:Label ID="LblIndustryName" CssClass="bindlabel" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div id="divProposalNo" runat="server">
                                                    <label class=" col-sm-2">
                                                        Proposal No.</label>
                                                    <div class="col-sm-4">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="DdlProposal" runat="server" CssClass="form-control" AutoPostBack="True"
                                                            OnSelectedIndexChanged="DdlProposal_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                                        
                                        <hr />
                                        <h4 class="margin-top10 margin-bottom10 text-red">Internal Services</h4>
                                        <div class="table-responsive" id="divGrd">
                                            <asp:GridView ID="GrdInternalService" runat="server" CssClass="table table-bordered table-striped bg-white"
                                                AllowPaging="false" AutoGenerateColumns="False" OnRowDataBound="GrdInternalService_RowDataBound"
                                                EmptyDataText="No Record(s) Found" CellPadding="4" DataKeyNames="intServiceId,intStatus,strProposalId,Int_ServiceType,Str_ExtrnalServiceUrl,Dec_Amount,intExternalType"
                                                GridLines="None">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkBxSelect" runat="server" />
                                                            <asp:HiddenField ID="Hid_Disable_Status" runat="server" Value='<%# Eval("str_ApplicationStatus") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDept" runat="server" Text='<%# Eval("strdeptname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="28%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Services">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblService" runat="server" Text='<%# Eval("strServiceName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Application Fee" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApplicationFee" runat="server" Text='<%# Eval("Str_Amount") %>'></asp:Label>
                                                            <asp:HiddenField ID="Hid_Service_Type" runat="server" Value='<%# Eval("Int_ServiceType") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apply Now" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypApply" runat="server" NavigateUrl="#" Text="Apply" ForeColor="White"
                                                                ToolTip="Click Here to Apply" Visible='<%#(Convert.ToBoolean(Eval("str_ApplicationStatus"))) %>'
                                                                CssClass=" btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <div style="text-align: left;">
                                                <asp:Button ID="BtnApplyMultiple" runat="server" Text="Apply Now" OnClick="BtnApplyMultiple_Click"
                                                    CssClass="btn btn-danger" ToolTip="Click here to Apply Multiple Services at a time" />
                                            </div>
                                        </div>
                                    </div>
                                    <h4 class="margin-top5 margin-bottom10 text-red">External Services</h4>
                                    <div class="table-responsive" id="div1">
                                        <asp:GridView ID="GrdExternalService" runat="server" CssClass="table table-bordered table-striped bg-white"
                                            AllowPaging="false" AutoGenerateColumns="False" OnRowDataBound="GrdExternalService_RowDataBound"
                                            EmptyDataText="No Record(s) Found" CellPadding="4" DataKeyNames="intServiceId,intStatus,strProposalId,Int_ServiceType,Str_ExtrnalServiceUrl,Dec_Amount"
                                            GridLines="None">
                                            <Columns>
                                                <asp:BoundField HeaderText="Sl#." HeaderStyle-Width="4%" />
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDept" runat="server" Text='<%# Eval("strdeptname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="28%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Services">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblService" runat="server" Text='<%# Eval("strServiceName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Application Fee" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApplicationFee" runat="server" Text='<%# Eval("Str_Amount") %>'></asp:Label>
                                                        <asp:HiddenField ID="Hid_Service_Type" runat="server" Value='<%# Eval("Int_ServiceType") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Apply Now">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hypApply" runat="server" NavigateUrl="#" Text="Apply" ForeColor="White"
                                                            ToolTip="Click Here to Apply" Visible='<%#(Convert.ToBoolean(Eval("str_ApplicationStatus"))) %>'
                                                            CssClass=" btn  btn-success btn-sm"><i class="fa fa-check-square"></i></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <%--<h4>Open Modal Popup</h4>--%>
                            <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                            <cc1:ModalPopupExtender ID="DepartmentClsModalPopup" BehaviorID="mpe" runat="server" PopupControlID="pnlPopup"
                                TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="Linkclose">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none; width: 800px;"
                                ToolTip="Important Notes">
                                <div class="mhead">
                                    <asp:LinkButton ID="Linkclose" runat="server"><i class="fa fa-close"></i></asp:LinkButton>
                                    <h4 class="modal-title">Important Notes</h4>
                                </div>
                                <div class="modal-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <h4 class="mt-0">List of Services Selected for Apply</h4>
                                                    <div class="table-responsive style-4  table-height" id="divins">
                                                        <asp:GridView ID="GrdInstruction" runat="server" CssClass="table table-bordered table-striped bg-white" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl#">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Department">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDept" runat="server" Text='<%# Eval("vchDeptName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Service">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblService" runat="server" Text='<%# Eval("vchServiceName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <p style="color: red;"><strong>N.B.:-</strong>   Please note that, you can't add or remove the selected servies during application process.Check the list of selected services before apply.</p>
                                                </div>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="mFooter">
                                    <div class="row">
                                        <div class="col-sm-6 text-left">
                                            Are you sure to continue?
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Button ID="BtnYes" runat="server" Text="Yes" CssClass="btn btn-success" ToolTip="Click Here to Continue." OnClick="BtnYes_Click" />
                                            <asp:Button ID="BtnNo" runat="server" Text="No" CssClass="btn btn-danger" ToolTip="Click Here to Cancel." OnClick="BtnNo_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
