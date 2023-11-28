<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCostDtlsMaster.aspx.cs"
    Inherits="SingleWindow_AddCostDtlsMaster" %>

<%@ Register Src="~/Include/IncludeScript.ascx" TagName="IncludeScript" TagPrefix="ucIncludeScript" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Include/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/includes/Leftmenupanel.ascx" TagName="leftMenu" TagPrefix="ucLeftMenu" %>
<%@ Register Src="~/include/AMSfooter.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>SWP</title>
    <!-- Favicon and touch icons -->
    <link rel="shortcut icon" href="../images/favicon.ico" type="image/x-icon">
    <!-- Start Styles
         =====================================================================-->
    <script src="../../js/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../js/jQuery.alert.js" type="text/javascript"></script>
    <link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../../PortalCSS/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/lobipanel.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/flash.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/pe-icon-7-stroke.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/themify-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/emojionearea.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/monthly.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/jquery.timepicker.css" rel="stylesheet" type="text/css" />
    <!-- End Styles
         =====================================================================-->
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.timepicker.js" type="text/javascript"></script>
      <script src="../../js/Validator.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function Validation() {
            if (!blankFieldValidation('txtCostDesc', ' Add Cost Description')) { return false; }
            if (!WhiteSpaceValidation1st('txtCostDesc', ' Add Cost Description')) { return false; }
        }

        function SelectAllCheckboxesSpecific(spanChk) {
            var IsChecked = spanChk.checked;
            var Chk = spanChk;
            Parent = document.getElementById('grdViwCostDtls');
            var items = Parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++) {
                if (items[i].id != Chk && items[i].type == "checkbox") {
                    if (items[i].checked != IsChecked) {
                        items[i].click();
                    }
                }
            }
        }
    
    
    </script>
</head>
<body  class="hold-transition sidebar-mini">
  <div class="wrapper">
    <form id="form1" runat="server" defaultfocus="txtCostDesc">
    <!--Header-->
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <ucheader:header ID="header1" runat="server" />
    <aside class="main-sidebar">
            <!-- sidebar -->
            <div class="sidebar">
               <!-- sidebar menu -->           
            <ucLeftMenu:leftMenu ID="leftMenu1" runat="server" />
            </div>
            <!-- /.sidebar -->
        </aside>
        <div class="content-wrapper">
            <section class="content-header">
                <div class="header-icon">
                    <i class="fa fa-dashboard"></i>
                </div>
                <div class="header-title">
                    <h1>
                        Project Cost Head</h1>
                    <ul class="breadcrumb">
                        <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                        <li><a>AMS</a></li><li><a> Project Cost Head</a></li>
                    </ul>
                </div>
            </section>
             <section class="content">
               <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-bd lobidrag">
                            <div class="panel-body">
                               <div class="form-group row">
                                    <label class="col-sm-3"> Add Cost Description <span class="text-danger">*</span></label> 
                                        <div class="col-sm-5"><span class="colon">:</span>
                                         <asp:TextBox ID="txtCostDesc" runat="server"  CssClass="form-control" MaxLength="500" TextMode="MultiLine"
                                            TabIndex="1" Rows="4" onkeyup="return TextCounter('txtCostDesc','lblCostDesc',500)"
                                            ondrop="return false;" />
                                         <cc1:FilteredTextBoxExtender ID="FEBrief" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                            TargetControlID="txtCostDesc" InvalidChars="!<>%'*&" ValidChars=" .(),-/\">
                                         </cc1:FilteredTextBoxExtender>
                                         
                                          <small class="text-danger">Maximum
                                            <asp:Label ID="lblCostDesc" runat="server" Text="500"></asp:Label>
                                         &nbsp;characters</small>
                                         <asp:HiddenField ID="hdnKey" runat="server" Value="0" />
                                      </div>
                                  </div>
                            
                            <div class="row">
                                <div class="col-sm-4 col-sm-offset-3"> 
                                   <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success" OnClientClick="return Validation();" OnClick="btnSubmit_Click" />
                                   <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" /></div>      
                              </div>  
                              <br>
<div class="row">
                             <div >
                                 <div class="col-sm-12">
                                     <asp:GridView ID="grdViwCostDtls" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    AllowSorting="true" PageIndex="10" DataKeyNames="INT_DELETED_FLAG,INT_COST_ID"
                                                                    OnRowDataBound="grdViwCostDtls_RowDataBound" CssClass="table table-bordered"  
                                                                    onrowcommand="grdViwCostDtls_RowCommand">
                                                                    <RowStyle CssClass="tdData2" />
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-CssClass="NOPRINT" HeaderStyle-CssClass="NOPRINT">
                                                                            <HeaderTemplate>
                                                                                <input name="cbAll" value="cbAll" type="checkbox" onclick="javascript:SelectAllCheckboxesSpecific(this);" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSel" runat="server" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="35px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sl#" HeaderStyle-Width="40px">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex + 1%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Project Cost Details" DataField="VCH_COST_DTLS_DESC"
                                                                            HeaderStyle-HorizontalAlign="Left" NullDisplayText="--" />
                                                                        <asp:TemplateField HeaderText="Edit">
                                                                           <ItemStyle Width="50px" />
                                                                            <ItemTemplate >
                                                                                <asp:LinkButton ID="lnkbtnEdit" CssClass="btn btn-sm btn-success" runat="server" 
                                                                                    CommandName="E" OnClientClick="return confirm(' Are you sure want to Edit the selected Item!')"
                                                                                    CommandArgument='<%# Eval("INT_COST_ID") %>' >
                                                                                    <i class="fa fa-pencil-square-o"></i>
                                                              
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                        </div>
                                    </div>
	</div>
                                <div class="row">
                                    <div class="col-sm-4">
                                      <asp:Button ID="BtnInactive" runat="server" Text="Make Inactive" CssClass="btn btn-primary"
                                                                    OnClick="BtnInactive_Click" />
                                    </div>
                                </div>
                             </div>   
                        </div>
                    </div>
                </div>
            </section>
            </div>

     <ucfooter:footer ID="footer1" runat="server" />
    <!-- Modal -->
    </form>
    </div>
</body>
</html>
