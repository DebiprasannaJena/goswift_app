<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OFDCInstruction.aspx.cs"
    Inherits="OFDCInstruction" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html>
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title>SWP(Single Window Portal)</title>
    <style>
        .panel-default > .panel-heading
        {
            padding: 8px 15px;
            font-size: 18px;
            text-transform: uppercase;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <br />
            <div class="content-form-section">
                <div class="col-sm-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Information for Enrollment of Film Registration</div>
                        <div class="panel-body text-left">
                            <br />
                            <p>
                                The Government of Odisha has announced a new Film Policy with the sole aim of projecting its artistic, cultural and historical heritage within the country and abroad through the medium of film. 
                                The artistes and the producers of the state and outside of the state will be provided a conducive ecosystem and relevant facilities under the new film policy.

                                For providing a hassle-free and prompt film production related facilities, the state has integrated the film registration process through the GO SWIFT online portal. 
                                For producers interested in shooting their film in the State of Odisha, please fill the form in GO SWIFT. Based on the details shared through the form, 
                                we will contact the producers to facilitate in film production in the state.</p>
                        </div>
                        <br />
                        <br />
                        <br />
                    </div>
                    <div class="text-center">
                        <asp:Button ID="btnnavigate" runat="server" Text="Proceed" class="btn btn-info" OnClick="btnnavigate_Click" />
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
