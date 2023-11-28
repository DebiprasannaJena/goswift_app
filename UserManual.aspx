<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManual.aspx.cs" Inherits="UserManual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

        });

    </script>
    <style>
    .pagination-grid table {
    float: right!important;
    width:auto;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:header ID="header" runat="server" />
           <div class="container wrapper">
        <div class="navigatorheader-div aboutheadernav">
            <div class="col-sm-12">
                <ul class="breadcrumb">
                    <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                    <li>User Manual</li>
                </ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="content-form-section">
            <div class="col-sm-12">
                <div class="aboutcontent-sec">
                    <h3>
                        User Manual</h3>
                  <%--  <div class="pageResult text-right">
                        <i class="fa fa-list" aria-hidden="true" id="icon" runat="server"></i>
                        <asp:LinkButton ID="lbtnAll" Visible="true" runat="server" Text="All" OnClick="lbtnAll_Click"></asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblPaging" runat="server"></asp:Label></div>--%>
                    <asp:GridView ID="grdVwUsrManual" runat="server" CssClass="table table-bordered table-striped bg-white"
                        DataKeyNames="strAttachment"  AutoGenerateColumns="false"  Width="100%"
                        OnRowDataBound="grdVwUsrManual_RowDataBound" 
                        onpageindexchanging="grdVwUsrManual_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="SI#." HeaderStyle-Width="40px">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Name" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblServcName" Text='<%#Eval("StrServicename") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Manual" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyprDownload" runat="server" Target="_blank" Text="Download"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <h2 class="nodata" style="color: #CCC; border-bottom: 0;">
                                No Data Found ...</h2>
                        </EmptyDataTemplate>
                        <PagerStyle CssClass="pagination-grid no-print" />
                    </asp:GridView>
                </div>
            </div>
           
            <div class="clearfix">
            </div>
        </div>
    </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
