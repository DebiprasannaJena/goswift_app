<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectView.aspx.cs" Inherits="SingleWindow_ProjectView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IPICOL Agenda Monitoring System</title>
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
   <label class="col-sm-3"> Project Title</label>
   <div class="col-sm-4">
   <span class="colon">:</span>
    <asp:Label ID="lblTitle" CssClass="form-control-static" runat="server" Text=""></asp:Label>
   </div>
   </div>
   </div>
    <div class="form-group">
   <div class="row">
   <label class="col-sm-3"> Project Name</label>
   <div class="col-sm-4">
   <span class="colon">:</span>
   <asp:Label ID="lblName" CssClass="form-control-static" runat="server" Text=""></asp:Label>
   </div>
   </div>
   </div>
    <div class="form-group">
   <div class="row">
   <label class="col-sm-3">Date of Application over e-Biz Portal</label>
   <div class="col-sm-4">
   <span class="colon">:</span>
   <asp:Label ID="lblDate" CssClass="form-control-static" runat="server" Text=""></asp:Label>
   </div>
   </div>
   </div>
    <div class="form-group">
   <div class="row">
   <label class="col-sm-3">Sector</label>
   <div class="col-sm-4">
   <span class="colon">:</span>
    <asp:Label ID="lblSector" CssClass="form-control-static" runat="server" Text=""></asp:Label>
   </div>
   </div>
   </div>
   <div class="form-group">
   <div class="row">
   <label class="col-sm-3">Location of Project</label>
   <div class="col-sm-6">
   <span class="colon">: </span> <div id="placeholder" runat="server"> </div>

    <asp:GridView ID="GrdCapacity" CssClass="table table-bordered" runat="server" Width="100%" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="VCHPRODUCT" HeaderText="Product"></asp:BoundField>
                        <asp:BoundField DataField="vchCapacity" HeaderText="Capacity"></asp:BoundField>
                    </Columns>
                </asp:GridView>

   </div>
   </div>
   </div>

    <div class="form-group">
   <div class="row">
   <label class="col-sm-3">Category</label>
   <div class="col-sm-4">
   <span class="colon">:</span>
   <asp:Label ID="lblCategory" CssClass="form-control-static" runat="server" Text=""></asp:Label>
   </div>
   </div>
   </div>
 <div class="form-group">
   <div class="row">
   <label class="col-sm-3">Type</label>
   <div class="col-sm-4">
   <span class="colon">:</span>
   <asp:Label ID="lblType" CssClass="form-control-static" runat="server" Text=""></asp:Label>
   </div>
   </div>
   </div> 
   
     <div class="form-group">
   <div class="row">
   <label class="col-sm-3"> Board Of Directors</label>
   <div class="col-sm-6">
   <span class="colon">:</span>
           <asp:Repeater ID="rptDirectors" runat="server">
                    <HeaderTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="table table-bordered" width="100%">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th>
                                <%# Container.ItemIndex + 1 %>
                            </th>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "VCHPROMOTOR")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
   </div>
   </div>
   </div> 
   
    <div class="form-group">
   <div class="row">
   <label class="col-sm-3">  Direct Existing Business Intrest Of the compnay</label>
   <div class="col-sm-6">
   <span class="colon">:</span>
         <asp:Repeater ID="rptExisting" runat="server">
                    <HeaderTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="table table-bordered" width="100%">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th>
                                <%# Container.ItemIndex + 1 %>
                            </th>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "VCHPROMOTOR")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
   </div>
   </div>
   </div> 
   
  
   </div>
    
  

    </form>
</body>
</html>
