d<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvesterInvestApply.aspx.cs"
    Inherits="InvesterInvestApply" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
        $(document).ready(function () {
            $("#DVDet").hide();
            var l = $('input[name=rdbApplication]:checked', '#form2').val();
            if (l == "1") {
                $("#DVDet").show();
            }

            $('#btnNext').click(function () {
                if (DropDownValidation('drpTurnOver', '0', 'Annual Turnover', projname) == false) {
                    return false;
                }
                if (DropDownValidation('drpCompanyType', '0', 'Company Type', projname) == false) {
                    return false;
                }
                var m = $('input[name=rdbApplication]:checked', '#form2').val();
                if (m == "1") {
                    if (blankFieldValidation('txtIndustryCode', 'Industry Code', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidation1st('txtIndustryCode', 'Industry Code', projname) == false) {
                        return false;
                    }
                    if (WhiteSpaceValidationLast('txtIndustryCode', 'Industry Code', projname) == false) {
                        return false;
                    }
                }
            });

            $('input[name="rdbApplication"]').change(function () {
                var k = $('input[name=rdbApplication]:checked', '#form2').val();
                if ($(this).val() == "1") {
                    $("#DVDet").show();
                }
                else {
                    $("#DVDet").hide();
                }
            });
        });     
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="registration-div">
        <div class="container">
            <div class="wizard wizard2">
                <div class="wizard-inner">
                    <ol class="breadcrumb breadcrumb-arrow">
                        <li><a href="InvesterInvestApply.aspx">Invester For</a></li>
                    </ol>
                </div>
                <div class="form-sec">
                    <div class="form-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="Type3">
                                        Applied For <span class="text-red">*</span></label><br />
                                    <asp:RadioButtonList ID="rdbApplication" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                        <asp:ListItem class="radio-inline" Text="New" Value="0" Selected="True" />
                                        <asp:ListItem class="radio-inline" Text="Expansion" Value="1" />
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Type3">
                                        Annual Turnover <span class="text-red">*</span></label><br />
                                    <asp:DropDownList ID="drpTurnOver" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="< 50 Crore" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=">=50 Crore and <=100 Crore" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="> 100 Crore" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label for="Type3">
                                        Company Type <span class="text-red">*</span></label><br />
                                    <asp:DropDownList ID="drpCompanyType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Industry" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="ITES" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="DVDet">
                            <div class="row">
                                <div class="col-sm-4">
                                    <label for="Type3">
                                        Industry Code <span class="text-red">*</span></label><br />
                                    <asp:TextBox ID="txtIndustryCode" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-sec">
                    <div class="form-footer">
                        <div class="row">
                            <div class="col-sm-12" align="center">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass=" btn btn-warning"/>
                                <asp:Button ID="btnNext" runat="server" Text="Submit" CssClass=" btn btn-success"
                                    OnClick="btnNext_Click" />
                                <input type="reset" text="Reset" class=" btn btn-reset" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
