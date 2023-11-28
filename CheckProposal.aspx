<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckProposal.aspx.cs" Inherits="CheckProposal" %>

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
            $('.department,.plLesi').addClass('active');
            $('#printbtn').click(function () {
                window.print();
            })
        });

    </script>
    <style>
        .deptlist li a span
        {
            float: none !important;
            margin-right: 12px;
        }
        .aboutcontent-sec h2
        {
            font-size: 1.2em;
            color: #3c3c3c;
            margin: 0;
            padding: .3em 0em;
            border-bottom: 2px solid #c09e46;
        }
        .deptlist
        {
            min-height: 420px !important;
            overflow: hidden;
            overflow-y: scroll;
        }
        .rightnews ul li.active a
        {
            color: #f00;
        }
        .backbtn
        {
            padding: 4px 6px;
            color: #cd1c24;
            border: 1px solid #cd1c24;
            border-radius: 2px;
        }
        .backbtn:hover, .backbtn:focus
        {
            color: #fff;
            background: #cd1c24;
        }
        .links
        {
            position: fixed;
            bottom: 15px;
            right: 60px;
        }
        .links .btn
        {
            background-color: #cd1c24;
            border-color: #cd1c24;
            color: #fff;
            margin-left: 5px;
            font-size: 14px;
            padding: 4px 10px;
            display: inline-block;
        }
        .links .btn:hover, .links .btn:focus
        {
            background: #af0b13;
            color: #fff;
        }
        .applicationdtls
        {
            padding: 4px 30px;
            margin-top: 5px;
            display: block;
            background: #cd1c24;
            text-align: center;
            margin-left: 11px;
            color: #fff;
            float: right;
            margin-top: 5px;
            right: 14px;
        }
        .applicationdtls:hover, .applicationdtls:focus
        {
            text-decoration: none;
            color: #fff;
        }
        .applicationdtls span
        {
            font-size: 24px;
            line-height: 28px;
            color: #f9cd5d;
        }
        .applicationdtls .fa
        {
            color: #f9cd5d;
            margin-right: 8px;
        }
        @media print
        {
            .rightnews, .aboutheadernav, .links, .applicationdtls
            {
                display: none;
            }
            .aboutcontent-sec table
            {
                width: 100% !important;
            }
            .aboutcontent-sec h3
            {
                text-align: left;
            }
            .aboutcontent-sec h3 span
            {
                width: 100%;
                text-align: left;
            }
        }
    </style>
     <script language="javascript" type="text/javascript">

        function Validation() {
            debugger;
            if (DropDownValidation('ddlproposal', '0', 'Proposal Number', 'SWP') == false) {
                return false;
            }
            }
            </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <uc2:header ID="header" runat="server" />
        <div class="navigatorheader-div aboutheadernav">
            <%--  <div class="col-sm-10">
                             
                    
            </div>
       
            <div class="clearfix">
            </div>--%>
        </div>
        <div class="content-form-section">
            <div class="col-sm-2">
                <div class="rightpanel laboutesi">
                </div>
            </div>
        </div>
        <div class="aboutcontent-sec">
            <div class="row">
                <label for="Answer" class="col-sm-3 col-md-2">
                    Select Proposal Number</label>
                <div class="col-sm-9 col-md-4">
                    <span class="colon">:</span>
                    <asp:DropDownList ID="ddlproposal" runat="server" Visible="false" CssClass="form-control">
                    </asp:DropDownList>
                    <span class="mandetory">*</span>                
                     <div class="col-sm-12 text-center">
                   <asp:Button ID="btnproceed" runat="server" Text="Proceed"  CssClass=" btn btn-success" OnClientClick="return Validation();"
                        OnClick="btnproceed_Click" />
                </div>
                </div>
            </div>          
        </div>        
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
