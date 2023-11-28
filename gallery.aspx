<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gallery.aspx.cs" Inherits="gallery" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html>
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title>SWP(Single Window Portal)</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />

        <div class="navigatorheader-div aboutheadernav">
            <div class="col-sm-12">
                <ul class="breadcrumb">
                    <li><a href="Default.aspx" title="Home page"><i class="fa fa-home"></i></a></li>
                    <li>Gallery</li></ul>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="gallery-div content-form-section">
            <div class="">
                <div class="col-sm-8 padding-right10 padding-left10">
                    <h3>
                        Gallery</h3>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-md-4 col-sm-6 gallery-top g-top">
                                <div class="grid-ga">
                                    <%--   <a href="GalleryImages/<%#Eval("vchImage") %>" class="b-link-stripe b-animate-go  thickbox">--%>
                                    <a href="Portal/ImageGallery/M_<%#Eval("vchImage") %>" class="b-link-stripe b-animate-go  thickbox">
                                        <figure class="effect-img">
						<asp:HiddenField runat="server" ID="hid1" Value='<%#Eval("vchImage") %>' />
                                <asp:Image ID="Image1" runat="server"  alt=""/>
						<figcaption>
							<%--<h4><i class="fa fa-expand"></i><br />Industry wide</h4>--%>
                            <h4><i class="fa fa-expand"></i><asp:HiddenField runat="server" ID="hidLiteral" Value='<%#Eval("vchImgDescription") %>' />
                            <asp:Literal ID="Literal1" runat="server" Visible="false"></asp:Literal>
						</figcaption>			
					</figure>
                                    </a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="col-sm-4">
                    <uc4:rightpanel ID="rightpanel" runat="server" />
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    <script>
        $(function () {
            $('.grid-ga a').Chocolat();
        });

     
    </script>
    </form>
</body>
</html>
