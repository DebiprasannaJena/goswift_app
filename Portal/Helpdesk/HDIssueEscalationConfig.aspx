<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="HDIssueEscalationConfig.aspx.cs" Inherits="Portal_HelpDesk_HDIssueEscalationConfig"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../js/WebValidation.js"></script>
    <script src="../../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function validation() {
            var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
            var appendId = "ContentPlaceHolder1_";
            if (!DropDownValidation(appendId + 'ddlType', '0', 'Type', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#" + appendId + "ddlType").focus(); });
                return false;
            }
            if (!DropDownValidation(appendId + 'ddlCategory', '0', 'Category', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#" + appendId + "ddlCategory").focus(); });
                return false;
            }
            if (!DropDownValidation(appendId + 'ddlSubcategory', '0', 'Sub Catgeory', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#" + appendId + "ddlSubcategory").focus(); });
                return false;
            }
            if (confirm("Are you sure you want to submit the details?")) {
                return true;
            }
            else {
                return false;
            }
        }
       
    </script>
    <script type="text/javascript" language="javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "100%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>
    <style type="text/css">
        .table
        {
            width: 87%;
            max-width: 100%;
            margin-bottom: 15px;
            margin-left: 42PX;
            margin-top: 36PX;
        }
    </style>
    <script type="text/javascript" language="javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    for (var selector in config) {
                        $(selector).chosen(config[selector]);
                    }

                }
            });
        };
    </script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Issue Registration</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>HelpDesk</a></li><li><a>Add</a></li></ul>
            </div>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="HDIssueEscalationConfig.aspx"><i class="fa fa-plus"></i>
                                    Add</a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="ViewEscalationConfig.aspx"><i class="fa fa-file"></i>View</a>
                            </div>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Type <span class="text-red">*</span></label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlType" runat="server"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Department</asp:ListItem>
                                                    <asp:ListItem Value="2">Investor</asp:ListItem>
                                                    <asp:ListItem Value="3">Other</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-sm-2">
                                                Category <span class="text-red">*</span></label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlCategory" runat="server"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <%-- <asp:ListItem Value="1">Department</asp:ListItem>
                                                            <asp:ListItem Value="2">Investor</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-sm-2">
                                                Sub Category <span class="text-red">*</span></label>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlSubcategory" runat="server"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSubcategory_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <%-- <asp:ListItem Value="1">Department</asp:ListItem>
                                                            <asp:ListItem Value="2">Investor</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvEscalation" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped"
                                                        OnRowDataBound="gvEscalation_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSlno" runat="server" Text='<%#Eval("slno") %>' CssClass="form-control"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Authority">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="DdlDesg" runat="server" CssClass="form-control" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnDesg" runat="server" Value='<%#Eval("desglvl") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Escalation Level" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddllocLvl" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Standard Action Taking Days">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtStndDay" CssClass="form-control" runat="server" Text='<%#Eval("stdP") %>'
                                                                        MaxLength="5"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Notify Email">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNotifyEmail" CssClass="form-control" runat="server" Text='<%#Eval("vchEmail") %>'
                                                                        TextMode="MultiLine" MaxLength="250"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Notify PhoneNo">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNotifyPhoneNo" CssClass="form-control" runat="server" Text='<%#Eval("vchMobile") %>'
                                                                        TextMode="MultiLine" MaxLength="250"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Notify Email Content">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNotifyMailContent" CssClass="form-control" runat="server" Text='<%#Eval("vchEmailContent") %>'
                                                                        TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Notify Phone Content">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNotifyPhoneContent" CssClass="form-control" runat="server" Text='<%#Eval("vchMobileContent") %>'
                                                                        TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--    <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/delete_btn.gif"
                                                                            Height="13px" Width="13px" OnClick="ImageButton1_Click"  />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlSubCategory" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <%--          <div class="form-group" runat="server" id="userdetails" >
                                   <div class="row">
                                
                                  <label class="col-sm-2">Name <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                   <asp:TextBox ID="txtOName" MaxLength="50"  CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                 </div>
                                
                                  
                                 
                                
                                
                                  <label class="col-sm-2">Mobile <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                   <asp:TextBox ID="txtMobile" MaxLength="50"  CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                 </div>
                                 
                             </div>
                        </div>--%>
                                    <%--<div class="form-group">
                                   <div class="row">
                                   <div id="divInvest" runat="server" >
                                  <label class="col-sm-2">Investor <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList  TabIndex="17" ID="ddlInvestor" CssClass="chosen-select-width ddlInvestor"
                                            runat="server" >
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                    
                                                        </asp:DropDownList>
                                 </div>
                                 </div>

                                 <div id="useraddress" runat="server">
                                  <label class="col-sm-2">Address <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:TextBox ID="txtAddress" MaxLength="500" TextMode="MultiLine" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Address')"></asp:TextBox>
                                 </div>
                                 </div>
                                <label class="col-sm-2">Issue Details <span class="text-red">*</span></label>
                               <div class="col-sm-3">
                               
                                <span class="colon">:</span>
                                  <asp:TextBox ID="txtIssue" MaxLength="500" TextMode="MultiLine" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Address')"></asp:TextBox>
                                          </div> 

                                           </div>
                                 </div>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<div class="form-group">
                                   <div class="row">
                                           <label class="col-sm-2">Request Source <span class="text-red">*</span></label>
                                <div class="col-sm-3">
                                        <span class="colon">:</span>
                                    <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpStatus" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Email</asp:ListItem>
                                                            <asp:ListItem Value="2">Mobile</asp:ListItem>
                                                            <asp:ListItem Value="2">Letter</asp:ListItem>
                                                        </asp:DropDownList>
                                 </div>
                                  <label class="col-sm-2">File Upload</label>
                                  <div class="col-sm-3">
                                        <span class="colon">:</span>
                                 <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server"></asp:FileUpload>
                                 </div>
                                   
                                 </div>
                                  </div><div class="form-group">
                                 <div class="row">
                                 <label class="col-sm-2">Email <span class="text-red">*</span></label>
                               <div class="col-sm-3">
                               
                                <span class="colon">:</span>
                                  <asp:TextBox ID="txtEmail" MaxLength="100"  CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Email')"></asp:TextBox>
                                          </div> 
                                 </div>
                                 </div>--%>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnSave" CssClass="btn btn btn-success" runat="server" Text="Save"
                                            OnClick="btnSave_Click" OnClientClick="return validation();"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
        <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
        <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
    </div>
    <%-- </div>--%>
</asp:Content>
