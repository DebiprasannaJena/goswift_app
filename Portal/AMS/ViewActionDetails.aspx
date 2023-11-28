<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewActionDetails.aspx.cs"
    Inherits="SingleWindow_ViewActionDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
     <style>
    body{background:#fff;}
 .form-control-static { padding-top: 5px;display: inline-block;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="col-sm-12">
       <div class="form-group">
    <div class="row">
<label class="col-sm-3"> Project Name</label>
<div class="col-sm-5">
<span class="colon">:</span>
 <asp:Label ID="lblproject" runat="server" CssClass="form-control-static" Text="Label"></asp:Label>
</div>
</div>
</div>
    <div class="form-group">
    <div class="row">
<label class="col-sm-3"> Project Created On</label>
<div class="col-sm-5">
<span class="colon">:</span>   <asp:Label ID="lblAddedOn" CssClass="form-control-static" runat="server" Text="Label"></asp:Label>
</div>
</div>
</div>


 <div class="form-group">
    <div class="row m-b-10">
<label class="col-sm-3">Forward details </label>
<div class="col-sm-9">
<span class="colon">:</span> 
 <div class="table-responsive">
            <asp:GridView ID="GrdProjectCostDtls" runat="server" Width="100%" AutoGenerateColumns="False"
                CssClass="table table-bordered table-condensed table-striped">
                <Columns>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("VCHCOMMENT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Forworded on" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblCost" runat="server" Text='<%# Eval("FORWORDEDDATE") %>' DataFormatString="{0:dd/MM/yyyy hh:mm}"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Time" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCost" runat="server" Text='<%# Eval("FORWORDEDDATE") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                </Columns>
                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:GridView>
        </div>
</div>
</div>
</div>
  <div class="form-group">
    <div class="row ">
<label class="col-sm-3"> SLFC Feedback details</label>
<div class="col-sm-9">
<span class="colon">:</span> 
 <div class="table-responsive">
            <asp:GridView ID="GrdSlfcCommnet" runat="server" Width="100%" AutoGenerateColumns="False"
                CssClass="table table-bordered table-condensed table-striped">
                <Columns>
                    <asp:TemplateField HeaderText="SLFC Members">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("vchFullName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblComment" runat="server" Text='<%# Eval("VCHCOMMENT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments given on" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("DTMCREATEDON") %>' DataFormatString="{0:dd/MM/yyyy hh:mm}"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GM Comments" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblGMComment" runat="server" Text='<%# Eval("VCH_COMMENT_GMSLNA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:GridView>
        </div>
        <asp:Label ID="LblSlfcComnts" runat="server" Text="SLFC members Not given their Feedback yet."></asp:Label>
</div>
</div>
</div>   

    <div class="form-group">
    <div class="row ">
<label class="col-sm-3">Clarification Details </label>
<div class="col-sm-9">
<span class="colon">:</span> 
    <div class="table-responsive">
            <asp:GridView ID="grvClarificationDtls" runat="server" Width="100%" AutoGenerateColumns="False"
                CssClass="table table-bordered table-condensed table-striped">
                <Columns>
                    <asp:TemplateField HeaderText="Sent by GM(SLNA)">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("vchClarification") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sent On" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("dtmSend") %>' DataFormatString="{0:dd/MM/yyyy hh:mm}"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Send To" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("vchFullName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Given by SLFC Members" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblClarificationSLFC" runat="server" Text='<%# Eval("vchClarificationSLFC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Given On" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblClarificationSLFC" runat="server" Text='<%# Eval("dtmClarificationSLFC") %>'
                                DataFormatString="{0:dd/MM/yyyy hh:mm}"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Modified By GM(SLNA)" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblModifiedCmnt" runat="server" Text='<%# Eval("vchClarificationGM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
            </asp:GridView>
        </div>
</div>
</div>
</div>      
       
        <asp:Label ID="LblClarification" runat="server" Text="No Clarification Given.."></asp:Label>
    </div>
    </form>
</body>
</html>
