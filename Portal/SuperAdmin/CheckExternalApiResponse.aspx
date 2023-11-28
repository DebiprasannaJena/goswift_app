<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="CheckExternalApiResponse.aspx.cs" Inherits="Portal_SuperAdmin_CheckExternalApiResponse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Check External API Response (Treasury / PAN-NSDL)</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Admin Privileges</a></li>
                    <li><a>Check External API Response</a></li>
                </ul>
            </div>
        </div>
        <div class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>

                                    <%-- Payment Status From Treasury--%>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <h4 class="h4-header">Check Payment Status from Treasury
                                            </h4>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Order Number</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Order_No_Treasury" runat="server" CssClass="form-control" placeholder="Enter Order Number" ToolTip="Enter treasury order number here."></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Challan Amount</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Challan_Amount_Treasury" runat="server" CssClass="form-control" placeholder="Enter Challan Amount" ToolTip="Enter challan amount here."></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:Button ID="BtnPaymentStatusTreasury" runat="server" Text="Get Payment Status from Treasury"
                                                OnClick="BtnPaymentStatusTreasury_Click" CssClass="btn btn-primary" ToolTip="Click here to get payment status from Treasury portal." />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-10">
                                            <asp:Label ID="Lbl_Treasury_Response" runat="server"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <%--PAN Status From NSDL--%>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <h4 class="h4-header">Check PAN Status from NDSL
                                            </h4>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            PAN Number</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_PAN" runat="server" CssClass="form-control" MaxLength="10" placeholder="Enter PAN Number" ToolTip="Enter PAN here."></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:Button ID="BtnPANStatus" runat="server" Text="Get PAN Status from NSDL"
                                                OnClick="BtnPANStatus_Click" CssClass="btn btn-success" ToolTip="Click here to get the PAN status from NSDL." />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-10">
                                            <asp:Label ID="Lbl_PAN_Response" runat="server"></asp:Label>
                                            <asp:HiddenField ID="Hid_PAN_Credential" runat="server" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    

                                    <%--Token Status From NSWS--%>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <h4 class="h4-header">Check Token Status from NSWS Portal
                                            </h4>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Click to Get Token from NSWS</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnTokenNSWS" runat="server" Text="Click here to Generate Token from NSWS"
                                                OnClick="BtnTokenNSWS_Click" CssClass="btn btn-danger" ToolTip="Click here to generate token from NSWS portal." />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-9">
                                            <div style="overflow: auto;">
                                                <asp:Label ID="Lbl_Token_Addrees" runat="server" CssClass="form-control-static"></asp:Label>
                                                <br />
                                                <asp:Label ID="Lbl_Token_Response" runat="server" CssClass="form-control-static"></asp:Label>
                                                <br />
                                                <asp:Label ID="Lbl_Token" runat="server" CssClass="form-control-static"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <%--Check SMS Status --%>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <h4 class="h4-header">Check SMS 
                                            </h4>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Main URL</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Main_Url" runat="server" CssClass="form-control"  placeholder="https://smsgw.sms.gov.in/failsafe/MLink" ToolTip="Enter Main URL."></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            User Name</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_User_Name" runat="server" CssClass="form-control"  placeholder="goswift.sms" ToolTip="Enter User Name."></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            PIN</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_PIN" runat="server" CssClass="form-control"  placeholder="Np%40%236745" ToolTip="Enter PIN."></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Message</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Msg" runat="server" CssClass="form-control"  placeholder="OTP has been generated.Kindly log into https://invest.odisha.gov.in with the new OTP [12345] for M/S TestUnit." ToolTip="Enter Message"></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Mobile Number</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Mobile_No" runat="server" CssClass="form-control"  placeholder="919090241990" ToolTip="Enter Mobile Number"></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Signature</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Signature" runat="server" CssClass="form-control"  placeholder="GOSWIT" ToolTip="Enter Signature"></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            DLT Entity Id</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_DLT_Entity_Id" runat="server" CssClass="form-control"  placeholder="1001936451134336346" ToolTip="Enter DLT Entity Id"></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            DLT Template Id</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_DLT_Template_Id" runat="server" CssClass="form-control"  placeholder="1407161579050838384" ToolTip="Enter DLT Template Id"></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>


                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:Button ID="Btn_Msg_Configure" runat="server" Text="SUBMIT"
                                                OnClick="Btn_Msg_Configure_Click" CssClass="btn btn-success" ToolTip="Click here to get the SMS API test satatus ." />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-10">
                                            <asp:Label ID="Lbl_Msg_Configure" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="Lbl_Response_1" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <br />
                                    <hr />
                                    <br />
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Full URL</label>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Full_Url" runat="server" CssClass="form-control" TextMode="MultiLine" Width="250%" Height="50%" placeholder="https://smsgw.sms.gov.in/failsafe/MLink?username=goswift.sms&pin=Np%40%236745&message='OTP has been generated.Kindly log into https://invest.odisha.gov.in with the new OTP 12345 for M/S TestUnit.'&mnumber=919090243166&signature=GOSWIT&dlt_entity_id=1001936451134336346&dlt_template_id=1407161579050838384"
                                                ToolTip="Enter Mobile Number"></asp:TextBox>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:Button ID="Btn_Msg_2" runat="server" Text="SUBMIT BY FULL URL " OnClick="Btn_Msg_2_Click" />
                                            <asp:Label ID="Lbl_Response_2" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
