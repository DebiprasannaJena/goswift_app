<%--'*******************************************************************************************************************
' File Name         : ProfessionalTaxData.aspx
' Description       :To Provide Some Important data to Professional Tax 
' Created by        : Prasun Kali
' Created On        : 10th october 2017
' Modification History:

'                        <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfessionalTaxData.aspx.cs" Inherits="ProfessionalTaxData" %>

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
    <title></title>
     <script>

         $(document).ready(function () {

             $('.menuservices').addClass('active');

         });
         function validate() {
             if (document.getElementById("txtPanNo").value == "") {
                 alert('Please Enter Pan No');
                 return false;
             }
           
         }
    </script>
</head>
<body>
    <form id="form2" runat="server">
     <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="container">
    <uc2:header ID="header" runat="server" />
    <div class="registration-div investors-bg">
        <div class="">
            <div id="exTab1">
                <div class="investrs-tab">
                    <uc4:investoemenu ID="ineste" runat="server" />
                </div>
                <div class="form-sec">
                  <%--  <div class="form-header">
                        <h2>
                           (Code-Industry Name)</h2>
                    </div>--%>
                    <div class="form-body ">
                        <div class="search-sec">
                            <div class="form-group ">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                  <div class="row">
                                    <label class="col-md-2 col-sm-2">
                                        Please Enter Pan No
                                    </label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPanNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                <%--    <label class="col-md-2 col-sm-2">
                                        Select Ulb
                                    </label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlULB" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>
                                    <div class="col-sm-2">
                                        <span class="apply ">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success"
                                                Width="80" onclick="btnSubmit_Click" OnClientClick="return validate();"/>
                                        </span>
                                    </div>
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                                </Triggers>
                                </asp:UpdatePanel>
                              
                            </div>
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
