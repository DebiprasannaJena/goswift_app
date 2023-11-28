<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="AddGrievanceDepartment.aspx.cs" Inherits="Portal_Grievance_AddGrievanceDepartment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
     <link rel="stylesheet" type="text/css" href="../css/custom.css" />
    
    
       <script type="text/javascript">

           $(document).ready(function () {
           
               $('.ddlInvestor').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });
           });
           function inputLimiter(e, allow) {

               var AllowableCharacters = '';
               if (allow == 'NameCharacters') {
                   AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
               }
               if (allow == 'NameCharactersAndNumbers') {
                   AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
               }
               if (allow == 'Numbers') {
                   AllowableCharacters = '1234567890';
               }
               if (allow == 'Decimal') {
                   AllowableCharacters = '1234567890.';
               }
               if (allow == 'Email') {
                   AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._';
               }
               if (allow == 'Address') {
                   AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
               }
               if (allow == 'DateFormat') {
                   AllowableCharacters = '1234567890/-';
               }
               if (allow == 'NumbersSSN') {
                   AllowableCharacters = '1234567890-';
               }
               if (allow == 'RawMetrial') {
                   AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
               }
               var k;
               k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
               if (k != 13 && k != 8 && k != 0) {
                   if ((e.ctrlKey == false) && (e.altKey == false)) {
                       return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
                   }
                   else {
                       return true;
                   }
               }
               else {
                   return true;
               }
           }

           function limitText(limitField, limitCount, limitNum) {
               if (limitField.value.length > limitNum) {
                   limitField.value = limitField.value.substring(0, limitNum);
               } else {
                   limitCount.value = limitNum - limitField.value.length;
               }
           }

           var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

           function Validate() {

               if ($('#ContentPlaceHolder1_LblCompanyName').text() == "") {
                   jAlert('<strong>Please enter company name !</strong>', projname);
                   return false;
               }
               if (blankFieldValidation('ContentPlaceHolder1_TxtApplicantName', 'Applicant name', projname) == false) {
                   return false;
               }
               if (blankFieldValidation('ContentPlaceHolder1_TxtDesignation', 'Designation', projname) == false) {
                   return false;
               }
               if (blankFieldValidation('ContentPlaceHolder1_TxtMobileNo', 'Mobile number', projname) == false) {
                   return false;
               }
               if ($('#ContentPlaceHolder1_TxtMobileNo').val().substring(0, 1) == '0') {
                   jAlert('<strong>Mobile number should not be start with zero !</strong>', projname);
                   $('#ContentPlaceHolder1_TxtMobileNo').val('');
                   $('#ContentPlaceHolder1_TxtMobileNo').focus();
                   return false;
               }
               if ($('#ContentPlaceHolder1_TxtMobileNo').val().length != 10) {
                   jAlert('<strong>Mobile number should be 10 digits !</strong>', projname);
                   $("#ContentPlaceHolder1_TxtMobileNo").focus();
                   return false;
               }
               if (WhiteSpaceValidation1st('ContentPlaceHolder1_TxtMobileNo', 'Mobile number', projname) == false) {
                   return false;
               }
               if (DropDownValidation('ContentPlaceHolder1_DrpDwnInvestmentLevel', '0', 'Investment level', projname) == false) {
                   $("#popup_ok").click(function () { $("#ContentPlaceHolder1_DrpDwnInvestmentLevel").focus(); });
                   return false;
               }
               if (DropDownValidation('ContentPlaceHolder1_DrpDwnDistrict', '0', 'district', projname) == false) {
                   $("#popup_ok").click(function () { $("#ContentPlaceHolder1_DrpDwnDistrict").focus(); });
                   return false;
               }
               if (blankFieldValidation('ContentPlaceHolder1_TxtEmail', 'Email id', projname) == false) {
                   return false;
               }
               if ($('#ContentPlaceHolder1_TxtEmail').val() != "") {
                   var email = $('#ContentPlaceHolder1_TxtEmail').val();
                   var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                   if (filter.test(email) == false) {
                       jAlert('<strong>Invalid email address !</strong>', projname);
                       $("#ContentPlaceHolder1_TxtEmail").focus();
                       return false;
                   }
               }
               if (DropDownValidation('ContentPlaceHolder1_DrpDwnGrivType', '0', 'grievance type', projname) == false) {
                   $("#popup_ok").click(function () { $("#ContentPlaceHolder1_DrpDwnGrivType").focus(); });
                   return false;
               }
               if (DropDownValidation('ContentPlaceHolder1_DrpDwnGrivSubType', '0', 'grievance sub type', projname) == false) {
                   $("#popup_ok").click(function () { $("#ContentPlaceHolder1_DrpDwnGrivSubType").focus(); });
                   return false;
               }
               if (blankFieldValidation('ContentPlaceHolder1_TxtGrievanceTitle', 'Grievance title', projname) == false) {
                   return false;
               }
               if (blankFieldValidation('ContentPlaceHolder1_TxtGrievanceDetail', 'Grievance detail', projname) == false) {
                   return false;
               }
           }

       </script>

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

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>

    <style>
.chosen-rtl .chosen-drop { left: -9000px; }
.chosen-container .chosen-container-single .chosen-single{ width:100% !important;}
.searchbox {
background-color: #def3ff;
padding: 8px;
}
</style>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <div class="content-wrapper">
     <div class="content-header">
        <div class="header-icon">
            <i class="fa fa-dashboard"></i>
        </div>
        <div class="header-title">
            <h1>Add Grievance</h1>
            <ul class="breadcrumb">
                <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                <li><a>Grievance</a></li>
                <li><a>Add Grievance </a></li>
            </ul>
        </div>
    </div>
               
    <div class="content">

        <div class="form-sec">
            
            <div class="form-header">
                
                <span class="mandatoryspan pull-right">( * ) Marked fields are mandatory</span>

                <h2 class="mt-0 mb-0">Search</h2> <%-- class="form-control" form-sec  form-body pd-l-r-10  --%>
            </div>
            <div class="search-sec">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-3 col-sm-3">
                            <label for="company">
                                Name of the Company/Enterprise :
                            </label>
                            
                        </div>
                        <div class="col-md-3 col-sm-3">
                           
                            <asp:DropDownList  TabIndex="0" ID="ddlInvestor"  CssClass="chosen-select-width ddlInvestor" runat="server"> </asp:DropDownList> 
                        </div>

                        <div class="col-sm-4">
                                <asp:Button ID="BtnSearch" TabIndex="1" runat="server" OnClick="BtnSearch_Click" Text="Search" class="btn btn-add" ></asp:Button>
                               
                        </div>
                                            
                    </div>
                </div>
            </div>


        </div>
        <div class="form-sec">
            
            <div class="form-header">
                
                <span class="mandatoryspan pull-right">( * ) Marked fields are mandatory</span>

                <h2 class="mt-0 mb-0">1.Company Information</h2> <%--  form-sec  form-body pd-l-r-10  --%>
            </div>
            <div class="search-sec">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-3 col-sm-12">
                            <label for="company">
                                Name of the Company/Enterprise
                            </label>
                            <asp:Label ID="LblCompanyName" runat="server" class="form-control"></asp:Label>
                        </div>
                        <div class="col-md-9 col-sm-12">
                            <div class="form-group row">
                                <div class="col-md-4 col-sm-4">
                                    <label for="applicant">
                                        Name of the Applicant <span class="text-red">*</span></label>
                                    <asp:TextBox ID="TxtApplicantName" TabIndex="2" runat="server" class="form-control" autocomplete="off" placeholder="Enter Applicant Name" ToolTip="Enter applicant name here."
                                        Onkeypress="return inputLimiter(event,'NameCharacters')" MaxLength="100"></asp:TextBox>
                                </div>
                                <div class="col-md-4 col-sm-4">
                                    <label for="desig">
                                        Designation<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TxtDesignation" TabIndex="3" runat="server" class="form-control" autocomplete="off" placeholder="Enter Designation" ToolTip="Enter designation of the applicant here."
                                        Onkeypress="return inputLimiter(event,'NameCharacters')" MaxLength="100"></asp:TextBox>
                                </div>
                                <div class="col-md-4 col-sm-4">
                                    <label for="mobile">
                                        Mobile Number<span class="text-red">*</span></label>
                                    <asp:TextBox ID="TxtMobileNo" TabIndex="4" runat="server" class="form-control" MaxLength="10" autocomplete="off" placeholder="Enter Mobile No" ToolTip="Enter mobile number of the applicant here."
                                        Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-9 col-sm-12">
                            <div class="form-group row">
                                <div class="col-md-4 col-sm-4">
                                    <label for="Grievanceinvestmentlebel">
                                        Investment Level <span class="text-red">*</span></label>
                                    <asp:DropDownList ID="DrpDwnInvestmentLevel" TabIndex="5" runat="server" class="form-control" ToolTip="Select investment level here.">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Project Cost >= 50 crore" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Project cost upto < 50 crore" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 col-sm-4">
                                    <label for="Grievancedistrict">
                                        District <span class="text-red">*</span></label>
                                    <asp:DropDownList ID="DrpDwnDistrict" TabIndex="6" runat="server" class="form-control" ToolTip="Choose your district here.">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 col-sm-4">
                                    <label for="Grievanceemail">
                                        Email Id <span class="text-red">*</span></label>
                                    <asp:TextBox ID="TxtEmail" TabIndex="7" runat="server" class="form-control" autocomplete="off" placeholder="Enter Email Address" ToolTip="Enter email address of the applicant here."
                                        Onkeypress="return inputLimiter(event,'Email')"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
        <div class="form-sec">
            <div class="form-header">
                <h2 class="mt-0 mb-0">2.Grievance Information</h2>
            </div>
            <div class="search-sec">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="row">
                                <label for="Grievancetype" class="col-sm-3  col-md-2">
                                    Grievance Type <span class="text-red">*</span></label>
                                <div class="col-sm-7">
                                    <span class="colon">:</span>
                                    <asp:DropDownList ID="DrpDwnGrivType" TabIndex="8" runat="server" class="form-control" OnSelectedIndexChanged="DrpDwnGrivType_SelectedIndexChanged" ToolTip="Select grievance type here."
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label for="Grievancesubtype" class="col-sm-3 col-md-2">
                                    Grievance Sub Type <span class="text-red">*</span></label>
                                <div class="col-sm-7">
                                    <span class="colon">:</span>
                                    <asp:DropDownList ID="DrpDwnGrivSubType" TabIndex="9" runat="server" class="form-control" ToolTip="Select grievance sub type here.">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="form-group">
                    <div class="row">
                        <label for="Topic" class="col-sm-3 col-md-2">
                            Grievance Title <span class="text-red">*</span></label>
                        <div class="col-sm-7">
                            <span class="colon">:</span>
                            <asp:TextBox ID="TxtGrievanceTitle" TabIndex="10" runat="server" class="form-control" autocomplete="off" placeholder="Grievance Title" ToolTip="Put the title of your grievance here."
                                Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" MaxLength="150"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <label for="Grievancedetail" class="col-sm-3 col-md-2">
                            Grievance Detail <span class="text-red">*</span></label>
                        <div class="col-sm-7">
                            <span class="colon">:</span>
                            <asp:TextBox ID="TxtGrievanceDetail" TabIndex="11" runat="server" class="form-control" autocomplete="off" ToolTip="Fill your grievance details here."
                                TextMode="MultiLine" MaxLength="1000" Height="200px" onKeyUp="limitText(this,this.form.count,1000);"
                                onKeyDown="limitText(this,this.form.count,1000);"></asp:TextBox>
                            <span class="mandatory" style="font-size: 14px; color: red"><small>Maximum
                            <input id="count" class="inputCss" readonly="readonly" style="font-weight: bold; color: red; width: 32px;"
                                type="text" value="1000" />
                                Characters Left</small></span>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                TargetControlID="txtGrievanceDetail" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                ValidChars="a-zA-Z0-9-. ">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <label for="attachment1" class="col-sm-3 col-md-2">
                            Attachment 1</label>
                        <div class="col-sm-7">
                             <div class="input-group">
                                <asp:FileUpload ID="fileUpldAttach1"  CssClass="form-control" runat="server" ToolTip="Upload your attachment here."/>
                                <asp:HiddenField ID="HdnAttach1" runat="server" />
                                <asp:LinkButton ID="LnkBtnUploadAttach1"  runat="server" CssClass="input-group-addon bg-green"
                                    OnClick="LnkBtnUploadAttach1_Click" ToolTip="Click Here to Upload File !!"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                <asp:LinkButton ID="LnkBtnDelAttach1"  runat="server" CssClass="input-group-addon bg-red"
                                    OnClick="LnkBtnDelAttach1_Click" Visible="false" ToolTip="Click Here to Delete File !!"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                <asp:HyperLink ID="HypLnkAttach1"  runat="server" Target="_blank" Visible="false"
                                    CssClass="input-group-addon bg-blue" ToolTip="Click Here to View File !!"><i class="fa fa-download"></i></asp:HyperLink>
                             </div>

                            <small class="text-danger">(.pdf/.jpg/.jpeg/.png file only and Max file size 5 MB)</small>
                            <asp:Label ID="LblAttach1" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                runat="server" Text="Attachment 1 uploaded successfully">
                            </asp:Label>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <label for="attachment2" class="col-sm-3 col-md-2">
                            Attachment 2</label>
                        <div class="col-sm-7">
                          

                            <div class="input-group">
                                <asp:FileUpload ID="fileUpldAttach2" CssClass="form-control" runat="server" ToolTip="Upload your attachment here."/>
                                <asp:HiddenField ID="HdnAttach2" runat="server" />
                                <asp:LinkButton ID="LnkBtnUploadAttach2" runat="server" CssClass="input-group-addon bg-green"
                                    OnClick="LnkBtnUploadAttach2_Click" ToolTip="Click Here to Upload File !!"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                <asp:LinkButton ID="LnkBtnDelAttach2" runat="server" CssClass="input-group-addon bg-red"
                                    OnClick="LnkBtnDelAttach2_Click" Visible="false" ToolTip="Click Here to Delete File !!"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                <asp:HyperLink ID="HypLnkAttach2" runat="server" Target="_blank" Visible="false"
                                    CssClass="input-group-addon bg-blue" ToolTip="Click Here to View File !!"><i class="fa fa-download"></i></asp:HyperLink>
                             </div>


                            <small class="text-danger">(.pdf/.jpg/.jpeg/.png file only and Max file size 5 MB)</small>
                            <asp:Label ID="LblAttach2" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                runat="server" Text="Attachment 2 uploaded successfully">
                            </asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-sec">
            <div class="p-xs text-center">
                <asp:Button ID="BtnSave" runat="server" Text="Save" class="btn btn-success" OnClick="BtnSave_Click"
                    OnClientClick="return Validate();" ToolTip="Click here to submit your Grievance." />
                <asp:Button ID="BtnReset" runat="server" Text="Reset" class="btn btn-danger" OnClick="BtnReset_Click" ToolTip="Click here to reset." />
            </div>
        </div>
    </div>
          <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
 
          <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>  
   </div>
</asp:Content>
