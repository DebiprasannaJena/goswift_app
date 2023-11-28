<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrievanceDetailsNonIndustry.aspx.cs" Inherits="GRIEVANCE_GrievanceDetailsNonIndustry" %>

<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/NonIndustryWebHeader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/NonIndustryWebMenu.ascx" TagName="NonIndustryWebMenu" TagPrefix="uc4" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link rel="stylesheet" type="text/css" href="../css/custom.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.menugrievance').addClass('active');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="registration-div">
                <div class="investrs-tab">
                    <uc4:NonIndustryWebMenu ID="Grievance" runat="server" />
                </div>
                <div class="wizard wizard2">
                    <div class="row">
                        <div class="col-sm-12">
                            <h1 class="headerpeal">Grievance  Details</h1>

                            <div class="panel panel-bd lobidisable">
                                <div class="panel-body">


                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Grievance Id</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Griv_Id" runat="server" CssClass="form-control-static" Font-Bold="true"
                                                ForeColor="Red"></asp:Label>
                                        </div>
                                        <label class="col-sm-2">
                                            Apply Date</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Apply_Date" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class=" row form-group">
                                        <div class="">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">Applicant Details
                                                </h4>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Name of the Company</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Company_Name" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                        </div>
                                        <%-- add by anil sahoo--%>
                                        <label class="col-sm-2">
                                            Industry Type</label>

                                        <div class="col-sm-4">
                                            <span class="colon">:</span>

                                            <asp:Label ID="Lbl_Industry_Type" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                            <%-- add by anil sahoo--%>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Applicant Name</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Applicant_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                        <label class="col-sm-2">
                                            Designation</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Designation" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Mobile No</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Mobile_No" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                        <label class="col-sm-2">
                                            District
                                        </label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_District" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Investment Level</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Investment_Level" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                        <label class="col-sm-2">
                                            Email
                                        </label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Email" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class=" row form-group">
                                        <div class="">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">Grievance Details
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Grievance Type</label>
                                        <div class="col-sm-10">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Griv_Type" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Grievance Sub Type
                                        </label>
                                        <div class="col-sm-10">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Griv_Sub_Type" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Grievance Title</label>
                                        <div class="col-sm-10">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Griv_Title" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Grievance Details
                                        </label>
                                        <div class="col-sm-10">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Griv_Details" runat="server" CssClass="form-control-static"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Attachment-1
                                        </label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:HyperLink ID="Hyp_Attachment_1" runat="server" Target="_blank" ToolTip="Click here to view document !"><i class="fa fa-download"></i></asp:HyperLink>
                                            <asp:Label ID="Lbl_Doc_Attachment_1" runat="server" CssClass="form-control-static"
                                                ForeColor="Red"></asp:Label>
                                        </div>
                                        <label class="col-sm-2">
                                            Attachment-2
                                        </label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:HyperLink ID="Hyp_Attachment_2" runat="server" Target="_blank" ToolTip="Click here to view document !"><i class="fa fa-download"></i></asp:HyperLink>
                                            <asp:Label ID="Lbl_Doc_Attachment_2" runat="server" CssClass="form-control-static"
                                                ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class=" row form-group">
                                        <div class="">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">Action Details
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class="col-sm-2">
                                            Current Status
                                        </label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Status" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                        </div>
                                        <label class="col-sm-2">
                                            Action Date
                                        </label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:Label ID="Lbl_Action_Date" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="row form-group">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover"
                                                AutoGenerateColumns="False" Width="100%" EmptyDataText="No Action Taken Yet..."
                                                OnRowDataBound="GridView1_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grievance Id" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Griv_Id" runat="server" Text='<%# Eval("vchGrivId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Status" runat="server" Text='<%# Eval("vchStatusName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Remarks" runat="server" Text='<%# Eval("vchRemarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reference Doc" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="Hid_Ref_Doc_Name" runat="server" Value='<%#Eval("vchRefDoc") %>' />
                                                            <asp:HyperLink ID="Hyp_Ref_Doc" runat="server" class="fa fa-download" aria-hidden="true" />
                                                            <asp:Label ID="Lbl_Ref_Doc" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Taken By" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Action_By" runat="server" Text='<%# Eval("vchActionBy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Date" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_Action_Date" runat="server" Text='<%# Eval("dtmCreatedOn" ,"{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row form-group">
                                        <div class="col-sm-4">
                                            <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn btn-success" OnClick="Btn_Back_Click" />
                                        </div>
                                        <div class="clearfix">
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
    </form>
</body>
</html>

