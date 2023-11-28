<%--'*******************************************************************************************************************
' File Name         : Update_PAN.aspx
' Description       : This page is used to update PAN of investors, which have not provided during registration.
' Created by        : Sushant Kumar Jena
' Created On        : 31-May-2018
' Modification History:

'<CR no.>     <Date>          <Modified by>        <Modification Summary>                                               <Instructed By>                                                     
    1       03-Aug-2018       Sushant Jena         New control added to update EIN/IEM Number.                          Smruti Ranjan Nayak
' *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_PAN.aspx.cs" Inherits="Update_PAN" %>


<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script src="Portal/js/jQuery-2.1.3.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*-------------------------------------------------------------*/

        function validatePanUpdate() {
            if (!blankFieldValidation('Txt_PAN', 'PAN', projname)) {
                return false;
            }
            if ($('#Txt_PAN').val().length != 10) {
                jAlert('<strong>PAN Should be 10 Digits.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_PAN").focus(); });
                return false;
            }
            var txt_pan = $('#Txt_PAN').val().toUpperCase();
            var checkval = /^[A-Z]{5}\d{4}[A-Z]{1}$/;
            if (checkval.test(txt_pan) == false) {
                jAlert('<strong>Please Enter a Valid PAN !</strong>', projname);
                $('#Txt_PAN').val('');
                return false;
            }
            if (DropDownValidation('DrpDwn_License_Type', '0', 'EIN/IEM Type', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_License_Type").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_EIN_IEM', 'EIN/IEM/Udyog Aadhaar/Production Certificate/Udayam Registration', projname)) {
                return false;
            }
            if ($('#FileUpload_Licence_Doc').val() == "") {
                jAlert('<strong>Please upload document in support of EIN/IEM/Udyog Aadhaar/Production Certificate/Udayam Registration !</strong>');
                $("#FileUpload_Licence_Doc").focus();
                return false;
            }
        }

        /*-------------------------------------------------------------*/
        /////// Alert message and redirect to login page
        /*-------------------------------------------------------------*/
        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    location.href = 'Login.aspx';
                    return true;
                }
                else {
                    return false;
                }
            });
        }


        function alertredirectEinPc(msg, redirectUrl) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    location.href = redirectUrl;
                    return true;
                }
                else {
                    return false;
                }
            });
        }



        /*--------------------------------------------------------------*/
        
        function showDocName() {
            var indType = $('#Hid_Industry_Type').val();
            var docName = $('#DrpDwn_License_Type').val();
            if (docName != '0') {
                $('#Lbl_Doc_Name').html("Upload " + docName + " Document");

                $('#LnkBtn_EIN').hide();
                $('#spnIEMNo').hide();
                $('#spnUAadhaarNo').hide();
                

                if (docName == "EIN") {
                    $('#LnkBtn_EIN').show();
                }
                else if (docName == "IEM") {
                    $('#spnIEMNo').show();
                }
                else if (docName == "Udyog Aadhaar") {
                    $('#spnUAadhaarNo').show();
                }
                else if (docName == "Production Certificate") {
                    $('#LnkBtn_EIN').show();
                }
                else if (docName == "Udayam Registration") {
                    $('#LnkBtn_EIN').show();
                }
                else {
                    if (indType == 1) ////Large 
                    {
                        $('#spnIEMNo').show();
                        $('#spnUAadhaarNo').show();
                    }
                    else if (indType == 2) ////MSME
                    {
                        $('#LnkBtn_EIN').show();
                    }
                }
            }
            else {
               
                if (indType == 1) ////Large 
                {
                    $('#spnIEMNo').show();
                    $('#spnUAadhaarNo').show();
                    $('#Lbl_Doc_Name').html("Upload IEM/Udyog Aadhaar Document");
                }
                else if (indType == 2) ////MSME
                {
                    $('#LnkBtn_EIN').show();
                    $('#Lbl_Doc_Name').html("Upload EIN/Production Certificate Document/Udayam Registration");
                }
            }
        }

    </script>
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 900px;
            height: 550px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="registration-div investors-bg">
            <div id="exTab1">
                <div class="form-sec">
                    <div class="m-b-10">
                    </div>
                    <div class="form-header">
                        <h2>
                            Update PAN and EIN/IEM/PC/Udyog Aadhaar</h2>
                    </div>
                    <div class="form-body">
                        <div class="formbodycontent">
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-3">
                                        User Id</label>
                                    <div class="col-sm-5">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_User_Id" runat="server" CssClass="form-control-static"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-3">
                                        Enter PAN</label>
                                    <div class="col-sm-9 col-md-4">
                                        <span class="colon">:</span>
                                        <asp:TextBox ID="Txt_PAN" runat="server" CssClass="form-control" MaxLength="10" Style="text-transform: uppercase;"
                                            autocomplete="off" AutoCompleteType="None"></asp:TextBox>
                                        <span class="mandetory">*</span>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label for="EINIEM" class="col-sm-3">
                                        EIN/IEM/Udyog Aadhaar/Production Certificate/Udayam Registration
                                    </label>
                                    <div class="col-md-1 col-sm-2 padding-right-0">
                                        <span class="colon">:</span>
                                        <asp:DropDownList ID="DrpDwn_License_Type" runat="server" CssClass="form-control"
                                            onchange="return showDocName();">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            <asp:ListItem>EIN</asp:ListItem>
                                            <asp:ListItem>IEM</asp:ListItem>
                                            <asp:ListItem>Production Certificate</asp:ListItem>
                                            <asp:ListItem>Udyog Aadhaar</asp:ListItem>
                                            <asp:ListItem>Udayam Registration</asp:ListItem>
                                        </asp:DropDownList>
                                        <span class="mandetory">*</span>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:TextBox ID="Txt_EIN_IEM" CssClass="form-control" runat="server" autocomplete="off"
                                            MaxLength="50"></asp:TextBox>
                                        <span class="mandetory">*</span>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label for="EINIEM" class="col-sm-3">
                                    </label>
                                    <div class="col-sm-5 ">
                                        <asp:LinkButton ID="LnkBtn_EIN" runat="server" OnClick="LnkBtn_EIN_Click" ToolTip="Click here to apply EIN/PC.">Don't have EIN/PC ? Click here to apply for EIN/PC.</asp:LinkButton>
                                        <span id="spnIEMNo" style="display: block" runat="server"><a href=" https://services.dipp.gov.in/lms/login"
                                            target="_blank">Click here to apply for IEM number.</a></span> <span id="spnUAadhaarNo"
                                                style="display: block" runat="server"><a href=" https://udyogaadhaar.gov.in/UA/UAM_Registration.aspx"
                                                    target="_blank">Click here to apply for Udyog Aadhaar number.</a></span>
                                        <span id="Udayamreg" style="display: block;
                                                                        font-family: Verdana; font-size: 11px;"><a href="https://udyamregistration.gov.in/"
                                                                            target="_blank">Click here to apply for Udayam Registration.</a></span>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label for="EINIEM" class="col-sm-3">
                                        <asp:Label ID="Lbl_Doc_Name" runat="server" Text="Upload EIN/IEM/Udyog Aadhaar/Production Certificate Document/Udayam Registration"></asp:Label>
                                    </label>
                                    <div class="col-sm-9 col-md-4">
                                        <span class="colon">:</span>
                                        <asp:FileUpload ID="FileUpload_Licence_Doc" CssClass="form-control" runat="server"
                                            onchange="return validateFile(this);" ToolTip="Browse File to Upload !!" />
                                        <span class="mandetory">*</span> <small class="text-danger">(.pdf file only and Max
                                            file size 4 MB)</small>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-3">
                                    </label>
                                    <div class="col-sm-4">
                                        <asp:Button ID="Btn_Update" runat="server" Text="Update" class="btn btn-success"
                                            OnClick="Btn_Update_Click" OnClientClick="return validatePanUpdate();" />
                                        <asp:HiddenField ID="Hid_Industry_Type" runat="server" />
                                                   <asp:HiddenField ID="Hid_Temp_Used" runat="server" />
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
        <asp:HiddenField ID="Hid_Pop" runat="server" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
            TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="Btn_Close">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
            <div id="undertakingipr2015">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header bg-purpul">
                            <h4 class="modal-title">
                                Alert</h4>
                        </div>
                        <div class="modal-body">
                            <p>
                                The PAN provided by you already exists for the following unit:
                            </p>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Unit Name</label>
                                    <div class="col-sm-7">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Unit_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                        <asp:HiddenField ID="Hid_Investor_Id" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        Location</label>
                                    <div class="col-sm-7">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_Unit_Address" runat="server" CssClass="form-control-static"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-sm-2">
                                        PAN</label>
                                    <div class="col-sm-7">
                                        <span class="colon">:</span>
                                        <asp:Label ID="Lbl_PAN" runat="server" CssClass="form-control-static"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div>
                                Do you want to make this user as a subsidiary of the above unit?
                                <br />
                                If Yes, Please click on OK button
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Button ID="Btn_Yes" runat="server" Text="Yes" OnClick="Btn_Yes_Click" class="btn btn-success"
                                        ToolTip="Click Here to Proceed" OnClientClick="return confirm('Are you sure to proceed !!')" />
                                    <asp:Button ID="Btn_Close" runat="server" Text="No" class="btn btn-danger" ToolTip="Click Here to Close Window" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
        </asp:Panel>
    </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
</body>
</html>

