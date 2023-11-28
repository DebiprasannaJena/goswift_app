<%--'*******************************************************************************************************************
' File Name         : IncentiveReport.aspx
' Description       : To show the summary of incentive applied in dashboard
' Created by        : Ritika lath
' Created On        : 12th December 2017
' Modification History:
'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                 
'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncentiveReport.aspx.cs"
    Inherits="Portal_Incentive_IncentiveReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/flash.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="Stylesheet" type="text/css" />
    <script src="../js/jQuery-2.1.3.min.js"></script>
    <style>
        .50width
        {
            width: 50%;
            text-align: left;
        }
        .25width
        {
            width: 25%;
            text-align: right;
        }
        .table
        {
            margin-bottom: 0px;
        }
    </style>
    <script>
        $(document).ready(function () {

            $('#grdIncentiveMain_pnlPhase_0').click(function () {

                $(this).find("span").toggleClass('fa-plus-square-o fa-minus-square-o');
            });

            $('.firstlbllink').click(function () {

                $(this).find("span").toggleClass('fa-plus-square-o fa-minus-square-o');
            });
            $('.secondLblLink').click(function () {

                $(this).find("span").toggleClass('fa-plus-square-o fa-minus-square-o');
            });

        });
    </script>
    <style>
    .header-table tr td{font-size:15px;color: #797979;}
    .f20{font-size: 20px;}
    .f18{font-size: 16px;}
    .f16{font-size: 14px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdIncentiveMain" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover"
                    Width="100% " ShowHeader="true" DataKeyNames="intMainId" OnRowDataBound="grdIncentiveMain_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div>
                                    <table style="width: 100%;">
                                        <tr valign="middle">
                                            <th style="width:5%;">
                                            </th>
                                            <th style="width: 40%;">
                                            </th>
                                            <th style="width: 15%; text-align: right;">
                                                Count
                                            </th>
                                            <th style="width: 40%; text-align: right;">
                                                (<i class="fa fa-rupee"></i>)Amount in Lakhs
                                            </th>
                                        </tr>
                                    </table>
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <table style="width: 100%;" class="header-table">
                                        <tr valign="middle">
                                            <td style="width: 5%;">
                                                <asp:Panel ID="pnlPhase" runat="server">
                                                    <span class="fa fa-plus-square-o" id="spPhase" runat="server"></span>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 40%; text-align: left;">
                                              <asp:Label ID="lblName" runat="server" Text='<%#Eval("strName") %>'></asp:Label>
                                            </td>
                                            <td style="width: 15%; text-align: right;">
                                                <asp:Label ID="lblCount" runat="server"  Text='<%#Eval("intCount") %>' Visible="false"></asp:Label></b>
                                                <asp:HyperLink ID="hypCount" runat="server" CssClass="f20" Target="_blank" Visible="false" Text='<%#Eval("intCount") %>'></asp:HyperLink>
                                            </td>
                                            <td style="width: 40%; text-align: right;">
                                               <asp:Label ID="lblAmount" runat="server" CssClass="f20"  Text='<%#Eval("decAmount") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                
                                    <asp:Panel ID="pnlActivity" runat="server" >
                               
                                        <asp:GridView AutoGenerateColumns="false" ID="grdFirstLevel" runat="server" CssClass="table table-bordered table-hover"
                                            EmptyDataRowStyle-CssClass="noRecordFound" BorderWidth="0" ShowHeader="false"
                                            EmptyDataText="No Records Found..." Width="100%" DataKeyNames="intMainId" OnRowDataBound="grdFirstLevel_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div>
                                                            <table style="width: 100%;font-size:14px!important">
                                                                <tr valign="middle">
                                                                    <td style="width: 5%;">
                                                                        <asp:Panel ID="pnlFirstLevelLink" class="firstlbllink" runat="server">
                                                                            <span class="fa fa-plus-square-o" id="spFirstLevelLink" runat="server"></span>
                                                                        </asp:Panel>
                                                                        <asp:HiddenField ID="hdnFirstLevelMainId" runat="server" Value='<%#Eval("intMainId") %>' />
                                                                    </td>
                                                                    <td style="width: 40%; text-align: left;">
                                                                        <%#Eval("strName") %>
                                                                    </td>
                                                                    <td style="width: 15%; text-align: right;">
                                                                        <asp:Label ID="lblCount" runat="server" Text='<%#Eval("intCount") %>' Visible="false"></asp:Label>
                                                                        <asp:HyperLink ID="hypCount" runat="server" CssClass="f18" Target="_blank" Visible="false" Text='<%#Eval("intCount") %>'></asp:HyperLink>
                                                                    </td>
                                                                    <td style="width: 40%; text-align: right;" class="f18">
                                                                        <%#Eval("decAmount") %>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="borderTable">
                                                            <asp:Panel ID="pnlFirstLevelDetails" runat="server">
                                                                <asp:GridView AutoGenerateColumns="false" ID="grdSecondLevel" runat="server" CssClass="table table-bordered table-hover"
                                                                    EmptyDataRowStyle-CssClass="noRecordFound" BorderWidth="0" ShowHeader="false"
                                                                    EmptyDataText="No Records Found..." Width="100%" DataKeyNames="intMainId" OnRowDataBound="grdSecondLevel_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <div>
                                                                                    <table style="width: 100%;font-size:13px!important; left:5px">
                                                                                        <tr valign="middle">
                                                                                            <td style="width: 5%;">
                                                                                                <asp:Panel ID="pnlSecondLevelLink" CssClass="secondLblLink" runat="server">
                                                                                                    <span class="fa fa-plus-square-o" id="spSecondLevelLink" runat="server"></span>
                                                                                                </asp:Panel>
                                                                                                <asp:HiddenField ID="hdnSecondLevelMainId" runat="server" Value='<%#Eval("intMainId") %>' />
                                                                                            </td>
                                                                                            <td style="width: 40%; text-align: left;">
                                                                                                <%#Eval("strName") %>
                                                                                            </td>
                                                                                            <td style="width: 15%; text-align: right;">
                                                                                                <asp:Label ID="lblCount" runat="server" Text='<%#Eval("intCount") %>' Visible="false"></asp:Label>
                                                                                                <asp:HyperLink ID="hypCount" runat="server" CssClass="f16" Target="_blank" Visible="false" Text='<%#Eval("intCount") %>'></asp:HyperLink>
                                                                                            </td>
                                                                                            <td style="width: 40%; text-align: right;"  class="f16">
                                                                                                <%#Eval("decAmount") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                                <div class="borderTable">
                                                                                    <asp:Panel ID="pnlSecondLevelDetails" runat="server">
                                                                                        <asp:GridView AutoGenerateColumns="false" ID="grdThirdLevel" runat="server" CssClass="table table-bordered table-hover"
                                                                                            EmptyDataRowStyle-CssClass="noRecordFound" BorderWidth="0" ShowHeader="false"
                                                                                            EmptyDataText="No Records Found..." Width="100%" DataKeyNames="intMainId" OnRowDataBound="grdThirdLevel_RowDataBound">
                                                                                            <Columns>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <table style="width: 100%;">
                                                                                                            <tr>
                                                                                                                <td style="width: 5%;">
                                                                                                                    <asp:HiddenField ID="hdnThirdLevelMainId" runat="server" Value='<%#Eval("intMainId") %>' />
                                                                                                                </td>
                                                                                                                <td style="width: 40%; text-align: left;">
                                                                                                                    <%#Eval("strName") %>
                                                                                                                </td>
                                                                                                                <td style="width: 15%; text-align: right;">
                                                                                                                    <asp:Label ID="lblCount" runat="server" Text='<%#Eval("intCount") %>' Visible="false"></asp:Label>
                                                                                                                    <asp:HyperLink ID="hypCount" runat="server" Target="_blank" Visible="false" Text='<%#Eval("intCount") %>'></asp:HyperLink>
                                                                                                                </td>
                                                                                                                <td style="width: 40%; text-align: right;">
                                                                                                                    <%#Eval("decAmount") %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                </div>
                                                                                <cc1:CollapsiblePanelExtender ID="cPanelSecondLevel" runat="Server" TargetControlID="pnlSecondLevelDetails"
                                                                                    CollapsedSize="0" Collapsed="True" ExpandControlID="pnlSecondLevelLink" CollapseControlID="pnlSecondLevelLink"
                                                                                    AutoCollapse="False" AutoExpand="False" ScrollContents="false" ImageControlID="imgSecondLevel"
                                                                                    ExpandedImage="../images/minus.gif" CollapsedImage="../images/plus.gif" ExpandDirection="Vertical" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </div>
                                                        <cc1:CollapsiblePanelExtender ID="cPanelFirstLevel" runat="Server" TargetControlID="pnlFirstLevelDetails"
                                                            CollapsedSize="0" Collapsed="True" ExpandControlID="pnlFirstLevelLink" CollapseControlID="pnlFirstLevelLink"
                                                            AutoCollapse="False" AutoExpand="False" ScrollContents="false" ImageControlID="imgFirstLevel"
                                                            ExpandedImage="../images/minus.gif" CollapsedImage="../images/plus.gif" ExpandDirection="Vertical" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        
                                    </asp:Panel>
                                  
                                </div>
                                <cc1:CollapsiblePanelExtender ID="cPanelMain" runat="Server" TargetControlID="pnlActivity"
                                    CollapsedSize="0" Collapsed="True" ExpandControlID="pnlPhase" CollapseControlID="pnlPhase"
                                    AutoCollapse="False" AutoExpand="False" ScrollContents="false" ImageControlID="imgCollapsible"
                                    ExpandedImage="../images/minus.gif" CollapsedImage="../images/plus.gif" ExpandDirection="Vertical" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
