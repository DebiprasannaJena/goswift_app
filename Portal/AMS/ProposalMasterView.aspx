<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProposalMasterView.aspx.cs"
    Inherits="SingleWindow_ProposalMasterView" %>

<!DOCTYPE html>
<html >
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
    <style>
     .listX ul li
        {
            list-style-type: disc;
            margin-left: 15px;
        }
        .listX ol
        {
            list-style-type: decimal;
        }
        .listX ol li
        {
            list-style-type: decimal;
            margin-left: 15px;
        }
        .listX li
        {
              list-style-type: upper-roman;
        }
    
    </style>
</head>
<body>
    <form id="form1" runat="server">

     <div class="form-group">
   <div class="row">
   <label class="col-sm-3"> Project Name </label>
   <div class="col-sm-4">
   <span class="colon">:</span>
   <asp:Label ID="lblName" CssClass="form-control-static" runat="server" Text=""></asp:Label>
   </div>
   </div>
   </div>
    

      <div class="form-group">
   <div class="row">
   
   <div class="col-sm-12">
  
      <asp:GridView ID="GrdProposal" CssClass="table table-bordered" runat="server" Width="100%"  
                        AutoGenerateColumns="False" onrowdatabound="GrdProposal_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <ItemTemplate>
                                    <span>
                                        <%#Container.DataItemIndex + 1%>
                                    </span>
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProposalDtl" HeaderText="Proposal in brief">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="paging NOPRINT" />
                        <PagerSettings Mode="NumericFirstLast" NextPageText="Next" FirstPageText="First"
                            LastPageText="Last" PreviousPageText="Prev" Position="Bottom" />
                    </asp:GridView>
                    <asp:Label ID="lblMessage" runat="server" Text="No Record(s) Found!!!" Visible="false"
                        CssClass="lblMessage" />
   </div>
   </div>
   </div>

    <table border="0" cellpadding="0" cellspacing="0" class="table" width="100%">
 
        <tr>
            <td colspan="3">
                <div class="viewTable listX" id="viewTable">
                 
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
