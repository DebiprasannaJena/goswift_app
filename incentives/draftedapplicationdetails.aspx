<%--'*******************************************************************************************************************
' File Name         : draftedapplicationdetails.aspx
' Description       : View Drafted Application Details
' Created by        : Sushant Kumar Jena
' Created On        : 13th Sept 2017
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="draftedapplicationdetails.aspx.cs"
    Inherits="incentives_incentiveoffered" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {

                window.print();
            });
        });    

    </script>
    <script type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        function validateSendMail(LnkBtn_Discard) {
            var ss = LnkBtn_Discard.id;
            var pp = $('#' + ss).attr('href');
            jConfirm('Are you sure you want to discard this application ? <br/> If discarded, the application will be no longer available.', projname, function (callback) {
                if (callback) {
                    location.href = pp;
                    return true;

                } else {
                    return false;
                }
            });
            return false;
        }
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
        <div class="container">
        <div  class="container wrapper">
    <div class="registration-div investors-bg">
     
        <div id="exTab1" class="container">

            <div class="investrs-tab">
                <uc4:investoemenu ID="ineste" runat="server" />
            </div>
            <div class="tab-content clearfix">
                <div class="tab-pane active" id="1a">
                    <div class="form-sec">
                        <div class="innertabs m-b-10">
                            <ul class="nav nav-pills pull-right">
                                <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                            </ul>
                            <div class="clearfix">
                            </div>
                        </div>
                        <div class="form-header">
                            <h2>
                                Drafted Application Details
                            </h2>
                        </div>
                        <div class="form-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Industry Code</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="Txt_Industry_Code" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-sm-2">
                                                Industry Name</label>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="Txt_Industry_Name" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="details-section">
                                            <br />
                                            <asp:GridView ID="Grd_Drafted_App" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                                EmptyDataText="No Records Found !!">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Application Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Inct_Name" runat="server" Text='<%# Eval("strInctName") %>'></asp:Label>
                                                            <asp:HiddenField ID="Hid_Inct_Id" runat="server" Value='<%# Eval("intInctId") %>' />
                                                            <asp:HiddenField ID="Hid_Inct_Unique_Id" runat="server" Value='<%# Eval("intInctUniqueId") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Saved On">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Created_On" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                            <asp:HiddenField ID="Hid_Form_Id" runat="server" Value='<%# Eval("strFormId") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Details">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LnkBtn_Details" runat="server" OnClick="LnkBtn_Details_Click"
                                                                ToolTip="Click Here to View and Edit Saved Application !!">Details</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discard">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LnkBtn_Discard" runat="server" OnClick="LnkBtn_Discard_Click"
                                                                OnClientClick="return  validateSendMail(this);" ForeColor="Maroon" ToolTip="Click Here to Discard this Application !!">Discard</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Button ID="Btn_Hidden" runat="server" Text="" Style="display: none;" OnClick="Btn_Hidden_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-footer">
                                <div class="row">
                                    <div class="col-sm-12 text-right">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            </div>
        <uc3:footer ID="footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
