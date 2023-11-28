<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServicePaymentNew.aspx.cs" Inherits="ServicePayment" %>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/investorheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/investormenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
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
        .gv
        {
            width: 100%;
            max-width: 99%;
            margin-bottom: 20px;
            margin-right: 52px;
            margin-left: 7px;
            margin-top: -12px;
        }
        .filebtn
        {
            min-width: 30px;
            border-radius: 0;
            margin-left: 7PX;
        }
    </style>
    <script>
        $(document).ready(function () {

            $('.menuservices').addClass('active');

        });
    </script>
    <%--   <script type="text/javascript">
                function valid() {
                    var counts = '<%=gvEscalation.Rows.Count %>';
                    var rowno, i;
                    var cur;
                    var val = 0;
                    var grid = document.getElementById("gvEscalation");
                    if (grid != null) {
                        var grdid = document.getElementById("gvEscalation");
                        var Rows = document.getElementById('HiddenField1').value; //grdid.rows.length;
                        cur = parseInt(Rows);
                        for (i = 0; i < cur; i++) {

                            var x = $("#gvEscalation_filUpld_" + i).val();
                            if (x != undefined && x != null && x != '') {
                                //alert('f');
                            }
                            else {
                                alert('Please select all file!');
                                return false;
                            }
                           
                        }
                    }
                }
            

    </script>--%>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div investors-bg">
            <div class="">
                <div id="exTab1">
                    <div class="investrs-tab">
                        <uc4:investoemenu ID="ineste" runat="server" />
                    </div>
                    <div class="wizard">
                        <div class="wizard-inner margin-top15">
                            <div class="connecting-line">
                            </div>
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" class="backactive"><a href="javasxript:void(0)" data-toggle="tab"
                                    aria-controls="Profile Creation" role="tab" title="Form Registration"><span class="round-tab">
                                        <i class="fa fa-file-text-o"></i></span><small>Form Registration</small> </a>
                                </li>
                                <li role="presentation" class="active"><a href="#step3" data-toggle="tab" aria-controls="Payment Details"
                                    role="tab" title="Payment Details"><span class="round-tab"><i class="fa fa-credit-card">
                                    </i></span><small>Payment Details</small> </a></li>
                                <li role="presentation" class="disabled"><a href="#complete" data-toggle="tab" aria-controls="Success"
                                    role="tab" title="Complete"><span class="round-tab"><i class="glyphicon glyphicon-ok">
                                    </i></span><small>Success</small> </a></li>
                            </ul>
                        </div>
                        <div class="form-sec">
                            <div class="form-body ">
                                <div class="">
                                    <%--        <div class="form-group ">
                            <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gvEscalation" runat="server" AutoGenerateColumns="False" 
                                                            CssClass ="table table-hover table-striped gv" 
                                                         
                                                            >
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL NO.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSlno" runat="server" Text='<%#Eval("slno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Label Name">
                                                                <ItemTemplate>
                                                                                       <asp:Label ID="lblLblName" runat="server" Text='<%#Eval("lblName") %>'></asp:Label>
                                                                                       </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="File Upload">
                                                                    <ItemTemplate>
                                                                    <asp:FileUpload id="filUpld" runat="server"/>
                                                                       <asp:HiddenField ID="hdnFile" runat="server" Value='<%#Eval("hdnVal") %>'/>
                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                      
                                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                               
                                                    </Triggers>
                                                </asp:UpdatePanel>
                            </div>
                               <div class="row">
                                 <asp:Button ID="btnFile"  CssClass="btn btn-success filebtn"  Text="File Upload" runat="server" OnClientClick="return valid();" OnClick="btnFile_Click" />
                            </div>
                            </div>--%>
                                    <div class="form-group " runat="server" id="divPayment">
                                        <div class="row">
                                            <label class="col-md-2 col-sm-2">
                                                Payment Amount :
                                            </label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2">
                                                <span class="apply "></span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <span class="apply "></span>
                                            </div>
                                            <div class="col-sm-12">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnApply" runat="server" Text="Pay Now" CssClass="btn btn-success"
                                                                Width="80" OnClick="btnApply_Click" />&nbsp;
                                                            <asp:Button ID="hrfPaymentConfrim" runat="server" Text="Confrim Payment" CssClass="btn btn-success"
                                                                OnClick="Button1_Click" Visible="false" />
                                                        </td>
                                                        <td style="padding: 10px">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="font-weight: bold; color: #FF0000">
                                                            N.B-<br />
                                                            1. Your payment is being submitted. Please do not close this window or click the
                                                            Back button on your browser.
                                                            <br />
                                                            2. Payment details are updated on the portal in 3 working days. If payment is not
                                                            reflected against your application after 3 working days, kindly contact support@investodiaha.gov.in
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--        </div>--%>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>
