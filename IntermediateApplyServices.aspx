<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntermediateApplyServices.aspx.cs"
    Inherits="IntermediateApplyServices" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script>

        $(document).ready(function () {

            $('.menuservices').addClass('active');

        });
   
    </script>
    <style>
        .bindlabel
        {
            border: 1px solid #cccccc;
            padding: 3px 10px;
            margin-top: 0px !important;
        }

        .search-sec
        {
            padding: 20px 20px 10px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
  <div class="container">
    <div class="registration-div investors-bg">
        
          <uc2:header ID="header" runat="server" />
            <div id="exTab1">
                <div class="investrs-tab">
                    <uc4:investoemenu ID="ineste" runat="server" />
                </div>
                <div class="form-sec">
                    <div class="form-body">
                        <div class="form-group row" style="margin:0;">
                                <div class="col-md-1 col-sm-2">
                                    <label for="Type4">
                                        Type<span class="text-red">*</span></label>
                                </div>
                                <div class="col-sm-4">
                                <span class="colon">:</span>
                                    <asp:RadioButtonList ID="rdbExIndustry" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                        <asp:ListItem class="radio-inline" Text="Industry Code" Value="1" />
                                        <asp:ListItem class="radio-inline" Text="Proposal No" Value="2" Selected="True" />
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-md-1 col-sm-2">
                                    <label for="Type4">
                                        Proposal<span class="text-red">*</span></label>
                                </div>
                                <div class="col-sm-4">
                                <span class="colon">:</span>
                                    <asp:TextBox ID="txtProposalid" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-2" align="center">
                                    <asp:Button ID="btnsave" runat="server" Text="Apply Now" CssClass="btn btn-success"
                                        OnClick="btnsave_Click" />
                                </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
