<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ManageSchedular.aspx.cs" Inherits="Portal_SuperAdmin_ManageSchedular" %>

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
                <h1>Manage Scheduler</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Admin Privileges</a></li>
                    <li><a>Manage Scheduler</a></li>
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
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Service Payment Scheduler</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnServicePaymentSched" runat="server" CssClass="btn btn-success"
                                                Text="SERVICE - Run Service Payment Scheduler" OnClientClick="return confirm('Are you sure you want to run SERVICE PAYMENT scheduler [?]');"
                                                OnClick="BtnServicePaymentSched_Click" Width="300px" ToolTip="Click here to run SERVICE payment scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">SERVICE</span> payment scheduler instantly. This will fetch pending transactions for SERVICES
                                                and run the scheduler immediately. This is the manual mode for running the payment
                                                scheduler for SERVICE. The method used in auto scheduler and manual scheduler is
                                                same.</label>
                                        </div>
                                    </div>
                                    <%--<div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                               External Service Payment status send Scheduler</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnExternalServicePaymentSched" runat="server" CssClass="btn btn-secondary"
                                                Text="SERVICE - Run External Service Payment status send Scheduler" OnClientClick="return confirm('Are you sure you want to run External Service Payment status send scheduler [?]');"
                                                OnClick="BtnExternalServicePaymentSched_Click" Width="300px" ToolTip="Click here to run External SERVICE payment status scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">External SERVICE</span> payment status scheduler instantly. This will send pending successful transactions status for external SERVICES
                                                and run the scheduler immediately. This is the manual mode for running the 
                                                scheduler for send pending successful transactions status to external SERVICE. The method used in auto scheduler and manual scheduler is
                                                same.</label>
                                        </div>
                                    </div>--%>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                PEAL Payment Scheduler</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnPealPaymentSched" runat="server" CssClass="btn btn-danger" Text="PEAL - Run PEAL Payment Scheduler"
                                                OnClientClick="return confirm('Are you sure you want to run PEAL PAYMENT scheduler [?]');"
                                                OnClick="BtnPealPaymentSched_Click" Width="300px" ToolTip="Click here to run PEAL payment scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">PEAL</span> payment scheduler instantly. This will fetch pending transactions for PEAL and run
                                                the scheduler immediately. This is the manual mode for running the payment scheduler
                                                for PEAL. The method used in auto scheduler and manual scheduler is same.</label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                OSPCB Scheduler</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnOSPCBSched" runat="server" CssClass="btn btn-primary" Text="OSPCB - Run OSPCB Scheduler"
                                                OnClientClick="return confirm('Are you sure you want to run OSPCB scheduler [?]');"
                                                OnClick="BtnOSPCBSched_Click" Width="300px" ToolTip="Click here to run OSPCB scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">OSPCB</span> scheduler instantly. This will fetch all pending OSPCB applications and start
                                                the scheduler immediately. This is the manual mode for running the OSPCB scheduler. The auto scheduler and the manual scheduler use the same method.</label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                CMS-ORTPSA Scheduler</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnCmsOrtpsaSched" runat="server" CssClass="btn btn-warning" Text="CMS-ORTPSA - Run CMS-ORTPSA Scheduler"
                                                OnClientClick="return confirm('Are you sure you want to run CMS-ORTPSA scheduler [?]');"
                                                OnClick="BtnCmsOrtpsaSched_Click" Width="300px" ToolTip="Click here to run CMS-ORTPSA scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">CMS-ORTPSA</span> scheduler instantly. This will fetch all pending applications for CMS-ORTPSA and run
                                                the scheduler immediately. This is the manual mode for running the CMS-ORTPSA scheduler. The auto scheduler and the manual scheduler use the same method.</label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Monthly Mail Scheduler</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnMonthlyMail" runat="server" CssClass="btn btn-info" Text="Click to go Mail Configuration Page"
                                                OnClick="BtnMonthlyMail_Click" Width="300px" ToolTip="Click here to goto mail configuration page." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Go to the <span style="color: Red; font-weight: bold;">mail configuration</span> page to run the monthly email scheduler. Check the internal mail settings before running the scheduler.</label>
                                        </div>
                                    </div>


                                    

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                     <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                MoSarkar Scheduler</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="Btn_Mosarkar_Scheduler" runat="server" OnClick="Btn_Mosarkar_Scheduler_Click" CssClass="btn btn-pink" Text="Click to go Mosarkar scheduler"
                                                 Width="300px" ToolTip="Click to go Mosarkar scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">MoSarkar</span> scheduler instantly. This will fetch all approval applications for MoSarkar and run
                                                the scheduler immediately. This is the manual mode for running the MoSarkar scheduler. The auto scheduler and the manual scheduler use the same method.</label>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <%-- NSWS Scheduler--%>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <h4 class="h4-header">NSWS Scheduler Section [ National Single Window System ]</h4>
                                            <br />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                NSWS - Push Redirection Url</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnNswsPushRedirectUrl" runat="server" CssClass="btn btn-primary" Text="NSWS - Push Redirection URL"
                                                OnClientClick="return confirm('Are you sure you want to run NSWS-Push Redirection URL scheduler [?]');"
                                                OnClick="BtnNswsPushRedirectUrl_Click" Width="300px" ToolTip="Click here to run NSWS-Push Redirection URL scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">NSWS-Push Redirection URL</span> scheduler instantly. This will retrieve all applications that have been registered through the NSWS portal, whose redirection url has not been shared with the NSWS portal, and then run the scheduler immediately. It will send the dymanic redirection url to NSWS portal and update the sent status in GOSWIFT portal. This is the manual mode for running the NSWS-Push Redirection URL scheduler. The auto scheduler and the manual scheduler use the same method.</label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                NSWS - Push License/Application Status</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnNswsPushLicenseStatus" runat="server" CssClass="btn btn-primary" Text="NSWS - Push License/Application Status"
                                                OnClientClick="return confirm('Are you sure you want to run NSWS-Push License/Application Status scheduler [?]');"
                                                OnClick="BtnNswsPushLicenseStatus_Click" Width="300px" ToolTip="Click here to run NSWS-Push License/Application Status scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">NSWS-Push License/Application Status</span> scheduler instantly. This will retrieve all the pending applications, whose application status has not been shared with the NSWS portal, and then run the scheduler immediately. This is the manual mode for running the NSWS-Push License/Application Status scheduler. The auto scheduler and the manual scheduler use the same method.</label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                NSWS - Push Query Status</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnNswsPushQuery" runat="server" CssClass="btn btn-primary" Text="NSWS - Push Query Status"
                                                OnClientClick="return confirm('Are you sure you want to run NSWS-Push Query Status scheduler [?]');"
                                                OnClick="BtnNswsPushQuery_Click" Width="300px" ToolTip="Click here to run NSWS-Push Query Status scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">NSWS-Push Query Status</span> scheduler instantly. This will retrieve all the pending applications, whose query status has not been shared with the NSWS portal, and then run the scheduler immediately. This is the manual mode for running the NSWS-Push Query Status scheduler. The auto scheduler and the manual scheduler use the same method.</label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                NSWS - Push Approval Document</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnNswsPushDoc" runat="server" CssClass="btn btn-primary" Text="NSWS - Push Approval Document"
                                                OnClientClick="return confirm('Are you sure you want to run NSWS-Push Approval Document scheduler [?]');"
                                                OnClick="BtnNswsPushDoc_Click" Width="300px" ToolTip="Click here to run NSWS-Push Approval Document scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">NSWS-Push Approval Document</span> scheduler instantly. This will retrieve all the applications, whose approval document has not been shared with the NSWS portal, and then run the scheduler immediately. This is the manual mode for running the NSWS-Push Approval Document scheduler. The auto scheduler and the manual scheduler use the same method.</label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
                                        </div>
                                    </div>


                                     <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                NSWS - Push To DWH</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:Button ID="BtnNswsPushDwh" runat="server" CssClass="btn btn-primary" Text="NSWS - Push To DWH"
                                                OnClientClick="return confirm('Are you sure you want to run NSWS-Push Approval Document scheduler [?]');"
                                                OnClick="BtnNswsPushDwh_Click" Width="300px" ToolTip="Click here to run NSWS-Push Approval Document scheduler." />
                                        </div>
                                        <div class="col-sm-7">
                                            <label for="User">
                                                Click on this button to run the <span style="color: Red; font-weight: bold;">NSWS - Push Data To DWH</span> scheduler instantly. This will used to push investors who registered through the NSWS portal but were unable to complete the push procedure to the DWH site, and then run the scheduler immediately.This will send a failed registered user to DWH and create a dynamic URL that will be sent to NSWS. This is the manual mode for running the NSWS - Push Data To DWH. The auto scheduler and the manual scheduler use the same method.</label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <hr />
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
