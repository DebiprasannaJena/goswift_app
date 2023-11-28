<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceInstruction1.aspx.cs"
    Inherits="ServiceInstruction1" %>

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
     <link href="css/global.css" rel="stylesheet" type="text/css" />

    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menuservices').addClass('active');
           $('.table').addClass('table-striped bg-white');
        });
        function SetTarget() {
            document.forms[0].target = "";
        }
        function RemoveTarget() {
            document.forms[0].target = "";
        }
        
    </script>
    <style type="text/css">
        .guidelines
        {
            display: table;
            width: 100%;
            min-height: 200px;
            text-align: center;
            background: #eee;
            margin-bottom: 10px;
        }
        
        .guidelines p
        {
            display: table-cell;
            vertical-align: middle;
            font-size: 18px;
            letter-spacing: 1px;
        }
        
        .guidelinesdetails
        {
        }
        
       
        
        .instructiondiv
        {
            padding: 20px 40px;
        }
        
        .instructiondiv h2
        {
            color: #b1020a;
        }
        
        .minheight300
        {
            min-height: 300px;
        }
        
        .links
        {
            margin-top: 0px;
            right: 14px;
            background-color: #cd1c24;
            border-color: #cd1c24;
            color: #fff;
            font-size: 12px;
            padding: 4px 10px;
        }
        
        .links:hover, .links:focus
        {
            background: #bd1018;
            color: #fff;
        }
          .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        
        .modalPopup
        {
            background-color: #fbfbfb;
            border: 3px solid #AC183E;
            margin: 0px;
        }
        
        .modalPopup .mhead
        {
            padding: 5px 5px;
            border-bottom: 1px solid #ccc;
            background: #AC183E;
            color: #fff;
        }
        
        .modalPopup .mhead h4
        {
            display: inline-block;
        }
        
        .modalPopup .mhead a
        {
            float: right;
            color: #fff;
            text-decoration: none;
        }
        
        .modalPopup .mbody
        {
            padding: 30px 15px;
        }
        
        .modalPopup .mFooter
        {
            padding: 15px;
            text-align: right;
            border-top: 1px solid #e5e5e5;
        }
        
        
        .radiodiv
        {
            padding: 10px 0px 20px;
        }
        
        .Confdiv
        {
            padding: 25px 120px 20px;
        }
        
        #PanelIdco h4
        {
            font-size: 17px;
            font-weight: bold;
            padding-bottom: 12px;
        }
        
        .radio-inline label
        {
            display: inline-block;
            padding-right: 20px;
            padding-left: 12px;
        }
        
        .reglogin
        {
            padding: 25px;
        }
        
        .reglogin p
        {
            text-align: justify;
        }
        
        .reglogin a
        {
            color: #0088cc;
            text-decoration: none;
        }
        
        .reglogin a:hover
        {
            color: #159f45;
        }
        
        .popBox
        {
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
        
        #pop-up
        {
            top: 120px;
        }
        
        #pop-up1
        {
            top: 120px;
        }
        
        #pop-up2
        {
            top: 100px;
        }
        
        .row
        {
            margin-left: -15px;
            margin-right: 0;
        }
        
        .navbar-inverse
        {
            background-color: none !important;
            border-color: none !important;
        }
        
        .portlet-sec
        {
            margin: 16px 15px 8px;
            padding: 5px;
            border-radius: 2px;
        }
        
        .portlet-sec h3
        {
            text-transform: uppercase;
            font-size: 20px;
        }
        
        .portlet-sec h3 span
        {
            font-weight: 600;
            color: #ac2d00;
            padding: 0px 4px;
        }
                     .registration-div{
    padding: 0px 10px 0px 0px!important;
    background: #fff!important;
}
    </style>
  
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <%-- <uc2:header ID="header" runat="server" />--%>
  <%--  <div class="container wrapper">--%>
        <div class="registration-div">
            <div class="">
                <div id="exTab1">
                    <%--<div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                    </div>--%>
                    
                    <div class="form-sec">
                        <%--<div class="form-header">
                            <asp:HyperLink class="btn pull-right links" ID="HyprLnk" Target="_blank" data-toggle="tooltip"
                                runat="server" title="Download User Manual"><i class="fa fa-download"></i>&nbsp;User Manual</asp:HyperLink>
                            <h2>
                                <asp:Label ID="lblService" runat="server" Text=""></asp:Label>
                                <%--(Code-Industry Name)</h2>
                        </div>--%>
                        <div class="form-body">
                           <%-- <div class="investrs-tab services--tabs" runat="server" id="myNavbar">
                            </div>--%>
                            <div class="guidelinesdetails">
                                <div class="form-group ">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="minheight300">
                                                <%--<h4>
                                                Guideline</h4>--%>
                                                <div id="divabout" runat="server" class="instructiondiv">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 text-center margin-top10">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="BtnProceed" runat="server" Text="Proceed" CssClass="btn btn-success" OnClientClick="parent.checkurl(1)"
                                        OnClick="BtnProceed_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <br />
                        </div>
                        <%--<h4>Open Modal Popup</h4>--%>
                        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                        <cc1:ModalPopupExtender ID="ServiceModalPopup" BehaviorID="mpe" runat="server" PopupControlID="pnlPopup"
                            TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="Linkclose">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;
                            width: 800px; height: 310px;" ToolTip="Important Notes">
                            <div class="mhead">
                                <asp:LinkButton ID="Linkclose" runat="server" OnClientClick="RemoveTarget();"><i class="fa fa-close"></i></asp:LinkButton>
                                <h4 class="modal-title">
                                    Important Notes</h4>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <ol style="padding-left: 20px;">
                                                    <li>By clicking on the <span style="font-weight: bold; color: Red;">"YES"</span> button,
                                                        you will be redirected to an external website,
                                                        <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>. Fill the
                                                        details in that portal and submit the application.</li>
                                                    <li>Your application will be in <span style="font-weight: bold; color: Red;">"Draft"</span>
                                                        mode in GOSWIFT portal. In case of any failure, log in to GOSWIFT portal and continue
                                                        the application from Draft page.</li>
                                                    <li>Once the application submitted successfully in an external portal, the status information
                                                        will be exchanged to GOSWIFT portal. You can track all the progresses in GOSWIFT
                                                        portal.</li>
                                                </ol>
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
                                        <asp:Button ID="BtnYes" runat="server" Text="Yes" CssClass="btn btn-success" OnClientClick="SetTarget();"
                                            OnClick="BtnYes_Click" ToolTip="Click Here to Continue." />
                                        <asp:Button ID="BtnNo" runat="server" Text="No" CssClass="btn btn-danger" OnClick="BtnNo_Click"
                                            OnClientClick="RemoveTarget();" ToolTip="Click Here to Cancel." />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
 <%--   </div>--%>
   <%-- <uc3:footer ID="footer" runat="server" />--%>
    </form>
</body>
</html>
