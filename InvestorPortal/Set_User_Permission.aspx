<%--'*******************************************************************************************************************
' File Name         : Set_User_Permission.aspx
' Description       : Grant application access permission to 2nd level user.
' Created by        : Sushant Kumar Jena
' Created On        : 26-Sep-2018
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Set_User_Permission.aspx.cs"
    Inherits="InvestorPortal_Set_User_Permission" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/animate.css" rel="stylesheet" type="text/css" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('.menumanage').addClass('active');
        })

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validatePermission() {

            if (DropDownValidation('DrpDwn_User', '0', 'user', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_User").focus(); });
                return false;
            }
        }
        
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:pealmenu ID="pealmenu" runat="server" />
                    </div>
                    <div class="form-sec">
                        <div class="form-header">
                            <h2>
                                Set Application Permission
                            </h2>
                        </div>
                        <div class="form-body">
                            <div class="formbodycontent">
                                <div class="form-group">
                                    <div class="row">
                                        <label for="Contact" class="col-sm-2">
                                            Select User
                                        </label>
                                        <div class="col-sm-5">
                                            <span class="colon">:</span>
                                            <asp:DropDownList ID="DrpDwn_User" runat="server" CssClass="form-control" OnSelectedIndexChanged="DrpDwn_User_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <span class="mandetory">*</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <label for="Contact" class="col-sm-2">
                                            Select application to provide access permission
                                        </label>
                                        <div class="col-sm-7">
                                            <span class="colon">:</span>
                                            <asp:DataList ID="DataList1" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                                CssClass="table table-bordered table-striped bg-white" Width="100%">
                                                <ItemTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="4%">
                                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:HiddenField ID="Hid_App_Id" runat="server" Value='<%# Eval("INTAPPID") %>' />
                                                                &nbsp;<asp:Label ID="Lbl_App_Name" runat="server" Text='<%# Eval("VCHAPPALIAS") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn btn-success"
                                                OnClick="Btn_Submit_Click" OnClientClick="return validatePermission();" />
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive" id="divGrd" style="margin-top: 15px;">
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
