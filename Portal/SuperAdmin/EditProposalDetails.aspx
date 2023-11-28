<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="EditProposalDetails.aspx.cs" Inherits="Portal_SuperAdmin_EditProposalDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <div class="content-wrapper">       
        <div class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Edit Proposal Details</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Admin Privileges</a></li>
                    <li><a>Edit Proposal Details</a></li>
                </ul>
            </div>
        </div>
        <div class="content">
            <div class="row">                
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable" id="DivSelect">
                        <div class="panel-body">
                                                      
                                   <div id="DivSelectType" runat="server">
                                          <div class="form-group row">
                                        <div class="col-sm-2">
                                            <label for="User">
                                                Enter Proposal Number</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="TextBox1" MaxLength="10" runat="server" Onkeypress="return inputLimiter(event,'Numbers')" placeholder="Type Proposal No. Here" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"  CssClass="btn btn-primary" Text="Search" OnClientClick="return Validate();" />
                                            <asp:Label ID="Label1" runat="server" CssClass="text-danger"></asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                       </div>
                                  </div>
                                 <div>
                        </div>
                    </div>
                    <div id="DivPeal" runat="server">
                    <div class="panel panel-bd lobidisable"  visible="true">
                        <div class="panel-body">
                                   <div class="form-sec">
                                <h1 class="headerpeal">
                                    Project Evaluation including Allotment of Land</h1>
                                <div class="form-header">
                                    <span class="pull-right"><span class="mandatoryspan ">(*) </span>Mark Fields Are Mandatory</span>
                                    <h2>
                                        1.Company Information</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group row row-xs m-b-0">
                                        <label for="Address2" class="col-md-3 col-sm-4 col-form-label">
                                            <asp:HiddenField ID="vers" Value="v1" runat="server" />
                                            Name of the Company/Enterprise <span class="text-red">*</span>
                                        </label>
                                        <div class="col-md-4 col-sm-8">
                                            <asp:TextBox ID="TextBox4" CssClass="form-control phcode" runat="server" ReadOnly="true"
                                                Text="M/s" ></asp:TextBox>
                                            <asp:TextBox CssClass="form-control phnum" ID="txtIName" MaxLength="100" runat="server"
                                                TabIndex="1"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                TargetControlID="txtIName" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                ValidChars=" ">
                                            </cc1:FilteredTextBoxExtender>
                                            <a data-toggle="tooltip" class="fieldinfo" title="It can accept all characters"><i
                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="sbuhdr">
                                                    Corporate Office Address</h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-3 col-sm-12">
                                                <label for="Address1">
                                                    Address<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtAddress" TabIndex="2" onkeyup="setvalue();" TextMode="MultiLine"
                                                    Onkeypress="return inputLimiter(event,'Address')" MaxLength="250" CssClass="form-control"
                                                    Height="104px" runat="server"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="The applicant will fill details of the main office/headquarters (i.e. where the executives of the company, including the CEO, maintain their offices and is the central location where top decisions are made). It can accept all characters">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><i>Maximum <span
                                                        id="SpanLbl" class="mandatoryspan">250</span> characters left</i></small>
                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <div class="form-group row">
                                                    <div class="col-md-4 col-sm-4">
                                                        <label for="Country">
                                                            Country<span class="text-red">*</span></label>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" runat="server" TabIndex="3">
                                                            <asp:ListItem Value="0" Selected="True">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Country can be selected from the drop-down menu">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4" id="st1" runat="server">
                                                        <label for="State">
                                                            State<span class="text-red">*</span></label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="4" ID="ddlState" runat="server">
                                                            <asp:ListItem Value="0" Selected="True">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="State can be selected from the drop-down menu">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4" id="st2" runat="server">
                                                        <label for="State">
                                                            State<span class="text-red">*</span></label>
                                                        <asp:TextBox CssClass="form-control" TabIndex="5" ID="txtState" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="100" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Only alphabets  and space allowed.">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4">
                                                        <label for="Address2">
                                                            City<span class="text-red">*</span></label>
                                                        <asp:TextBox CssClass="form-control" TabIndex="6" ID="txtCity" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="50" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Only alphabets and space allowed.">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-4">
                                                        <label for="PhoneNo">
                                                            Phone Number</label>
                                                        <div class="clearfix">
                                                        </div>
                                                        <asp:DropDownList ID="ddlCode" TabIndex="7" runat="server" CssClass="form-control phcode"
                                                            Width="65px">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtPhoneNoStateCodedet" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="6" TabIndex="8" CssClass="form-control phnum" Width="50px" Style="margin-right: 2px;"></asp:TextBox>
                                                        <asp:TextBox ID="txtPhoneNo" TabIndex="9" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="10" CssClass="form-control phnum2" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Official Phone Number. In case it is a landline number, it should only be numbers, with area code (2-4 digits) and local number (6-8 digits) in separate boxes, and no special characters will be allowed. In case it is a mobile number, it should only be numbers, with a minimum length of 10, and no special characters will be allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="FaxNo">
                                                            Fax Number</label>
                                                        <div class="clearfix">
                                                        </div>
                                                        <asp:DropDownList ID="drpFx" TabIndex="10" runat="server" CssClass="form-control phcode">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtFaxNo" TabIndex="11" CssClass="form-control phnum" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="10" runat="server"></asp:TextBox>
                                                         <a data-toggle="tooltip" class="fieldinfo" title="Official Fax Number. It should only be numbers, with area code (2-4 digits) and local number (6-8 digits) in separate boxes, and no special characters will be allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><i>Specify with STD
                                                                Code, Example: 0674256123</i></small>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="Email">
                                                            Email Address<span class="text-red">*</span></label>
                                                        <asp:TextBox ID="txtEmail" TabIndex="12" CssClass="form-control" Onkeypress="return inputLimiter(event,'Email')"
                                                            MaxLength="50" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="It can accept both alphabets and numbers. Special Characters like '@', '-', '_', are allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row pinsection">
                                        <div class="col-md-3  col-sm-4">
                                            <label for="PINcode" class=" col-form-label">
                                                PIN Code<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtPinCode" TabIndex="13" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                MaxLength="6" runat="server"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Only Numbers are accepted and it should not start with zero. No special characters will be allowed">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                    </div>

                                        <div class="form-sec">
                                <div class="form-header">
                                    Correspondence Address &nbsp;<label><asp:CheckBox TabIndex="14" ID="chkBoxAddress"
                                        AutoPostBack="true" OnCheckedChanged="chkBoxAddress_CheckedChanged" CssClass="padding-left-20 " runat="server" Text="Address same as corporate address"></asp:CheckBox>
                                    </label>
                                </div>
                                <div class="form-body">
                                    <div class="form-group row row-xs">
                                        <label for="Address2" class="col-md-3 col-sm-4 col-form-label">
                                            Name of the Contact Person <span class="text-red">*</span>
                                        </label>
                                        <div class="col-md-3 col-sm-6">
                                            <asp:TextBox CssClass="form-control pull-left phnum" TabIndex="15" ID="txtCperson"
                                                Onkeypress="return inputLimiter(event,'NameCharacters')" MaxLength="100" runat="server"></asp:TextBox>
                                            <%--     <a data-toggle="tooltip" class="fieldinfo" title="It will accept only alphabets and spaces, no special characters are allowed">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>--%>
                                            <%--<a data-toggle="tooltip" class="fieldinfo" title="The applicant will fill details of the office that will be directly involved in executing the project. It can accept all characters">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-3 col-sm-12">
                                                <label for="Address1">
                                                    Address<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtCorAddrs" onkeyup="setvalue1();" TabIndex="16" TextMode="MultiLine"
                                                    CssClass="form-control" Onkeypress="return inputLimiter(event,'Address')" MaxLength="250"
                                                    Height="104px" runat="server"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="The applicant will fill details of the office that will be directly involved in executing the project. It can accept all characters">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><i>Maximum <span
                                                        id="SpanLbl1" class="mandatoryspan">250</span> characters left</i></small>
                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <div class="form-group row">
                                                    <div class="col-sm-4">
                                                        <label for="Country">
                                                            Country<span class="text-red">*</span></label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpCorCountry" runat="server"
                                                            AutoPostBack="true">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Country can be selected from the drop-down menu">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-sm-4" runat="server" id="st3">
                                                        <label for="State">
                                                            State<span class="text-red">*</span></label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="18" ID="drpCorState" runat="server">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="State can be selected from the drop-down menu">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <div class="col-sm-4" id="st4" runat="server">
                                                        <label for="State">
                                                            State<span class="text-red">*</span></label>
                                                        <asp:TextBox CssClass="form-control" TabIndex="19" ID="txtCorState" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="100" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Only alphabets  and space allowed.">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                    <%--  <div class="col-sm-3">
                                            <label for="State">
                                                District<span class="text-red">*</span></label>
                                            <asp:DropDownList CssClass="form-control" ID="drpCorDist" runat="server">
                                                <asp:ListItem Value="0">---Select---</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>
                                                    <div class="col-sm-4">
                                                        <label for="Address2">
                                                            City<span class="text-red">*</span></label>
                                                        <asp:TextBox CssClass="form-control" ID="txtCorCity" TabIndex="20" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="50" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Only alphabets and space allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-4">
                                                        <label for="PhoneNo">
                                                            Mobile Number<span class="text-red">*</span></label>
                                                        <div class="clearfix">
                                                        </div>
                                                        <asp:DropDownList ID="drpMob" TabIndex="21" runat="server" CssClass="form-control phcode">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtCorMob" TabIndex="22" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onclick="checkLength()" MaxLength="10" CssClass="form-control phnum" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Do not include +91 or start with 0">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="FaxNo">
                                                            Fax Number</label>
                                                        <div class="clearfix">
                                                        </div>
                                                        <asp:DropDownList ID="drpFax2" TabIndex="23" runat="server" CssClass="form-control phcode">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtCorFax" TabIndex="24" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            MaxLength="10" CssClass="form-control phnum" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="Official Fax Number. It should only be numbers, with area code (2-4 digits) and local number (6-8 digits) in separate boxes, and no special characters will be allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a><small><i>Specify with STD
                                                                Code, Example: 0674256123</i></small>
                                                        <div class="clearfix">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="Email">
                                                            Email Address<span class="text-red">*</span></label>
                                                        <asp:TextBox ID="txtCorEmailid" TabIndex="25" Onkeypress="return inputLimiter(event,'Email')"
                                                            MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="It can accept both alphabets and numbers. Special Characters like '@', '-', '_', are allowed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row pinsection">
                                        <div class="col-md-3 col-sm-4">
                                            <label for="PIN code">
                                                PIN Code<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtCorPin" TabIndex="26" Onkeypress="return inputLimiter(event,'Numbers')"
                                                MaxLength="6" CssClass="form-control" runat="server"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Only Numbers are accepted and it should not start with zero. No special characters will be allowed">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                        <div class="col-md-3 col-sm-4">
                                            <label for="Iname">
                                                Constitution of Company/Enterprise<span class="text-red">*</span></label>
                                            <asp:DropDownList ID="ddlConstitution" TabIndex="27" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Proprietorship" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Partnership" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Private Limited Company" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Public Limited Company" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="PSU" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="SPV" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Co-operative" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="Others" Value="8"></asp:ListItem>
                                            </asp:DropDownList>
                                            <a data-toggle="tooltip" class="fieldinfo" title="The applicant will select from the list of dropdown values and the default value shall be blank">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                        <div class="col-sm-3" id="DvOths">
                                            <label for="Iname">
                                                Others (Please specify)<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtOthrConsti" TabIndex="28" CssClass="form-control" Onkeypress="return inputLimiter(event,'OtherSpecify')"
                                                runat="server"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="In case, in the above dropdown values, “Others” is selected. A text box will appear and the applicant will enter the name. Only alphabets are allowed and the minimum length is 5">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                </div>
                            </div>
                             <div class="form-sec">
                                <div class="form-header">
                                    <span class="mandatoryspan pull-right" id="spmandFile">(File type allowed is pdf, .jpg,.png
                                        , Max Size 4 MB and for Memorandum & articles of association is 12 MB) </span>
                                    <h2>
                                        2.Entrepreneur Registration Details</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group" id="dvyearofIncrp">
                                        <div class="row pinsection">
                                            <div class="col-sm-4" id="DVC1">
                                                <label for="Designation">
                                                    Year of Establishment<span class="text-red">*</span></label>
                                                <%--<asp:TextBox ID="txtyrIncorporation" TabIndex="28" Onkeypress="return inputLimiter(event,'Numbers');"
                                                    MaxLength="4" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="drpYearofEstablishment" TabIndex="29" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Should be <= current year"><i class="fa fa-question-circle"
                                                    aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-4" id="DVC2">
                                                <label for="Others">
                                                    Place of incorporation</label>
                                                <asp:TextBox ID="txtPlaceIncorporation" TabIndex="30" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                    MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Place of Registration of the Company. It should be only alphabets and no special characters will be allowed">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="Others">
                                                    GSTIN<%--<span class="text-red">*</span>--%></label><asp:TextBox ID="txtGSTIN" Style="text-transform: capitalize:uppercase;"
                                                        TabIndex="31" Onkeypress="return inputLimiter(event,'GSTINDET')" MaxLength="15"
                                                        CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="form-group row pinsection upladdocs" id="dvPAN">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-4">
                                                        <label for="Others">
                                                            PAN<span class="text-red">*</span></label>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpldPan" TabIndex="32" CssClass="form-control" runat="server" />
                                                            <%--<asp:HiddenField ID="hdn1" runat="server" />--%>
                                                            <asp:HiddenField ID="hdnPanFile" runat="server" />
                                                            <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <asp:LinkButton ID="lnkPan"  runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelPan"  runat="server" CssClass="input-group-addon bg-red"> <i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink CssClass="input-group-addon bg-blue" ID="hlDoc1" Visible="false" runat="server"
                                                                Target="_blank">
                                                             <i class="fa  fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <asp:Label ID="lblPAN" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="PAN uploaded successfully."></asp:Label>
                                                        <asp:HiddenField ID="hdPn1" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkPan" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-4">
                                                        <label for="Others">
                                                            GSTIN</label>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpldGstn" TabIndex="35" CssClass="form-control" runat="server" />
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 1 Mb">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnGstinFile" runat="server" />
                                                            <asp:LinkButton ID="lnkGSTIN" runat="server" CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelGST"  runat="server"  CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i> </asp:LinkButton>
                                                            <asp:HyperLink CssClass="input-group-addon bg-blue" ID="hlDoc2" Visible="false" runat="server"
                                                                Target="_blank">
                                                             <i class="fa  fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <asp:Label ID="lblGSTIN" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="GSTIN uploaded successfully."></asp:Label>
                                                        <asp:HiddenField ID="hdPn2" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkGSTIN" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-4" id="DVC3">
                                                        <label for="Others">
                                                            Upload Memorandum & Articles of Association<span class="text-red">*</span></label>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpldMemo" TabIndex="38" CssClass="form-control" runat="server" />
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnMemoFile" runat="server" />
                                                            <%-- <asp:Button ID="btnUpload2" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <%-- <asp:LinkButton ID="lnkMemo" runat="server"></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lnkMemo"  runat="server"  CssClass="input-group-addon bg-green"> <i class="fa fa-upload " aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelMemo"  runat="server" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i> </asp:LinkButton>
                                                            <asp:HyperLink ID="hlDoc3" runat="server" Visible="false" Target="_blank" CssClass="input-group-addon bg-blue">
                                                             <i class="fa  fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <asp:Label ID="lblMemo" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Memorandum & Articles of Association uploaded successfully."></asp:Label>
                                                        <asp:HiddenField ID="hdPn3" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkMemo" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="form-group row pinsection upladdocs">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-4" id="DVC4">
                                                        <label for="Others">
                                                            Certificate of incorporation/Registration/Partnership Deed<span style="color: Red;
                                                                font-weight: bold;">*</span></label>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpldCerti" TabIndex="41" CssClass="form-control" runat="server" />
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            <asp:HiddenField ID="hdnCerti" runat="server" />
                                                            <%--<asp:Button ID="btnUpload3" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <asp:LinkButton ID="lnkCerti"  runat="server"  CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelCerti"  runat="server" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i> </asp:LinkButton>
                                                            <asp:HyperLink ID="hlDoc4" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa  fa-download" aria-hidden="true"></i></asp:HyperLink>
                                                        </div>
                                                        <asp:Label ID="lblCerti" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Certificate of incorporation/Registration/Partnership Deed uploaded successfully."></asp:Label>
                                                        <asp:HiddenField ID="hdPn4" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkCerti" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="col-sm-4">
                                                <label for="Others">
                                                    Project Type<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="drpProjectCat" TabIndex="44" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Project Cost >= 50 crore" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Project cost upto < 50 crore" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="The applicant will select either Large (for projects that cost more than INR 50 Cr) or MSME (for projects that cost less that INR 50 Cr) from the dropdown list">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="Others">
                                                    Application For<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="drpApplicationFor" TabIndex="45" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">New Unit</asp:ListItem>
                                                    <asp:ListItem Value="2">Expansion of existing unit</asp:ListItem>
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="New Unit/Expansion of Unit - Can be selected from the dropdown menu">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                       <div class="form-sec" id="dvPromoterNM1">
                                <div class="form-header">
                                    <h2>
                                        <asp:Label ID="lblDet" runat="server"></asp:Label>
                                    </h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group">
                                        <div class="row pinsection" id="DvtxtPromoter">
                                            <div class="col-sm-3">
                                                <label for="Others">
                                                    Name of Promoter<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtNameOfPromoter" TabIndex="46" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                    MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="It will accept only alphabets and spaces, no special characters are allowed">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                <%--<table class="table table-bordered">
                                                <tr>
                                                    <th>
                                                        Name of promoter
                                                    </th>
                                                    <th>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtNameOfPromoter" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                            MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" Text="Add More" ID="AddMore" CssClass="btn btn-mini btn-danger pull-right"
                                                            Height="40px" OnClick="AddMore_Click"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>--%>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3" id="DvtxtNoOfParter">
                                                <label for="Others">
                                                    Number of Partner(s) <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtNoOfParter" TabIndex="47" Onkeypress="return inputLimiter(event,'Numbers')"
                                                    MaxLength="4" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3" id="DvtxtNameOfMP">
                                                <label for="Others">
                                                    Name of the Managing Partner <span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtNameOfMP" TabIndex="48" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                    MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="dvNm">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:GridView ID="GrvNameOfPromoter" TabIndex="49" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                <asp:HiddenField ID="hdpid" Value='<%#Eval("intProId") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="vchNameOfPromoter" HeaderText="Name of Promoter" />
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-Height="45">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDelete" CssClass="btn btn-danger btn-sm" runat="server"
                                                                    ImageUrl="~/images/DeleteIcn.png" ToolTip="Click To Delete !"
                                                                    OnClientClick="return confirm('Are you sure you want to delete?');" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="DvDirectorww">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th align="center">
                                                            Name
                                                        </th>
                                                        <th align="center">
                                                            Designation
                                                        </th>
                                                        <th width="100px">
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtPName" TabIndex="50" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="It will accept only alphabets and spaces, no special characters are allowed. Up to 5 number of rows can be added">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPDesg" TabIndex="51" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" TabIndex="52" Text="Add" ID="AddMoreBD" CssClass="btn btn-primary">
                                                            </asp:Button>
                                                            <asp:HiddenField ID="hdna" Value="0" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-6 " id="GrdBD1">
                                                <asp:GridView ID="GrdBD" runat="server" TabIndex="53" AutoGenerateColumns="false" CssClass="table table-bordered margin-bottom0">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#" ItemStyle-Height="45">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                <asp:HiddenField ID="hdpid1" Value='<%#Eval("intProId1") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnName" Value='<%#Eval("vchName") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="vchName" HeaderText="Name" />
                                                        <asp:BoundField DataField="vchDesignation" HeaderText="Designation" />
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDelete1" CssClass="btn btn-danger" runat="server" ImageUrl="~/images/rubbish.png"
                                                                    OnClientClick="return confirm('Are you sure you want to delete?');" ToolTip="Click To Delete !" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row" id="DvEdu">
                                            <div class="col-sm-3" id="TgToo">
                                                <label for="Others">
                                                    Name<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="drpTagTo" TabIndex="54" CssClass="form-control" runat="server">
                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Others">
                                                    Educational Qualification<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="drpEducation" TabIndex="55" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="The applicant will select the Highest Educational Qualification of Promoter from the drop-down menu. (Only if the proposed project category is MSME)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Others">
                                                    Technical Qualification<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="drpTechnical" TabIndex="56" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="The applicant will select Technical Qualifications of Promoter from the drop-down menu. Any number of qualifications that are relevant to the project being applied for, can be selected. (Only if the proposed project category is MSME)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-sm-3">
                                                <label for="Others">
                                                    Experience in Years<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtexpinYr" TabIndex="57" Onkeypress="return inputLimiter(event,'Numbers')"
                                                    MaxLength="2" CssClass="form-control" runat="server"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Experience of the Promoter, in years.">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row" id="Enc">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-3">
                                                            <label for="Others">
                                                                Educational Qualification<span class="text-red">*</span></label>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpldEducational" TabIndex="58" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 4 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnEdu" runat="server" />
                                                                <asp:LinkButton ID="lnkQuali" runat="server" CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelEdu" runat="server" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc5" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                 <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblEdu" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Educational Qualification uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn5" runat="server" />
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkQuali" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-3">
                                                            <label for="Others">
                                                                Non Technical/Technical Qualification<span class="text-red" id="q1">*</span></label>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpldTechnical" TabIndex="59" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 4 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnTecnical" runat="server" />
                                                                <asp:LinkButton ID="lnkTechni" runat="server"  CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelTechni" runat="server" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc6" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                 <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblTechni" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Non Technical/Technical Qualification uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn6" runat="server" />
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkTechni" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-3">
                                                            <label for="Others">
                                                                Experience<span class="text-red">*</span></label>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpldExperience" TabIndex="60" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 4 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnExperience" runat="server" />
                                                                <asp:LinkButton ID="lnkExperience" runat="server" CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelExperience" runat="server"
                                                                    CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc7" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                     <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblExp" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Experience uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn7" runat="server" />
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkExperience" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                     <div class="form-sec">
                                <div class="form-header">
                                    <h2>
                                        4.Financial Status (INR in Lakhs)</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th align="center">
                                                        </th>
                                                        <th align="center">
                                                            <asp:DropDownList ID="drpFinYear1" TabIndex="61" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </th>
                                                        <th align="center">
                                                            <asp:DropDownList ID="drpFinYear2" TabIndex="62" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="2025">2025-2026</asp:ListItem>
                                                                <asp:ListItem Value="2024">2024-2025</asp:ListItem>
                                                                <asp:ListItem Value="2023">2023-2024</asp:ListItem>
                                                                <asp:ListItem Value="2022">2022-2023</asp:ListItem>
                                                                <asp:ListItem Value="2021">2021-2022</asp:ListItem>
                                                                <asp:ListItem Value="2020">2020-2021</asp:ListItem>
                                                                <asp:ListItem Value="2019">2019-2020</asp:ListItem>
                                                                <asp:ListItem Value="2018">2018-2019</asp:ListItem>
                                                                <asp:ListItem Value="2017">2017-2018</asp:ListItem>
                                                                <asp:ListItem Value="2016">2016-2017</asp:ListItem>
                                                                <asp:ListItem Value="2015">2015-2016</asp:ListItem>
                                                                <asp:ListItem Value="2014">2014-2015</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:Label ID="drpFinYear2" runat="server" Text=""></asp:Label>--%>
                                                        </th>
                                                        <th align="center">
                                                            <asp:DropDownList ID="drpFinYear3" TabIndex="63" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="2025">2025-2026</asp:ListItem>
                                                                <asp:ListItem Value="2024">2024-2025</asp:ListItem>
                                                                <asp:ListItem Value="2023">2023-2024</asp:ListItem>
                                                                <asp:ListItem Value="2022">2022-2023</asp:ListItem>
                                                                <asp:ListItem Value="2021">2021-2022</asp:ListItem>
                                                                <asp:ListItem Value="2020">2020-2021</asp:ListItem>
                                                                <asp:ListItem Value="2019">2019-2020</asp:ListItem>
                                                                <asp:ListItem Value="2018">2018-2019</asp:ListItem>
                                                                <asp:ListItem Value="2017">2017-2018</asp:ListItem>
                                                                <asp:ListItem Value="2016">2016-2017</asp:ListItem>
                                                                <asp:ListItem Value="2015">2015-2016</asp:ListItem>
                                                                <asp:ListItem Value="2014">2014-2015</asp:ListItem>
                                                                <asp:ListItem Value="2013">2013-2014</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label for="Others">
                                                                Annual turn over</label><span style="font-weight: bold;" class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAnnual1" TabIndex="64" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAnnual2" TabIndex="65" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                MaxLength="10" onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAnnual3" TabIndex="66" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                MaxLength="10" onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label for="Others">
                                                                Profit after tax</label><span style="font-weight: bold;" class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProfit1" TabIndex="67" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProfit2" TabIndex="68" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProfit3" TabIndex="69" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                    <tr id="dvReserveAndSurplus">
                                                        <td>
                                                            <label for="Others">
                                                                Reserve and surplus</label><span style="font-weight: bold;" class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReserve1" TabIndex="70" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="10" CssClass="form-control" onkeyup="sumExist();" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReserve2" TabIndex="71" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="10" CssClass="form-control" onkeyup="sumExist1();" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReserve3" TabIndex="72" Onkeypress="return inputLimiter(event,'Profit')"
                                                                MaxLength="10" CssClass="form-control" onkeyup="sumExist2();" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal and negative  allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                    <tr id="dvShareCapital">
                                                        <td>
                                                            <label for="Others">
                                                                Share capital</label>
                                                            <span style="font-weight: bold;" class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtShare1" TabIndex="73" onkeyup="sumExist();" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtShare2" TabIndex="74" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="10" CssClass="form-control" onkeyup="sumExist1();" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtShare3" TabIndex="75" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="10" CssClass="form-control" onkeyup="sumExist2();" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label for="Others">
                                                                Net worth</label><span style="font-weight: bold;" class="text-red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNtWorth1" TabIndex="76" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNtWorth2" TabIndex="77" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNtWorth3" TabIndex="78" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                                MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Only 2 digit after decimal allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row pinsection">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group ">
                                                        <label for="Others" class="col-md-5 col-sm-6">
                                                            <asp:Label ID="lblf1" runat="server" Text="Upload Audited Financial Statements for First Year"></asp:Label>
                                                            <%--Upload Audited Financial Statements for First Year--%>
                                                            <asp:Label ID="lblFirstYear" runat="server" Style="font-weight: bold;" Text=""></asp:Label><span
                                                                id="dv123" class="text-red">*</span></label>
                                                        <div class="col-md-6 col-sm-6">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileUpldAudited" TabIndex="79" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnAudit" runat="server" />
                                                                <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                <asp:LinkButton ID="lnkAudit" runat="server" CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDelAudit" runat="server" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc8" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                              <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblAudit1" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Audited Financial Statements for First Year uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn8" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkAudit" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="row pinsection">
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label for="Others" class="col-md-5 col-sm-6">
                                                            <asp:Label ID="lblf2" runat="server" Text="Upload Audited Financial Statements for Second Year"></asp:Label>
                                                            <asp:Label ID="lblSecondYear" Style="font-weight: bold;" runat="server" Text=""></asp:Label><span
                                                                id="adt1" class="text-red">*</span></label>
                                                        <div class="col-md-6 col-sm-6">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileUploadSecond" TabIndex="80" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnFySecond" runat="server" />
                                                                <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                <asp:LinkButton ID="lnkFySecond" runat="server" CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkFySecondDelete" runat="server" CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc12" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblAudit2" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Audited Financial Statements for Second Year uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn10" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkFySecond" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="row pinsection">
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label for="Others" class="col-md-5 col-sm-6">
                                                            <asp:Label ID="lblf3" runat="server" Text="Upload Audited Financial Statements for Third Year"></asp:Label>
                                                            <asp:Label ID="lblThirdYear" Style="font-weight: bold;" runat="server" Text=""></asp:Label><span
                                                                id="adt2" class="text-red">*</span></label>
                                                        <div class="col-md-6 col-sm-6">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileUploadThird" TabIndex="81" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnFyThird" runat="server" />
                                                                <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                <asp:LinkButton ID="lnkFyThird" runat="server" CssClass="input-group-addon bg-green"
                                                                    ><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkFyThirdDel" runat="server" CssClass="input-group-addon bg-red"
                                                                  ><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc13" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                               <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblAudit3" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Audited Financial Statements for Third Year uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn11" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkFyThird" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="row pinsection" runat="server" id="dvfy4">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label for="Others" class="col-md-5 col-sm-6">
                                                            <asp:Label ID="lblIncomeTax" runat="server" Text="Income tax return"></asp:Label>
                                                            <span id="Span1" class="text-red">*</span></label>
                                                        <div class="col-md-6 col-sm-6">
                                                            <div class="input-group">
                                                                <span class="colon">:</span>
                                                                <asp:FileUpload ID="FileUploadFourthupd" TabIndex="82" CssClass="form-control" runat="server" />
                                                                <a data-toggle="tooltip" class="fieldinfo" title="Upload pdf, jpeg with size < = 12 Mb">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                <asp:HiddenField ID="hdnFyFourth" runat="server" />
                                                                <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                <asp:LinkButton ID="lnkFyFourth" runat="server" CssClass="input-group-addon bg-green"
                                                                    ><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkFyFourDel" runat="server" CssClass="input-group-addon bg-red"
                                                                    ><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hlDoc14" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                               <i class="fa  fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <asp:Label ID="lblAudit4" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Audited Financial Statements for Third Year uploaded successfully"></asp:Label>
                                                            <asp:HiddenField ID="hdPn14" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkFyFourth" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="row pinsection">
                                            <div class="form-group">
                                                <label for="Others" class="col-md-4 col-sm-6">
                                                    <asp:Label ID="lblf4" runat="server" Text="(financial statements,profit/loss accounts,balance sheet)"></asp:Label>
                                                    <%-- (financial statements,profit/loss accounts,balance sheet)--%>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row pinsection">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                     <div class="form-group" id="DvNetWorth">
                                                    
                                                        <label for="Others" class="col-md-5 col-sm-6">
                                                            Net worth certified by CA<span class="text-red">*</span></label>
                                                        <div class="col-md-6 col-sm-6">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FileUpldNetWorth" TabIndex="83" CssClass="form-control" runat="server" />
                                                            <asp:HiddenField ID="hdnNetWorth" runat="server" />
                                                            <%--<asp:Button ID="btnUpload8" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <asp:LinkButton ID="lnknetWorth" runat="server" CssClass="input-group-addon bg-green"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelnetWorth" runat="server"
                                                                CssClass="input-group-addon bg-red"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hlDoc9" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                               <i class="fa  fa-download"></i>
                                                            </asp:HyperLink>
                                                        </div>
                                                            </div>
                                                        <asp:Label ID="lblNet" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Net worth certified by CA uploaded successfully"></asp:Label>
                                                        <asp:HiddenField ID="hdPn9" runat="server" />
                                                     
                                                         </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnknetWorth" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-header" id="DvIndustry1">
                                        <h2>
                                            5.Existing Industry details</h2>
                                    </div>
                                    <div class="form-body" id="DvIndustry">
                                        <div class="form-group">
                                            <div class="row ">
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        Existing industry name</label>
                                                    <asp:TextBox ID="txtExtIndName" MaxLength="100" TabIndex="84" CssClass="form-control"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Iname">
                                                        District<span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" TabIndex="85" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        Block<span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="drpBlock" TabIndex="86" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="ICapacity">
                                                        Whether land allotted by IDCO<span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlAlloted" TabIndex="87" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4" id="divLD">
                                                    <label for="Capacity">
                                                        Extent of land(in acres)<span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtExtentLand" TabIndex="88" CssClass="form-control" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                        onblur="isNumberBlur(event, this, 2);" runat="server" Onkeypress="return inputLimiter(event,'Decimal')"
                                                        MaxLength="10"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4" id="divLD1">
                                                    <label for="Capacity">
                                                        Nature of activity<span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtNatureAct" TabIndex="89" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')"
                                                        MaxLength="100" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        Sector<span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlsector" TabIndex="90" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        Sub Sector<span class="text-red">*</span></label>
                                                    <asp:DropDownList ID="ddlSubSec" TabIndex="91" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        Capacity<span class="text-red">*</span></label>
                                                    <div class="clearfix">
                                                    </div>
                                                    <asp:TextBox ID="txtCapacity" TabIndex="92" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                        onblur="isNumberBlur(event, this, 2);" Onkeypress="return inputLimiter(event,'Decimal')"
                                                        MaxLength="10" CssClass="form-control" Style="width: 58%; float: left;" runat="server"></asp:TextBox>
                                                    <asp:DropDownList ID="drpCp" TabIndex="93" runat="server" CssClass="form-control" Style="width: 40%;
                                                        float: left; margin-left: 5px">
                                                    </asp:DropDownList>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                <div class="col-sm-3" id="dvoth">
                                                    <label for="Iname">
                                                        Others (Please specify)<span class="text-red">*</span></label>
                                                    <asp:TextBox ID="txtCapOthr" TabIndex="94" Onkeypress="return inputLimiter(event,'OtherSpecify')"
                                                        MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                    </div>
                                    <div id="DvRaw">
                                        <div class="form-group">
                                            <div class="row ">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>
                                                                Raw material for production
                                                            </th>
                                                            <th>
                                                                Material source
                                                            </th>
                                                            <th>
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtRawMaterial" TabIndex="95" Onkeypress="return inputLimiter(event,'RawMetrial')"
                                                                    MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="drprawMeterial" TabIndex="96" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Within the state" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Outside the state" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Imported" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="100px">
                                                                <asp:Button runat="server" Text="Add" TabIndex="97" ID="btnAddMoreRWM" CssClass="btn btn-mini btn-primary"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="GrvRWM" runat="server" TabIndex="98" AutoGenerateColumns="false" CssClass="table table-bordered margin-bottom0">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdpid2" Value='<%#Eval("intProId2") %>' runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="vchRawMaterial" HeaderText="Raw material for production" />
                                                            <asp:BoundField DataField="vchRawMeterialSrc" HeaderText="Material source" />
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnDelete2" CssClass="btn btn-danger btn-sm" runat="server"
                                                                        ImageUrl="~/images/rubbish.png" OnClientClick="return confirm('Are you sure you want to delete?');"
                                                                        ToolTip="Click To Delete !" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="p-xs text-center">
                                    <asp:Button ID="btnNext" runat="server" TabIndex="99" Text="Next" CssClass=" btn btn-success" />
                                    <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save As Draft" CssClass=" btn btn-primary draftbtn noprint" />
                                    <%-- <input type="reset" text="Reset" class=" btn btn-reset" />--%>
                                    <asp:Button ID="btnReset" runat="server" TabIndex="100" Text="Reset" CssClass="btn btn-danger" />
                                    <asp:HiddenField ID="hdnAllFileValue" runat="server" />
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
    </div>
    
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

