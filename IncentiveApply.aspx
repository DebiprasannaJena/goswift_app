<%--'*******************************************************************************************************************
' File Name         : IncentiveApply.aspx
' Description       : Apply Incentive
' Created by        : AMit Sahoo
' Created On        : 13 July 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncentiveApply.aspx.cs" Inherits="IncentiveApply" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <style>
        
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($("#ddlPolicy").val() > 0) {
                    return true;
                }
                else {
                    alert('Please select Policy')
                    return false;
                }
                if ($("#ddlScheme").val() > 0) {
                    return true;
                }
                else {
                    alert('Please select Scheme')
                    return false;
                                }
            })
        });
</script>

</head>
<body>
    <<form id="form2" runat="server">
    <uc2:header ID="header" runat="server" />
    <div class="pagenavigator">
        <h2>
            <a class="" href="javascript:history.back()"><i class="fa fa-chevron-circle-left"></i>
            </a>Incentive</h2>
    </div>
    <div class="registration-div">
        <div class="container">
            <div class="form-sec">
            <h2>
                    <b>Apply Incentive</b></h2>
                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-9">
                            <label for="Policy">
                                Policy Name<span class="text-red">*</span></label>
                            <asp:DropDownList ID="ddlPolicy" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Policy 1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Policy 2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Policy 3" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-9">
                            <label for="Scheme">
                                Scheme Name<span class="text-red">*</span></label>
                            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Scheme 1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Scheme 2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Scheme 3" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-4">
                        </div>
                        <div class="col-sm-4" align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass=" btn btn-success"
                                Width="80" onclick="btnSubmit_Click" />
                                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass=" btn btn-warning"
                                Width="80" />
                        </div>
                        <div class="col-sm-4">
                       
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
