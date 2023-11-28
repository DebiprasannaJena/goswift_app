<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateLandDetails.aspx.cs"
    MasterPageFile="~/MasterPage/Application.master" Inherits="Portal_SuperAdmin_UpdateLandDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">       
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Update PEAL Land Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Admin Privileges</a></li>
                    <li><a>Update PEAL Land Details</a></li>
                </ul>
            </div>
        </div>
        <div class="content">
            <div class="row">                
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>

                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Enter Proposal Number</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Proposal_No" runat="server" placeholder="Type Proposal No. Here" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Button ID="Btn_Search" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="Btn_Search_Click" />
                                            <asp:Button ID="Btn_Reset" runat="server" CssClass="btn btn-warning" Text="Reset" OnClick="Btn_Reset_Click" />
                                            <asp:Label ID="Lbl_Msg" runat="server" CssClass="text-danger"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>                                   

                                    <div id="DivApplicationDetails" runat="server">

                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">Applicant Details
                                                </h4>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="User">
                                                    Proposal No</label>
                                            </div>
                                            <div class="col-sm-10">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Lbl_Proposal_No" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="User">
                                                    Industry Name</label>
                                            </div>
                                            <div class="col-sm-10">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Lbl_Industry_Name" runat="server" CssClass="form-control-static" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="User">
                                                    Status</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:Label ID="Lbl_Status" runat="server" CssClass="form-control-static" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            </div>
                                            <div class="col-sm-7">
                                                 <label for="User">                                             
                                                     Before updating <span style="color: Red; font-weight: bold;">Approved</span> application, take consent(approval) from IPICOL through email.
                                               </label>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-sm-12">
                                                <h4 class="h4-header">Land Details
                                                </h4>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="Type4">
                                                    Land required from government <span class="text-red">*</span></label>
                                            </div>
                                            <div class="col-sm-4">
                                                <span class="colon">:</span>
                                                <asp:RadioButtonList ID="RadBtn_Land_Req_Govt" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="RadBtn_Land_Req_Govt_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem class="radio-inline" Text="Yes" Value="1" />
                                                    <asp:ListItem class="radio-inline" Text="No" Value="0" />
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="Type4">
                                                    District <span class="text-red">*</span></label>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Specify the District of proposed location of land">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_District" runat="server" CssClass="form-control" OnSelectedIndexChanged="DrpDwn_District_SelectedIndexChanged"
                                                    AutoPostBack="true" TabIndex="1">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="Type4">
                                                    Block <span class="text-red">*</span></label>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Specify the Block of proposed location of land">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Block" runat="server" CssClass="form-control" TabIndex="2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <label for="Type4">
                                                    Extent of land (in acre) <span class="text-red">*</span></label>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="Txt_Extent_Of_Land" CssClass="form-control" runat="server" MaxLength="8" onkeypress="return FloatOnly(event,this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group row" id="DivLandRequired" runat="server">
                                            <div class="col-sm-2">
                                                <label for="Type4">
                                                    Whether land is required in IDCO industrial estate <span class="text-red">*</span></label>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Land_Req_Idco" runat="server" CssClass="form-control" TabIndex="5" OnSelectedIndexChanged="DrpDwn_Land_Req_Idco_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row" id="DivIndustrialEstate" runat="server">
                                            <div class="col-sm-2">
                                                <label for="Type4">
                                                    Name of the IDCO industrial estate<span class="text-red">*</span></label>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Industrial_Estate" runat="server" CssClass="form-control" TabIndex="6">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row" id="DivLandAcquired" runat="server">
                                            <div class="col-sm-2">
                                                <label for="Type4">
                                                    Whether land to be acquired by IDCO <span class="text-red">*</span></label>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="DrpDwn_Land_Acquired_IDCO" runat="server" CssClass="form-control"
                                                    TabIndex="7">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-10">
                                                <asp:Button ID="Btn_Update" runat="server" Text="Update" CssClass="btn btn-success" OnClick="Btn_Update_Click" OnClientClick="return conformation(this);" />
                                                <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="Btn_Cancel_Click" OnClientClick="return conformation(this);" />                                               
                                            </div>
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

    <script type="text/Javascript" language="javascript">

        function valid() {
            debugger;
            var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

            if (document.getElementById('ContentPlaceHolder1_DrpDwn_District').value == "0") {
                jAlert('<strong>Please select district !</strong>', projname);
                document.getElementById('ContentPlaceHolder1_DrpDwn_District').focus();
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_DrpDwn_Block').value == "0") {
                jAlert('<strong>Please select block !</strong>', projname);
                document.getElementById('ContentPlaceHolder1_DrpDwn_Block').focus();
                return false;
            }
            if (document.getElementById('ContentPlaceHolder1_Txt_Extent_Of_Land').value == "") {
                jAlert('<strong>Extent of land can not be left blank !</strong>', projname);
                document.getElementById('ContentPlaceHolder1_Txt_Extent_Of_Land').focus();
                return false;
            }

            var radioValue = $("input[name='ctl00$ContentPlaceHolder1$RadBtn_Land_Req_Govt']:checked").val();           
            if (radioValue == "1")  /// YES
            {            
                if (document.getElementById('ContentPlaceHolder1_DrpDwn_Land_Req_Idco').value == "0") {
                    jAlert('<strong>Please select whether land is required in IDCO industrial estate.</strong>', projname);
                    document.getElementById('ContentPlaceHolder1_DrpDwn_Land_Req_Idco').focus();
                    return false;
                }
                if (document.getElementById('ContentPlaceHolder1_DrpDwn_Land_Req_Idco').value == "1") {
                    if (document.getElementById('ContentPlaceHolder1_DrpDwn_Industrial_Estate').value == "0") {
                        jAlert('<strong>Please select IDCO industrial estate.</strong>', projname);
                        document.getElementById('ContentPlaceHolder1_DrpDwn_Industrial_Estate').focus();
                        return false;
                    }
                }
                if (document.getElementById('ContentPlaceHolder1_DrpDwn_Land_Req_Idco').value == "2") {
                    if (document.getElementById('ContentPlaceHolder1_DrpDwn_Land_Acquired_IDCO').value == "0") {
                        jAlert('<strong>Please select land to be acquired by IDCO.</strong>', projname);
                        document.getElementById('ContentPlaceHolder1_DrpDwn_Land_Acquired_IDCO').focus();
                        return false;
                    }
                }                
            }
            return true;
        }

        function conformation(btnId) {
            debugger;
            var btntext = btnId.value;
            if (btntext.toLowerCase() == "update") {
                if (!valid()) {
                    return false;
                }
                else {
                    return confirm('Do you want to update ?');
                }
            }
            else if (btntext.toLowerCase() == "cancel") {
                return confirm('Do you want to cancel ?');
            }
            return true;
        }


        function FloatOnly(evt, control) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                //alert("Enter Numerals only in this Field!");             
                return false;
            }
            if (charCode == 46) {
                var patt1 = new RegExp("\\.");
                var ch = patt1.exec(control.value);
                if (ch == ".") {
                    //alert("More then One Decimal Point not Allowed");
                    return false;
                }
            }
            return true;
        }    

    </script>

</asp:Content>
