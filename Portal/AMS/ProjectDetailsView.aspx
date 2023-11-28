<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectDetailsView.aspx.cs"
    Inherits="SingleWindow_ProjectDetailsView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IPICOL Agenda Monitoring System</title>
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <style>
        body
        {
            background: #fff;
        }
        .form-control-static
        {
            padding-top: 5px;
            display: inline-block;
        }
    </style>
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/bootstrap.min.js" type="text/javascript" language="javascript"></script>
    <script src="../js/loadComponent.js" language="javascript" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Project Name</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:Label ID="lblName" CssClass="form-control-static" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Project Cost Details</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:GridView ID="GrdProjectCostDtls" runat="server" Width="100%" AutoGenerateColumns="False"
                    CssClass="table table-bordered" ShowFooter="True" OnRowDataBound="GrdProjectCostDtls_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Details Description">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("VCH_COST_DTLS_DESC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost (Rs in Crores)" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblCost" runat="server" Text='<%# Eval("DEC_COST") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Financing Details(Rs in crores)</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:GridView ID="GrdFinDtls" runat="server" Width="100%" AutoGenerateColumns="False"
                    CssClass="table table-bordered" ShowFooter="true" OnRowDataBound="GrdFinDtls_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("VCH_FIN_DTLS_DESC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("DEC_FIN_COST") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGTotal" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle Font-Bold="True" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Financing Description</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:Label ID="lblFinDescription" CssClass="form-control-static" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Land</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:Label ID="lblLand" CssClass="form-control-static" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Power</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:Label ID="lblPower" CssClass="form-control-static" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Water</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:Label ID="lblWater" CssClass="form-control-static" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Source</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:Label ID="lblSource" CssClass="form-control-static" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Direct Employment</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:Label ID="lblDirectEmployment" CssClass="form-control-static" runat="server"
                    Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Contractual Employment</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:Label ID="lblContractual" CssClass="form-control-static" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Implementation Period</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:Label ID="lblMonths" CssClass="form-control-static" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="row">
            <label class="col-sm-4">
                Raw Materials Source</label>
            <div class="col-sm-8">
                <span class="colon">:</span>
                <asp:GridView ID="GrvSource" runat="server" CssClass="table table-bordered" Width="100%"
                    AutoGenerateColumns="False" GridLines="None">
                    <Columns>
                        <asp:TemplateField HeaderText="Materials">
                            <ItemTemplate>
                                <asp:Label ID="lblMaterials" runat="server" Text='<%# Eval("Materials") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Source">
                            <ItemTemplate>
                                <asp:Label ID="lblSource" runat="server" Text='<%# Eval("Source") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
