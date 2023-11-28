<%--'*******************************************************************************************************************
' File Name         : OFDCDetails.aspx
' Description       : Details of OFDC
' Created by        : Manoj Kumar Behera
' Created On        : 11 SEP 2019
'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OFDCDetails.aspx.cs" Inherits="OFDC_Details"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/webfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/rightpannel.ascx" TagName="rightpanel" TagPrefix="uc4" %>
<html>
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <title>SWP(Single Window Portal)</title>
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src='//code.jquery.com/jquery-1.8.3.js'></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker3.min.css">
    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/js/bootstrap-datepicker.min.js"></script>
    <script type='text/javascript'>
        $(function () {
            $('.input-group.date').datepicker({
                calendarWeeks: true,
                todayHighlight: true,
                autoclose: true
            });
        });

    </script>
    <style>
        .panel-default > .panel-heading
        {
            padding: 8px 15px !important;
            font-size: 18px;
            text-transform: uppercase;
        }
        .note
        {
            border: 1px solid #f0f0f0;
            padding: 15px;
            border-radius: 4px;
            background: #f9f9f9;
        }
        .note ol
        {
            margin-bottom: 0;
        }
        .note ol li
        {
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
    <div class="container wrapper">
        <div class="content-form-section">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <div class="col-sm-12">
                        <div class="aboutcontent-sec">
                            <span class="pull-right text-danger">(*) Indicate Mandatory</span>
                            <h2 class=" margin-bottom15">
                                Application for Enrollment
                            </h2>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Film Details</div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Name of the film</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:TextBox CssClass="form-control" ID="txtFilmName" runat="server"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Language of the film</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="txtFilmLanguage" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Name of the production house(s) for the production of the film</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:TextBox ID="txtProductionHouse" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Estimated cost of the film (in INR lakh)</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:TextBox CssClass="form-control" ID="txtEstimateCost" runat="server"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Cost."
    ValidationExpression="^\d{0,8}(\.\d{1,4})?$" ControlToValidate="txtEstimateCost" ValidationGroup="a"
    ForeColor="Red" SetFocusOnError="true" ></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Tentative duration of the film (in min)</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:TextBox CssClass="form-control" ID="txtTentativeDuration" runat="server"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Is the title of the film registered with IMPA/ Its subsidary bodies ?</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="ddlImpa" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlImpa_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="reg" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Registration No. and Date
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="colon">:</span>
                                                <asp:TextBox CssClass="form-control" ID="txtRegdNo" runat="server" placeholder="Registration No."></asp:TextBox>
                                                <span class="mandetory">*</span>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="input-group date">
                                                    <asp:TextBox CssClass="form-control" ID="txtRegdDate" runat="server" autocomplete="off"></asp:TextBox><span
                                                        class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Office Details</div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Full address of registered office of the producer(s)
                                                </label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:TextBox CssClass="form-control" ID="txtProducerAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Telephone Number
                                                </label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:TextBox CssClass="form-control" ID="txtProducerNumber" runat="server"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Telephone Number."
    ValidationExpression="^([0-9]{10})$" ControlToValidate="txtProducerNumber" ValidationGroup="a"
    ForeColor="Red" SetFocusOnError="true" ></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Email Id
                                                </label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:TextBox CssClass="form-control" ID="txtProducerEmail" runat="server"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                            ControlToValidate="txtProducerEmail" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
                                            ErrorMessage = "Invalid Email Address" SetFocusOnError="true" ValidationGroup="a"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Producer's Details</div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Type of producer's firm
                                                </label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:DropDownList ID="ddlFirmType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFirmType_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Proprietary</asp:ListItem>
                                                    <asp:ListItem Value="2">Partnership</asp:ListItem>
                                                    <asp:ListItem Value="3">Company</asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="Proprietary" runat="server" visible="false">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Name of the Proprietor
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox CssClass="form-control" ID="txtProprietorName" runat="server"></asp:TextBox>
                                                    <span class="mandetory">*</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Address
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox CssClass="form-control" ID="txtProprietorAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <span class="mandetory">*</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Telephone Number
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox CssClass="form-control" ID="txtProprietorNumber" runat="server"></asp:TextBox>
                                                    <span class="mandetory">*</span>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Telephone Number."
    ValidationExpression="^([0-9]{10})$" ControlToValidate="txtProprietorNumber" ValidationGroup="a"
    ForeColor="Red" SetFocusOnError="true" ></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Upload an affidavit dully attested by a Notary or Magistrate
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <div class="input-group">
                                                        <asp:FileUpload ID="fluProprietorDoc" runat="server" CssClass="form-control" />
                                                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="input-group-addon bg-green"
                                                            ToolTip="Click Here to Upload File !!"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="input-group-addon bg-red"
                                                            Visible="true" ToolTip="Click Here to Delete File !!"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" Visible="true" CssClass="input-group-addon bg-blue"
                                                            ToolTip="Click Here to View File !!"><i class="fa fa-download"></i></asp:HyperLink>
                                                    </div>
                                                    <span class="mandetory">*</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="Partnership" runat="server" visible="false">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Names of the Managing Partner
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox CssClass="form-control" ID="txtManagerName" runat="server"></asp:TextBox>
                                                    <span class="mandetory">*</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Address
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox CssClass="form-control" ID="txtManagerAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <span class="mandetory">*</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Telephone Number
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox CssClass="form-control" ID="txtManagerNumber" runat="server"></asp:TextBox>
                                                    <span class="mandetory">*</span>
                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Telephone Number."
    ValidationExpression="^([0-9]{10})$" ControlToValidate="txtManagerNumber" ValidationGroup="a"
    ForeColor="Red" SetFocusOnError="true" ></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Upload the Partnership Deed or Memorandum and registration certificate from registrar
                                                        of firms
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <div class="input-group">
                                                        <asp:FileUpload ID="fluManagerDoc" runat="server" CssClass="form-control" />
                                                        <asp:LinkButton ID="LnkBtn_Upload_Unit_Type_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                            ToolTip="Click Here to Upload File !!"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LnkBtn_Delete_Unit_Type_Doc" runat="server" CssClass="input-group-addon bg-red"
                                                            Visible="true" ToolTip="Click Here to Delete File !!"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                        <asp:HyperLink ID="Hyp_View_Unit_Type_Doc" runat="server" Target="_blank" Visible="true"
                                                            CssClass="input-group-addon bg-blue" ToolTip="Click Here to View File !!"><i class="fa fa-download"></i></asp:HyperLink>
                                                    </div>
                                                    <span class="mandetory">*</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="Company" runat="server" visible="false">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Name of the Managing Director
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox CssClass="form-control" ID="txtDirectorName" runat="server"></asp:TextBox>
                                                    <span class="mandetory">*</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Address of Shareholders
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox CssClass="form-control" ID="txtShareholdersAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    <span class="mandetory">*</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Telephone Number
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:TextBox CssClass="form-control" ID="txtDirectorNumber" runat="server"></asp:TextBox>
                                                    <span class="mandetory">*</span>
                                                    <asp:RegularExpressionValidator ID="revMobNo" runat="server" ErrorMessage="Invalid Telephone Number."
    ValidationExpression="^([0-9]{10})$" ControlToValidate="txtDirectorNumber" ValidationGroup="a"
    ForeColor="Red" SetFocusOnError="true" ></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        Upload the Articles of Association and registration certificate from registrar of
                                                        companies
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <div class="input-group">
                                                        <asp:FileUpload ID="fluDirectorDoc" runat="server" CssClass="form-control" />
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="input-group-addon bg-green"
                                                            ToolTip="Click Here to Upload File !!"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="input-group-addon bg-red"
                                                            Visible="true" ToolTip="Click Here to Delete File !!"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Visible="true" CssClass="input-group-addon bg-blue"
                                                            ToolTip="Click Here to View File !!"><i class="fa fa-download"></i></asp:HyperLink>
                                                    </div>
                                                    <span class="mandetory">*</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>
                                                    Details of previous film produced by the producer concern
                                                </label>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="colon">:</span>
                                                <asp:TextBox CssClass="form-control" ID="txtFilmDetails" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <span class="mandetory">*</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12 text-right">
                                        <asp:Button ID="btnsave" CssClass="btn btn-success" runat="server" 
                                            Text="Submit" onclick="btnsave_Click" ValidationGroup="a" />
                                        <asp:Button ID="btndraft" CssClass="btn btn-warning" runat="server" 
                                            Text="Save As Draft" onclick="btndraft_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="note">
                                Note:
                                <ol>
                                    <li>All names should be given in full, not in initials.</li>
                                    <li>Full and complete information in regard to the particulars must be provided.</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddlImpa" />
                    <asp:PostBackTrigger ControlID="btnsave" />
                    <asp:PostBackTrigger ControlID="btndraft" />
                    <asp:PostBackTrigger ControlID="ddlFirmType" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
