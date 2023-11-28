<%@ Page Language="C#" AutoEventWireup="true" CodeFile="proposeddetails.aspx.cs"
    Inherits="proposeddetails" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/pealwebfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .input-group a.input-group-addon {
            background: #fff !important;
        }

        .form-body {
            padding: 10px 10px;
        }

        @media print {
            .noprint {
                display: none;
            }

            .col-sm-4 {
                width: 32.2%;
                float: left;
                padding: 0px;
                margin-right: 7px;
            }

            .upladdocs .col-sm-4 {
                width: 98%;
                float: left;
                padding: 0px;
                margin-right: 7px;
            }

            .table {
                width: 98%;
            }

            .pinsection {
                margin-left: 15px;
            }

            .form-control {
                height: 26px;
                padding: 0px 3px;
                font-size: 12px;
            }

            .input-group-addon {
                padding: 3px 12px;
            }

            .form-body .form-group {
                margin-bottom: 5px;
            }

            label {
                margin: 2px 0px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="registration-div">
                <div class="investrs-tab">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="iconsdiv tab-icondiv">
                                <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                    <i class="fa fa-print"></i></a><a href="javascript:history.back()" title="Back" id="A2"
                                        class="pull-right printbtn"><i class="fa fa-chevron-circle-left"></i></a>
                            </div>
                            <uc4:pealmenu ID="pealmenu" runat="server" />
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div class="wizard wizard2">
                            <div class="wizard-inner">
                                <div class="connecting-line">
                                </div>
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="backactive"><a href="PromoterDetails.aspx" aria-controls="Company Information"
                                        title="Company Information" data-toggle="tooltip"><span class="round-tab"><i class="glyphicon glyphicon-home"></i></span><small><i class="fa fa-check text-success backcheck" aria-hidden="true"></i>
                                            &nbsp;<b>1.</b> Company Information</small> </a></li>
                                    <li class="active"><a href="javascript:void(0)" aria-controls="Project Information"
                                        title="Project Information" data-toggle="tooltip"><span class="round-tab"><i class="glyphicon glyphicon-pencil"></i></span><small><i class="fa fa-check text-success backcheck2" aria-hidden="true"></i>&nbsp;<b>2.</b> Project Information</small> </a></li>
                                    <li role="presentation" class=""><a href="javascript:void(0)" aria-controls="Land and Utility Requirment"
                                        title=" Land and Utility Requirment" data-toggle="tooltip"><span class="round-tab"><i
                                            class="glyphicon glyphicon-picture"></i></span><small><b>3.</b> Land and Utility Requirement</small>
                                    </a></li>
                                    <li role="presentation" class="disabled"><a href="#complete" data-toggle="tab" aria-controls="Declaration"
                                        role="tab" title="Declaration" data-toggle="tooltip"><span class="round-tab"><i class="glyphicon glyphicon-ok"></i></span><small><b>4.</b> Declaration</a></small> </a></li>
                                </ul>
                            </div>
                            <div class="form-sec">
                                <h1 class="headerpeal">Project Evaluation including Allotment of Land</h1>
                                <%--    <asp:UpdatePanel ID="updPaneltot" runat="server">
                <ContentTemplate>--%>
                                <div class="form-header">
                                    <span class="pull-right"><span class="mandatoryspan ">(*) </span>Mark Fields Are Mandatory</span>
                                    <h2 class="m-t-0 m-b-0">6.Project Information</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Type">
                                                    Name of the unit<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtUnitName" MaxLength="100" CssClass="form-control" runat="server"
                                                    Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" TabIndex="1"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="It will accept only alphabets and spaces, no special characters are allowed">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Capacity">
                                                    EIN/IEM/IL/Udyog Aadhar/Udayam Aadhar<span class="text-red">*</span></label>
                                                <div class="clearfix">
                                                </div>
                                                <asp:DropDownList ID="drpEin" runat="server" CssClass="form-control" Style="width: 40%; float: left; margin-right: 5px"
                                                    TabIndex="2">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="EIN" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="IEM" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="IL" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Udyog Aadhaar" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Udayam Aadhar" Value="5"></asp:ListItem>
                                                    
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtregApp" TabIndex="3" Style="width: 58%; margin-left: 5px;" CssClass="form-control"
                                                    runat="server" Onkeypress="return inputLimiter(event,'IEMVAlid')"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Accepts only digits and characters with - and /">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Name">
                                                    Sector of activity<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    TabIndex="4" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Select from the dropdown list"><i
                                                    class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Type">
                                                    Sub sector<span class="text-red">*</span></label>
                                                <asp:DropDownList ID="ddlSubSector" runat="server" CssClass="form-control" TabIndex="5">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Select from the dropdown list"><i
                                                    class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Capacity">
                                                    Is the project coming under priority sector<span class="text-red">*</span></label>
                                                <asp:DropDownList CssClass="form-control" ID="ddlProjectcomingunder" runat="server" TabIndex="6">
                                                    <asp:ListItem Value="3">---Select---</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Please select the applicable type">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-md-4 col-sm-6" style="display: none;">
                                                <label for="Capacity">
                                                    Proposed annual capacity<span class="text-red">*</span></label>
                                                <div class="clearfix">
                                                </div>
                                                <div style="position: relative">
                                                    <asp:TextBox ID="txtProposedAnnCapacity" Text="0" MaxLength="12" CssClass="form-control"
                                                        TabIndex="7" Style="width: 58%; float: left;" runat="server" Onkeypress="return inputLimiter(event,'Decimal')"
                                                        onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);">
                                                    </asp:TextBox>
                                                    <a data-toggle="tooltip" class="fieldinfo" style="position: absolute; top: 34px; left: 53%;"
                                                        title="Only Numbers are accepted. No special characters will be allowed. Unit can be selected from the drop-down menu">
                                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                </div>
                                                <asp:DropDownList ID="ddlUnit" runat="server" TabIndex="8" CssClass="form-control"
                                                    Style="width: 40%; float: left; margin-left: 5px">
                                                    <asp:ListItem Text="--Select Unit--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-6" id="DvSpecify" runat="server" style="display: none;">
                                                <label for="ICapacity">
                                                    Others(Please specify)<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtOtherUnit" TabIndex="9" CssClass="form-control" MaxLength="50" runat="server"
                                                    Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="divProduct">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>Product name<span class="text-red">*</span>
                                                        </th>
                                                        <th>Proposed annual capacity<span class="text-red">*</span>
                                                        </th>
                                                        <th>Unit<span class="text-red">*</span>
                                                        </th>
                                                        <th width="100px"></th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtProductNamedet" TabIndex="10" MaxLength="50" CssClass="form-control" runat="server"
                                                                Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" ></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAnlCap" MaxLength="12" CssClass="form-control" TabIndex="11" runat="server"
                                                                Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                                onblur="isNumberBlur(event, this, 2);">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drpProductUnit" runat="server" TabIndex="12" CssClass="form-control"
                                                                Style="width: 40%; float: left; margin-left: 5px">
                                                                <asp:ListItem Text="--Select Unit--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtOtherProductUnit" TabIndex="13" placeholder="Others(Please specify)" Style="width: 40%; float: left; margin-left: 5px;"
                                                                CssClass="form-control" MaxLength="50" runat="server"
                                                                Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" TabIndex="14" Text="Add" ID="btnProduct" CssClass="btn btn-primary"
                                                                 OnClick="btnProduct_Click"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-12 bg-white">
                                                <asp:GridView ID="grdProduct" TabIndex="15" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                <asp:HiddenField ID="hdnidproduct" Value='<%#Eval("intProductid") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnunitid" Value='<%#Eval("intUnitId") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="vchProductName" HeaderText="Product name" />
                                                        <asp:BoundField DataField="vchProposedAnnualCapacity" HeaderText="Proposed annual capacity" />
                                                        <asp:BoundField DataField="vchProposedUnit" HeaderText="Unit" />
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDeleteproduct" OnClick="imgbtnDeleteproduct_Click" CssClass="btn btn-danger"
                                                                    runat="server" ImageUrl="~/images/rubbish.png" ToolTip="Click To Delete !" OnClientClick="return confirm('Are you sure you want to delete?');" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-sec">
                                <div class="form-header">
                                    <h2 class="m-t-0 m-b-0">7.Proposed Capital Investment (INR in Lakhs)</h2>
                                </div>
                                <div class="form-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Capacity">
                                                    Land including land development<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtlanddev" MaxLength="12" Onkeypress="return inputLimiter(event,'Decimal')"
                                                    onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                    onkeyup="sum();" CssClass="form-control" runat="server" TabIndex="16"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Capital investment in land (INR in Lakhs)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Capacity">
                                                    Building & civil construction<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtBuilding" MaxLength="12" CssClass="form-control" Onkeypress="return inputLimiter(event,'Decimal')"
                                                    onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                    onkeyup="sum();" runat="server" TabIndex="17"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Capital investment in Building & Civil Construction (INR in Lakhs)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Capacity">
                                                    Plant & machinery<span class="text-red">*</span></label>
                                                <asp:TextBox ID="txtPlantMachinery" MaxLength="12" Onkeypress="return inputLimiter(event,'Decimal')"
                                                    onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                    onkeyup="sum();" CssClass="form-control" runat="server" TabIndex="18"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Capital investment in Plant & Machinery (INR in Lakhs)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Capacity">
                                                    Others</label>
                                                <asp:TextBox ID="txtOthers" MaxLength="12" Onkeypress="return inputLimiter(event,'Decimal')"
                                                    onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                    onkeyup="sum();" CssClass="form-control" runat="server" TabIndex="19"></asp:TextBox>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Capital investment in Other activities (INR in Lakhs)">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-md-4 col-sm-6">
                                                <label for="Capacity">
                                                    Total capital investment</label>
                                                <asp:TextBox ReadOnly="true" ID="txtTotalCapitalInv" MaxLength="12" CssClass="form-control"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4 col-sm-7">
                                                <label for="Capacity">
                                                    Period to commence commercial production(in months)<span class="text-red">*</span></label>
                                                <asp:DropDownList CssClass="form-control" ID="ddlCommProdInMonth" runat="server" TabIndex="20">
                                                    <%-- <asp:ListItem Value="0">---Select---</asp:ListItem>
                                            <asp:ListItem Value="1">6</asp:ListItem>
                                            <asp:ListItem Value="2">7</asp:ListItem>
                                            <asp:ListItem Value="3">8</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Period to commence commercial production (in months) from the date of application">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                            <div class="col-md-4 col-sm-5">
                                                <label for="Capacity">
                                                    Pollution category<span class="text-red">*</span></label>
                                                <asp:DropDownList CssClass="form-control" ID="ddlPolutionCategory" runat="server" TabIndex="21">
                                                    <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                    <asp:ListItem Value="1">White</asp:ListItem>
                                                    <asp:ListItem Value="2">Green </asp:ListItem>
                                                    <asp:ListItem Value="3">Orange</asp:ListItem>
                                                    <asp:ListItem Value="4">Red</asp:ListItem>
                                                </asp:DropDownList>
                                                <a data-toggle="tooltip" class="fieldinfo" title="Select one option: Green/Orange/Red/White">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-sec">
                            <div class="form-header">
                                <h2 class="m-t-0 m-b-0">8.Means of Finance for Capital Investment (INR in Lakh)</h2>
                            </div>
                            <div class="form-body">
                                <div class="">
                                    <div class="row form-group">
                                        <div class="col-md-4 col-sm-6">
                                            <label for="Capacity">
                                                Equity contribution <span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtEquity" MaxLength="15" CssClass="form-control" runat="server"
                                                Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                onblur="isNumberBlur(event, this, 2);" onkeyup="sumFixed();" TabIndex="22"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Equity contribution (in INR in Lakh)">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <label for="Capacity">
                                                Bank/institutional finance</label>
                                            <asp:TextBox ID="txtBankFinance" MaxLength="16" Onkeypress="return inputLimiter(event,'Decimal')"
                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                onkeyup="sumFixed();" CssClass="form-control" runat="server" TabIndex="23"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Bank/Institutional Finance Contribution (in INR in Lakh)">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <label for="Capacity">
                                                Total</label>
                                            <asp:TextBox ID="txtTotal" MaxLength="17" ReadOnly="true" CssClass="form-control"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-md-4 col-sm-6">
                                            <label for="Capacity">
                                                Foreign direct investment (if any)</label>
                                            <asp:TextBox ID="txtFDI" CssClass="form-control" MaxLength="12" runat="server" Onkeypress="return inputLimiter(event,'Decimal')"
                                                onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                onkeyup="sumFixed();" TabIndex="24"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="FDI Contribution (in INR in Lakh)">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <div class="col-md-4 col-sm-6">
                                                    <label for="Capacity">
                                                        In case of FDI, please upload relevant document <span id="dvIncase" class="text-red">*</span></label>
                                                    <div class="input-group">
                                                        <asp:FileUpload ID="FileOtherFin" CssClass="form-control" runat="server" TabIndex="25" />
                                                        <asp:HiddenField ID="hdnOtherFin" runat="server" />
                                                        <%--<asp:Button ID="btnUpload4" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />
                                                        --%>
                                                        <asp:LinkButton ID="lnkOtherFin" runat="server" CssClass="input-group-addon bg-green"
                                                            OnClick="lnkOtherFin_Click">
                                                   <i class="fa fa-upload" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelOtherFin" CssClass="input-group-addon bg-red" runat="server"
                                                            OnClick="lnkDelOtherFin_Click">
                                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:HyperLink ID="hlDoc5" CssClass="input-group-addon bg-blue" Visible="false" runat="server"
                                                            Target="_blank">
                                                        <%--<asp:Image ID="pdficon5" runat="server" ImageUrl="../images/pdf1.png" Height="14"
                                                            Width="14" Visible="false" />--%>
                                                        <i class="fa  fa-download"></i>
                                                        </asp:HyperLink>
                                                    </div>
                                                    <asp:Label ID="lblOtherFin" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                        runat="server" Text="Other source of finance uploaded successfully."></asp:Label>
                                                    <asp:HiddenField ID="hdn1" runat="server" />
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkOtherFin" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div id="dvIRR">
                                        <div class="margin-bottom15">
                                            <h4 class="subhdr">For IRR & DSCR<span class="text-red" id="spIRR">*</span></h4>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        IRR</label>
                                                    <asp:TextBox ID="txtIRR" TabIndex="26" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Decimal')"
                                                        MaxLength="10" onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                    <a data-toggle="tooltip" class="fieldinfo" title="Internal Rate of Return of the project in percentage (Only if the proposed project category is MSME)">
                                                        <i class="fa fa-question-circle" aria-hidden="true" ></i></a>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label for="Capacity">
                                                        DSCR</label>
                                                    <asp:TextBox ID="txtDSCR" Onkeypress="return inputLimiter(event,'Decimal')" MaxLength="10"
                                                        onkeydown="return isNumberDecimalKey(event, this, 2);" onblur="isNumberBlur(event, this, 2);"
                                                        CssClass="form-control" runat="server" TabIndex="27"></asp:TextBox>
                                                    <a data-toggle="tooltip" class="fieldinfo" title="Debt Service Coverage Ratio of the project  in percentage (Only if the proposed project category is MSME)">
                                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divGroupOfCompany" runat="server">
                                        <div class="form-group" id="div2">
                                            <div class="margin-bottom15">
                                                <h4 class="subhdr">Group of Company Details
                                                </h4>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 margin-bottom10">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th width="4%">SlNo
                                                            </th>
                                                            <th>Company Name
                                                            </th>
                                                            <th width="28%">Net worth of last financial year (INR in Lakh)
                                                            </th>
                                                            <th width="30%">Document related to net worth
                                                            </th>
                                                            <th width="5%">Add
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>--&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_GC_Company_Name" MaxLength="50" CssClass="form-control" runat="server"
                                                                    Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" TabIndex="28"
                                                                    placeholder="Company Name"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_GC_Net_Worth" MaxLength="12" CssClass="form-control" TabIndex="29"
                                                                    runat="server" Onkeypress="return inputLimiter(event,'Decimal')" onkeydown="return isNumberDecimalKey(event, this, 2);"
                                                                    onblur="isNumberBlur(event, this, 2);" placeholder="0.00">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="input-group">
                                                                            <asp:FileUpload ID="FU_GC_New_Worth" runat="server" CssClass="form-control" ToolTip="Browse File to Upload !!"
                                                                                TabIndex="30" />
                                                                            <asp:HiddenField ID="Hid_GC_New_Worth_File_Name" runat="server" />
                                                                            <asp:LinkButton ID="LnkBtn_Upload_GC_New_Worth_Doc" runat="server" CssClass="input-group-addon bg-green"
                                                                                OnClick="LnkBtn_Upload_GC_New_Worth_Doc_Click" ToolTip="Click Here to Upload File !!"><i class="fa fa-upload" aria-hidden="true"></i>
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="LnkBtn_Delete_GC_New_Worth_Doc" CssClass="input-group-addon bg-red"
                                                                                runat="server" OnClick="LnkBtn_Delete_GC_New_Worth_Doc_Click" Visible="false"
                                                                                ToolTip="Click Here to Delete File !!"><i class="fa fa-trash-o" aria-hidden="true"></i>
                                                                            </asp:LinkButton>
                                                                            <asp:HyperLink ID="Hyp_View_GC_New_Worth_Doc" CssClass="input-group-addon bg-blue"
                                                                                Visible="false" runat="server" Target="_blank"><i class="fa  fa-download"></i>
                                                                            </asp:HyperLink>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="LnkBtn_Upload_GC_New_Worth_Doc" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                <small class="text-danger">(.pdf/.jpg/.jpeg/.png/.zip file only and Max file size 12
                                                                MB)</small>
                                                                <asp:Label ID="Lbl_Msg_GC_Net_Worth_Doc" Style="font-size: 12px;" CssClass="text-blue"
                                                                    Visible="false" runat="server" Text="Document Uploaded Successfully"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Button runat="server" Text="Add" ID="Btn_Add_GC_Net_Worth" CssClass="btn btn-primary"
                                                                    TabIndex="31" OnClick="Btn_Add_GC_Net_Worth_Click"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:GridView ID="Grd_GC_Net_Worth" TabIndex="32" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                                        OnRowDataBound="Grd_GC_Net_Worth_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SlNo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_GC_SlNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Company Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_GC_Company_Name_G" runat="server" Text='<%# Eval("vchCompanyName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Net worth of last financial year (INR in Lakh)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Lbl_GC_Net_Worth_G" runat="server" Text='<%# Eval("decNetWorth") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="28%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Document related to net worth">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="Hyp_View_GC_Doc" runat="server" Target="_blank" ToolTip="Click here to view document."><i class="fa fa-download"></i></asp:HyperLink>
                                                                    <asp:HiddenField ID="Hid_GC_Net_Worth_File_Name_G" runat="server" Value='<%# Eval("vchNetWorthDoc") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgBtn_Delete_GC_Net_Worth" OnClick="ImgBtn_Delete_GC_Net_Worth_Click"
                                                                        CssClass="btn btn-danger" runat="server" ImageUrl="~/images/rubbish.png" CommandArgument='<%# Container.DataItemIndex %>'
                                                                        ToolTip="Click To Delete !" OnClientClick="return confirm('Are you sure you want to delete?');" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-sec pinsection">
                            <div class="form-header">
                                <h2 class="m-t-0 m-b-0">9.Project Implementation Schedule
                                </h2>
                            </div>
                            <div class="form-body">
                                <table class="table table-bordered">
                                    <tr>
                                        <th>Activities
                                        </th>
                                        <th>Months (Zero date starts from acquisition /allotment of land)
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="Capacity">
                                                Ground breaking</label><span class="text-red">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGroundBreaking" MaxLength="2" CssClass="form-control" runat="server"
                                                Onkeypress="return inputLimiter(event,'Numbers')" TabIndex="33"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="No. of months to start of ground breaking (Zero date starts from acquisition/allotment of land)">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="Capacity">
                                                Civil and structural completion</label><span class="text-red">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCivil" MaxLength="2" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'Numbers')"
                                                TabIndex="34"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="No. of months to completion of civil and structural completion (Zero date starts from acquisition/allotment of land)">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="Capacity">
                                                Major equipment erection</label><span class="text-red">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEquipment" MaxLength="2" CssClass="form-control" runat="server"
                                                Onkeypress="return inputLimiter(event,'Numbers')" TabIndex="35"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="No. of months to completion of major equipment erection (Zero date starts from acquisition/allotment of land)">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="Capacity">
                                                Start of commercial production</label><span class="text-red">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCommissioning" MaxLength="2" CssClass="form-control" runat="server"
                                                Enabled="false" Onkeypress="return inputLimiter(event,'Numbers')" TabIndex="36"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="form-group">
                                    <div class="row">
                                        <asp:UpdatePanel ID="updPanel" runat="server">
                                            <ContentTemplate>
                                                <div class="col-md-4 col-sm-6">
                                                    <label for="Capacity" id="lblEIM">
                                                        EIN/IEM/IL/Udyog Aadhar/Udayam Aadhar/Prod Cert No
                                                    </label>
                                                    <span id="IEM123" class="text-red">*</span>
                                                    <div class="input-group">
                                                        <asp:FileUpload ID="FileIndustryEntMemorandum" CssClass="form-control" runat="server"
                                                            TabIndex="37" />
                                                        <asp:HiddenField ID="hdnIndustryEntMemorandum" runat="server" />
                                                        <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                        <asp:LinkButton ID="lnkIndustryEntMemorandum" CssClass="input-group-addon bg-green"
                                                            runat="server" OnClick="lnkIndustryEntMemorandum_Click">
                                                   <i class="fa fa-upload" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelIndustryEntMemorandum" CssClass="input-group-addon bg-red"
                                                            runat="server" OnClick="lnkDelIndustryEntMemorandum_Click">
                                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:HyperLink ID="hlDoc1" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                        <%--<asp:Image ID="pdficon1" runat="server" ImageUrl="../images/pdf1.png" Height="14"
                                                            Width="14" Visible="false" />--%>
                                                        <i class="fa  fa-download"></i>
                                                        </asp:HyperLink>
                                                    </div>
                                                    <asp:Label ID="lblIndMemo" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                        runat="server" Text="EIN/IEM/IL/Udyog Aadhar/Udayam Aadhar uploaded successfully."></asp:Label>
                                                    <asp:HiddenField ID="hdn2" runat="server" />
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkIndustryEntMemorandum" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="col-md-4 col-sm-6" style="display: none;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <label for="Capacity">
                                                        Manufacturing process flow<span class="text-red"></span></label>
                                                    <div class="input-group">
                                                        <asp:FileUpload ID="FileMnfprocess" TabIndex="38" CssClass="form-control" runat="server" />
                                                        <asp:HiddenField ID="hdnFileMnfprocess" runat="server" />
                                                        <asp:LinkButton ID="lnkMnfprocess" CssClass="input-group-addon bg-green" runat="server">
                                                   <i class="fa fa-upload" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelMnfprocess" CssClass="input-group-addon bg-red" runat="server">
                                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:HyperLink ID="hlDoc2" CssClass="input-group-addon bg-blue" Visible="false" runat="server"
                                                            Target="_blank">
                                                    <i class="fa  fa-download"></i>
                                                        </asp:HyperLink>
                                                    </div>
                                                    <asp:Label ID="lblManufact" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                        runat="server" Text="Manufacturing uploaded successfully."></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkMnfprocess" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="col-md-4 col-sm-6">
                                                    <label for="Capacity">
                                                        Feasibility report<span class="text-red">*</span></label>
                                                    <div class="input-group">
                                                        <asp:FileUpload ID="FileFeasibilityReport" CssClass="form-control" runat="server"
                                                            TabIndex="39" />
                                                        <asp:HiddenField ID="hdnFeasibilityReport" runat="server" />
                                                        <%--<asp:Button ID="btnUpload2" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                        <asp:LinkButton ID="lnkFeasibilityReport" CssClass="input-group-addon bg-green" runat="server"
                                                            OnClick="lnkFeasibilityReport_Click">
                                                   <i class="fa fa-upload" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelFeasibilityReport" CssClass="input-group-addon bg-red"
                                                            runat="server" OnClick="lnkDelFeasibilityReport_Click">
                                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:HyperLink ID="hlDoc3" runat="server" CssClass="input-group-addon bg-blue" Visible="false"
                                                            Target="_blank">
                                                    <%--<asp:Image ID="pdficon3" runat="server" ImageUrl="../images/pdf1.png" Height="14"
                                                        Width="14" Visible="false" />--%>
                                                    <i class="fa  fa-download"></i>
                                                        </asp:HyperLink>
                                                    </div>
                                                    <asp:Label ID="lblFeasibility" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                        runat="server" Text="Feasibility report uploaded successfully."></asp:Label>
                                                    <asp:HiddenField ID="hdn3" runat="server" />
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkFeasibilityReport" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <div class="col-md-4 col-sm-6" id="DvX">
                                                    <label for="Capacity">
                                                        Board resolution to take up the project<span class="text-red" id="spnBoard">*</span></label>
                                                    <div class="input-group">
                                                        <asp:FileUpload ID="FileBoardResolution" CssClass="form-control" runat="server" TabIndex="40" />
                                                        <asp:HiddenField ID="hdnBoardResolution" runat="server" />
                                                        <%-- <asp:Button ID="btnUpload3" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                        <asp:LinkButton ID="lnkBoardResolution" CssClass="input-group-addon bg-green" runat="server"
                                                            OnClick="lnkBoardResolution_Click">
                                                   <i class="fa fa-upload" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelBoardResolution" CssClass="input-group-addon bg-red" runat="server"
                                                            OnClick="lnkDelBoardResolution_Click">
                                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                        <asp:HyperLink ID="hlDoc4" CssClass="input-group-addon bg-blue" runat="server" Visible="false"
                                                            Target="_blank">
                                                   <%-- <asp:Image ID="pdficon4" runat="server" ImageUrl="../images/pdf1.png" Height="14"
                                                        Width="14" Visible="false" />--%>
                                                    <i class="fa  fa-download"></i>
                                                        </asp:HyperLink>
                                                    </div>
                                                    <asp:Label ID="lblBoardResolution" Style="font-size: 12px;" CssClass="text-blue"
                                                        Visible="false" runat="server" Text="Board resolution uploaded successfully."></asp:Label>
                                                    <asp:HiddenField ID="hdn4" runat="server" />
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkBoardResolution" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-sec">
                            <div class="form-header">
                                <h2 class="m-t-0 m-b-0">10.Employment Potential</h2>
                            </div>
                            <div class="form-body">
                                <div class="form-group">
                                    <div class="row pinsection">
                                        <div class="col-sm-12">
                                            <table class="table table-bordered">
                                                <tr>
                                                    <th></th>
                                                    <th>Existing
                                                    </th>
                                                    <th>Proposed
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>Managerial<span class="text-red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtManagerExist" CssClass="form-control" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onkeyup="sumExist();" MaxLength="4" runat="server" TabIndex="41"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="No. of people to be employed in the project for existing">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtManagerProposed" MaxLength="4" CssClass="form-control" runat="server"
                                                            TabIndex="42" Onkeypress="return inputLimiter(event,'Numbers')" onkeyup="sumProposed();"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="No. of people to be employed in the project for proposed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Supervisory<span class="text-red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSupervisorExist" MaxLength="4" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onkeyup="sumExist();" CssClass="form-control" runat="server" TabIndex="43"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="No. of people to be employed in the project for existing">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSupervisorProposed" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onkeyup="sumProposed();" MaxLength="4" CssClass="form-control" runat="server"
                                                            TabIndex="44"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="No. of people to be employed in the project for proposed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Skilled<span class="text-red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSkilledExist" MaxLength="4" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onkeyup="sumExist();" CssClass="form-control" runat="server" TabIndex="45"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="No. of people to be employed in the project for existing">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSkilledProposed" MaxLength="4" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onkeyup="sumProposed();" CssClass="form-control" runat="server" TabIndex="46"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="No. of people to be employed in the project for proposed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Semi skilled<span class="text-red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSemiskilledExist" MaxLength="4" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onkeyup="sumExist();" CssClass="form-control" runat="server" TabIndex="47"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="No. of people to be employed in the project for existing">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSemiskilledProposed" MaxLength="4" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onkeyup="sumProposed();" CssClass="form-control" runat="server" TabIndex="48"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="No. of people to be employed in the project for proposed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Un skilled<span class="text-red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUnskilledExist" MaxLength="4" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onkeyup="sumExist();" CssClass="form-control" runat="server" TabIndex="49"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="No. of people to be employed in the project for existing">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUnskilledProposed" MaxLength="4" Onkeypress="return inputLimiter(event,'Numbers')"
                                                            onkeyup="sumProposed();" CssClass="form-control" runat="server" TabIndex="50"></asp:TextBox>
                                                        <a data-toggle="tooltip" class="fieldinfo" title="No. of people to be employed in the project for proposed">
                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total employment
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalExist" MaxLength="10" CssClass="form-control" runat="server"
                                                            ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalProposed" MaxLength="10" CssClass="form-control" runat="server"
                                                            ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <label for="Capacity">
                                                Proposed direct employment (On company payroll)<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtDirectEmp" MaxLength="7" CssClass="form-control" runat="server"
                                                TabIndex="51" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Applicant will enter the no. of direct employees (on Company payroll), out of the total no. of employees proposed to be employed in the project">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                        <div class="col-md-4 col-sm-6">
                                            <label for="Capacity">
                                                Proposed contractual employment<span class="text-red">*</span></label>
                                            <asp:TextBox ID="txtContractualEmp" MaxLength="7" CssClass="form-control" runat="server"
                                                TabIndex="52" Onkeypress="return inputLimiter(event,'Numbers')"></asp:TextBox>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Applicant will enter the no. of contractual employees, out of the total no. of employees proposed to be employed in the project">
                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-sec">
                            <div class="form-header">
                                <h2 class="m-t-0 m-b-0">11.Projects at other Locations</h2>
                            </div>
                            <div class="form-body">
                                <div class="form-group">
                                    <div class="row">
                                        <label for="Name" class="col-md-4 col-sm-6">
                                            Does the company have projects at other locations in India?<span class="text-red">*</span></label>
                                        <div class="col-md-4 col-sm-6">
                                            <asp:DropDownList ID="ddlprojloc" runat="server" CssClass="form-control" TabIndex="53">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Select Yes/No"><i class="fa fa-question-circle"
                                                aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                    <div class="form-group" id="Locdetails1">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="subhdr">Location Details</h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="DvDet1">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>Unit name
                                                        </th>
                                                        <th>Product
                                                        </th>
                                                        <th>Total capacity(With unit)
                                                        </th>
                                                        <th>State
                                                        </th>
                                                        <th>District
                                                        </th>
                                                        <th width="100px"></th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtUnit" MaxLength="50" CssClass="form-control" runat="server" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                TabIndex="54"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Provide Unit Name( Please don't add the prefix M/s Name)">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProduct" MaxLength="50" CssClass="form-control" runat="server"
                                                                TabIndex="55" Onkeypress="return inputLimiter(event,'NameCharacterscomma')"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Name of Final Product at top 5 units (as per turnover)">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCapacity" CssClass="form-control" runat="server" MaxLength="30"
                                                                TabIndex="56" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Specify the capacity with unit"><i
                                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                TabIndex="57" AutoPostBack="true">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddldist" runat="server" CssClass="form-control" TabIndex="58">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" Text="Add" ID="AddMore" CssClass="btn btn-primary" OnClick="AddMore_Click"
                                                                TabIndex="59"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <%--   <div class="row">--%>
                                            <div class="col-sm-12 bg-white">
                                                <asp:GridView ID="gvLOCDetails" TabIndex="60" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                <asp:HiddenField ID="hdnid" Value='<%#Eval("intProjectId") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnstateid" Value='<%#Eval("intStateId") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnDistid" Value='<%#Eval("intDistId") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="vchUnitName" HeaderText="Unit name" />
                                                        <asp:BoundField DataField="vchProduct" HeaderText="Product" />
                                                        <asp:BoundField DataField="vchTotCapacity" HeaderText="	Total capacity" />
                                                        <asp:BoundField DataField="vchStateName" HeaderText="State" />
                                                        <asp:BoundField DataField="vchDistName" HeaderText="District" />
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDelete" CssClass="btn btn-danger" runat="server" ImageUrl="~/images/rubbish.png"
                                                                    ToolTip="Click To Delete !" OnClick="imgbtnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?');" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <%-- </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-group row">
                                        <label for="Name" class="col-md-4 col-sm-6">
                                            Is there any unit outside India<span class="text-red">*</span></label>
                                        <div class="col-md-4 col-sm-6">
                                            <asp:DropDownList ID="ddlprojectUnits" runat="server" CssClass="form-control" TabIndex="61">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                            <a data-toggle="tooltip" class="fieldinfo" title="Select Yes/No"><i class="fa fa-question-circle"
                                                aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                    <div class="form-group row" id="Div1">
                                        <div class="col-sm-12">
                                            <h4 class="subhdr">Location Details(Top five as per turnover)</h4>
                                        </div>
                                    </div>
                                    <div class="form-group" id="DivOtherUnits">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table class="table table-bordered m-b-0">
                                                    <tr>
                                                        <th>Unit name
                                                        </th>
                                                        <th>Product
                                                        </th>
                                                        <th>Total capacity(With unit)
                                                        </th>
                                                        <th>Country
                                                        </th>
                                                        <th>City
                                                        </th>
                                                        <th width="100px"></th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtOtherUnitname" MaxLength="50" CssClass="form-control" runat="server"
                                                                TabIndex="62" Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Provide Unit Name( Please don't add the prefix M/s Name)">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOtherProd" MaxLength="50" CssClass="form-control" runat="server"
                                                                TabIndex="63" Onkeypress="return inputLimiter(event,'NameCharacterscomma')"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Name of Final Product at top 5 units (as per turnover)">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOthercapacity" CssClass="form-control" runat="server" MaxLength="30"
                                                                TabIndex="64" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Specify the capacity with unit"><i
                                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" TabIndex="65">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="India" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Specify the country where unit is located">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCityname" MaxLength="50" CssClass="form-control" runat="server"
                                                                TabIndex="66" Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Specify the city name, only characters and space allowed">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" Text="Add" ID="btnUnitAdd" CssClass="btn btn-primary"
                                                                TabIndex="67" OnClick="btnUnitAdd_Click"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-12 bg-white">
                                                <asp:GridView ID="gvOtherUnits" TabIndex="68" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                <asp:HiddenField ID="hdnid" Value='<%#Eval("intUnitId") %>' runat="server" />
                                                                <asp:HiddenField ID="hdncountryid" Value='<%#Eval("intCountryId") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="vchUnitName" HeaderText="Unit name" />
                                                        <asp:BoundField DataField="vchProduct" HeaderText="Product" />
                                                        <asp:BoundField DataField="vchTotCapacity" HeaderText="	Total capacity" />
                                                        <asp:BoundField DataField="vchCountryName" HeaderText="Country" />
                                                        <asp:BoundField DataField="vchCityName" HeaderText="City" />
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDelete" CssClass="btn btn-danger" runat="server" ImageUrl="~/images/rubbish.png"
                                                                    ToolTip="Click To Delete !" OnClick="imgbtnUnitDelete_Click" OnClientClick="return confirm('Are you sure you want to delete?');" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- </ContentTemplate>
                      <Triggers>
                       <asp:PostBackTrigger ControlID="btnUpload" />
                      </Triggers>
                    </asp:UpdatePanel>--%>
                        </div>
                        <div class="form-footer">
                            <div class="row">
                                <div class="col-sm-12" align="center">
                                    <asp:Button ID="btnBack" TabIndex="69" runat="server" Text="Back" CssClass=" btn btn-warning" OnClick="btnBack_Click" />
                                    <asp:Button ID="btnNext" TabIndex="70" runat="server" Text="Next" CssClass=" btn btn-success" OnClick="btnNext_Click" />
                                    <asp:Button ID="btnSaveAsdraft" runat="server" Text="Save As Draft" CssClass=" btn btn-primary draftbtn noprint"
                                        OnClick="btnSaveAsdraft_Click" />
                                    <asp:Button ID="btnReset" TabIndex="71" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                    <asp:HiddenField ID="hdnAllFileValue" runat="server" />
                                </div>
                            </div>
                        </div>
                        <%--Modal Section--%>
                        <asp:HiddenField ID="Hid_Pop" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                            TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="Btn_No">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: block;">
                            <div id="undertakingipr2015">
                                <div class="modal-dialog">
                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header bg-purpul">
                                            <h4 class="modal-title">
                                                <strong>Alert</strong></h4>
                                        </div>
                                        <div class="modal-body">
                                            <p>
                                                The equity should be more than or equal to 30% of the Total Capital Investment of
                                            the project proposal.
                                            </p>
                                        </div>
                                        <div class="modal-footer">
                                            <div class="row">
                                                <div class="col-sm-6 text-left">
                                                    <span style="color: #FD0F00;">Do you want to proceed? </span>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Button ID="Btn_Yes" runat="server" Text="Yes" OnClick="Btn_Yes_Click" class="btn btn-success"
                                                        ToolTip="Click Here to Confirm" />
                                                    <asp:Button ID="Btn_No" runat="server" Text="No" class="btn btn-danger" ToolTip="Click Here to Back" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <%--Modal Section--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    <uc3:footer ID="footer" runat="server" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#printbtn").click(function () {
                window.print();
            });
            $('.menuproposal').addClass('active');

            $('.backcheck,.backcheck2,.backcheck3,.backcheck4,').hide();

            var sessioncal = '<%=Session["proposalno"] %>';
            var Qrvalue = '<%= Request.QueryString["StrPropNo"] %>';

            if ((sessioncal != null) && (sessioncal != "")) {
                $('.wizard2 .nav-tabs > li').addClass("backactive");
            }
            else if ((Qrvalue != null) && (Qrvalue != "")) {
                $('.wizard2 .nav-tabs > li').addClass("backactive");
            }
            else {
                $('.wizard2 .nav-tabs > li').removeClass("backactive");
            }
        });

        /*-----------------------------------------------------------------------------------------------*/

        if ('<%=Session["AllFileValue"] %>' != "") {
            $('#hdnAllFileValue').val('<%=Session["AllFileValue"] %>');
        }
        var allFileVal = $("#hdnAllFileValue").val();

        /*-----------------------------------------------------------------------------------------------*/
        function pageLoad(sender, args) {

            if (('<%=Session["Constitution"] %>' == "1") || ('<%=Session["Constitution"] %>' == "2")) {
                $('#spnBoard').hide();
            }
            else {
                $('#spnBoard').show();
            }

            /*-----------------------------------------------------------------------------------------------*/

            $('#dvIncase').hide();

            /*-----------------------------------------------------------------------------------------------*/

            $("#txtFDI").change(function () {
                var Equity = document.getElementById('txtEquity').value;
                var FDI = document.getElementById('txtFDI').value;

                if (parseFloat(FDI) > parseFloat(Equity)) {
                    jAlert('<strong>Foreign direct investment should be equal or smaller than Equity contribution</strong>');
                    document.getElementById('txtFDI').value = "";
                    return false;
                }

                if (parseFloat(FDI) > 0) {
                    $('#dvIncase').show();
                }
                else {
                    $('#dvIncase').hide();
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $("#FileOtherFin").change(function (e) {
                var filename = $("#FileOtherFin").val().replace(/C:\\fakepath\\/i, '');
                var fileExtension = ['jpeg', 'jpg', 'png', 'pdf'];
                var yourValues = $("#hdnAllFileValue").val();
                var array = yourValues.split(",");
                var isValue = 0;
                for (i in array) {
                    if (array[i] == filename) {
                        isValue = 1;
                    }
                }
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    jAlert('<strong>Please Upload  PDF,PNG,JPG,JPEG file Only!', projname);
                    $("#FileOtherFin").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileOtherFin").val('');
                        return false;
                    }
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $("#FileIndustryEntMemorandum").change(function (e) {
                var filename = $("#FileIndustryEntMemorandum").val().replace(/C:\\fakepath\\/i, '');
                var fileExtension = ['jpeg', 'jpg', 'png', 'pdf'];
                var yourValues = $("#hdnAllFileValue").val();
                var array = yourValues.split(",");
                var isValue = 0;
                for (i in array) {
                    if (array[i] == filename) {
                        isValue = 1;
                    }
                }
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    jAlert('<strong>Please Upload  PDF,PNG,JPG,JPEG file Only!', projname);
                    $("#FileIndustryEntMemorandum").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileIndustryEntMemorandum").val('');
                        return false;
                    }
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $("#FileFeasibilityReport").change(function (e) {
                var filename = $("#FileFeasibilityReport").val().replace(/C:\\fakepath\\/i, '');
                var fileExtension = ['jpeg', 'jpg', 'png', 'pdf'];
                var yourValues = $("#hdnAllFileValue").val();
                var array = yourValues.split(",");
                var isValue = 0;
                for (i in array) {
                    if (array[i] == filename) {
                        isValue = 1;
                    }
                }
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    jAlert('<strong>Please Upload  PDF,PNG,JPG,JPEG file Only!', projname);
                    $("#FileFeasibilityReport").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileFeasibilityReport").val('');
                        return false;
                    }
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $("#FileBoardResolution").change(function (e) {
                var filename = $("#FileBoardResolution").val().replace(/C:\\fakepath\\/i, '');
                var fileExtension = ['jpeg', 'jpg', 'png', 'pdf'];
                var yourValues = $("#hdnAllFileValue").val();
                var array = yourValues.split(",");
                var isValue = 0;
                for (i in array) {
                    if (array[i] == filename) {
                        isValue = 1;
                    }
                }
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    jAlert('<strong>Please Upload  PDF,PNG,JPG,JPEG file Only!', projname);
                    $("#FileBoardResolution").val('');
                    return false;
                }
                else {
                    if (isValue == 1) {
                        jAlert('<strong>File already exist.</strong>', projname);
                        $("#FileBoardResolution").val('');
                        return false;
                    }
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            var km = $('#drpEin').find("option:selected").text();
            if (km == "--Select--") {
                $('#lblEIM').text("EIN/IEM/IL/Udyog Aadhar/Udayam Aadhar");
                $('#IEM123').hide();
            }
            else {
                $('#lblEIM').text(km);
                $('#IEM123').show();
            }

            var l = $('#drpEin').val();
            if (l == "2") {
                $("#txtregApp").attr('maxlength', '50');
            }
            else {
                $("#txtregApp").attr('maxlength', '50');
            }

            /*-----------------------------------------------------------------------------------------------*/

            $('#drpEin').change(function () {
                var k = $(this).val();
                var km = $(this).find("option:selected").text();

                if (km == "--Select--") {
                    $('#lblEIM').text("EIN/IEM/IL/Udyog Aadhar/Udayam Aadhar");
                    $('#IEM123').hide();
                }
                else {
                    $('#lblEIM').text(km);
                    $('#IEM123').show();
                }

                if (k == "2") {
                    $("#txtregApp").attr('maxlength', '50');
                }
                else {
                    $("#txtregApp").attr('maxlength', '50');
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $('#spIRR').hide();

            var ProjCat = '<%=Session["ProjectCategory"] %>';
            if (ProjCat == "2") {
                $("#dvIRR").show();
                $('#spIRR').show();
            }
            else {
                $("#dvIRR").hide();
                $('#spIRR').hide();
            }

            if ('<%=Session["txtIName"] %>' != "") {
                $('#txtUnitName').val('<%=Session["txtIName"] %>');
            }

            /*-----------------------------------------------------------------------------------------------*/

            $("#txtGroundBreaking").change(function () {
                var g = $(this).val();
                var civil = $('#txtCivil').val();
                if (civil == "") {
                    civil = 0;
                }
                var major = $('#txtEquipment').val();
                if (major == "") {
                    major = 0;
                }
                var cmpd = $('#txtCommissioning').val();
                if (cmpd == "") {
                    cmpd = 0;
                }
                if (g != "") {
                    if (parseInt(civil) != "0") {
                        if (parseInt(g) > parseInt(civil)) {
                            jAlert('<strong>Ground breaking should not be greater than civil and structural completion.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                    if (parseInt(major) != "0") {
                        if (parseInt(g) > parseInt(major)) {
                            jAlert('<strong>Ground breaking should not be greater than major equipment erection.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                    if (parseInt(cmpd) != "0") {
                        if (parseInt(g) > parseInt(cmpd)) {
                            jAlert('<strong>Ground breaking should not be greater than start of commercial production.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $("#txtCivil").change(function () {

                var g = $(this).val();
                var civil = $('#txtGroundBreaking').val();
                if (civil == "") {
                    civil = 0;
                }
                var major = $('#txtEquipment').val();
                if (major == "") {
                    major = 0;
                }
                var cmpd = $('#txtCommissioning').val();
                if (cmpd == "") {
                    cmpd = 0;
                }
                if (g != "") {
                    if (parseInt(civil) != "0") {
                        if (parseInt(g) < parseInt(civil)) {
                            jAlert('<strong>Civil and structural completion should not be less than Ground breaking.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                    if (parseInt(major) != "0") {
                        if (parseInt(g) > parseInt(major)) {
                            jAlert('<strong>Civil and structural completion should not be greater than major equipment erection.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                    if (parseInt(cmpd) != "0") {
                        if (parseInt(g) > parseInt(cmpd)) {
                            jAlert('<strong>Civil and structural completion should not be greater than start of commercial production.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                }
            });

            /*-----------------------------------------------------------------------------------------------*/
            $("#txtEquipment").change(function () {

                var g = $(this).val();
                var civil = $('#txtGroundBreaking').val();
                if (civil == "") {
                    civil = 0;
                }
                var major = $('#txtCivil').val();
                if (major == "") {
                    major = 0;
                }
                var cmpd = $('#txtCommissioning').val();
                if (cmpd == "") {
                    cmpd = 0;
                }
                if (g != "") {
                    if (parseInt(civil) != "0") {
                        if (parseInt(g) < parseInt(civil)) {
                            jAlert('<strong>Major equipment erection should not be less than ground breaking.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                    if (parseInt(major) != "0") {
                        if (parseInt(g) < parseInt(major)) {
                            jAlert('<strong>Major equipment erection should not be less than civil and structural completion.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                    if (parseInt(cmpd) != "0") {
                        if (parseInt(g) > parseInt(cmpd)) {
                            jAlert('<strong>Major equipment erection should not be greater than start of commercial production.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $("#txtCommissioning").change(function () {

                var g = $(this).val();
                var civil = $('#txtGroundBreaking').val();
                if (civil == "") {
                    civil = 0;
                }
                var major = $('#txtCivil').val();
                if (major == "") {
                    major = 0;
                }
                var cmpd = $('#txtEquipment').val();
                if (cmpd == "") {
                    cmpd = 0;
                }
                if (g != "") {
                    if (parseInt(civil) != "0") {
                        if (parseInt(g) < parseInt(civil)) {
                            jAlert('<strong>Start of commercial production should not be less than ground breaking.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                    if (parseInt(major) != "0") {
                        if (parseInt(g) < parseInt(major)) {
                            jAlert('<strong>Start of commercial production should not be less than civil and structural completion.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                    if (parseInt(cmpd) != "0") {
                        if (parseInt(g) < parseInt(cmpd)) {
                            jAlert('<strong>Start of commercial production should not be less than major equipment erection.</strong>', projname);
                            $(this).val('');
                            return false;
                        }
                    }
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $("#txtCommissioning").val($("#ddlCommProdInMonth").val());

            /*-----------------------------------------------------------------------------------------------*/

            $("#ddlCommProdInMonth").change(function () {
                var n = $(this).val();
                $("#txtCommissioning").val(n);
            });

            if ('<%=Session["ApplicationFor"] %>' == "1") {
                document.getElementById("txtManagerExist").readOnly = true;
                document.getElementById("txtSupervisorExist").readOnly = true;
                document.getElementById("txtSkilledExist").readOnly = true;
                document.getElementById("txtSemiskilledExist").readOnly = true;
                document.getElementById("txtUnskilledExist").readOnly = true;
            }
            else {
                document.getElementById("txtManagerExist").readOnly = false;
                document.getElementById("txtSupervisorExist").readOnly = false;
                document.getElementById("txtSkilledExist").readOnly = false;
                document.getElementById("txtSemiskilledExist").readOnly = false;
                document.getElementById("txtUnskilledExist").readOnly = false;
            }

            $('#DvDet1').hide();
            $('#txtTotal').attr('readonly', 'readonly');

            if ($("#ddlUnit option:selected").text() == "Other") {
                $('#DvSpecify').show();
            }
            else {
                $('#DvSpecify').hide();
            }

            /*-----------------------------------------------------------------------------------------------*/

            $("#ddlUnit").change(function () {
                if ($("#ddlUnit option:selected").text() == "Other") {
                    $('#DvSpecify').show();
                }
                else {
                    $('#DvSpecify').hide();
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            if ($("#drpProductUnit option:selected").text() == "Other") {
                $('#txtOtherProductUnit').show();
            }
            else {
                $('#txtOtherProductUnit').hide();
            }

            /*-----------------------------------------------------------------------------------------------*/

            $("#drpProductUnit").change(function () {
                if ($("#drpProductUnit option:selected").text() == "Other") {
                    $('#txtOtherProductUnit').show();
                }
                else {
                    $('#txtOtherProductUnit').hide();
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            if ($("#ddlprojloc").val() == "1") {
                $('#DvDet1').show();
                $('#Locdetails1').show();
            }
            else {
                $('#DvDet1').hide();
                $('#Locdetails1').hide();
            }

            /*-----------------------------------------------------------------------------------------------*/

            $("#ddlprojloc").change(function () {
                if ($(this).val() == "1") {
                    $('#DvDet1').show();
                    $('#Locdetails1').show();
                }
                else {
                    $('#DvDet1').hide();
                    $('#Locdetails1').hide();
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            if ($("#ddlprojectUnits").val() == "1") {
                $('#DivOtherUnits').show();
                $('#Div1').show();
            }
            else {
                $('#DivOtherUnits').hide();
                $('#Div1').hide();
            }

            /*-----------------------------------------------------------------------------------------------*/

            $("#ddlprojectUnits").change(function () {
                if ($(this).val() == "1") {
                    $('#DivOtherUnits').show();
                    $('#Div1').show();
                }
                else {
                    $('#DivOtherUnits').hide();
                    $('#Div1').hide();
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'

            $('#btnProduct').click(function () {
                if (blankFieldValidation('txtProductNamedet', 'Product name', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtAnlCap', 'Proposed annual capacity', projname) == false) {
                    return false;
                }
                if (DropDownValidation('drpProductUnit', '0', 'Unit', projname) == false) {
                    return false;
                }
                if ($('#drpProductUnit').val() == "4") {
                    if (blankFieldValidation('txtOtherProductUnit', 'Other Unit', projname) == false) {
                        return false;
                    }
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $('#AddMore').click(function () {
                if (blankFieldValidation('txtUnit', 'Unit name', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtProduct', 'Product', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtCapacity', 'Total capacity', projname) == false) {
                    return false;
                }
                if (DropDownValidation('ddlState', '0', 'State', projname) == false) {
                    return false;
                }
                if (DropDownValidation('ddldist', '0', 'District', projname) == false) {
                    return false;
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $('#btnUnitAdd').click(function () {
                if (blankFieldValidation('txtOtherUnitname', 'Unit name', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtOtherProd', 'Product', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtOthercapacity', 'Total capacity', projname) == false) {
                    return false;
                }
                if (DropDownValidation('ddlCountry', '0', 'Country', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtCityname', 'City', projname) == false) {
                    return false;
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            var txtDirectEmp = document.getElementById('txtDirectEmp').value;
            var txtTotalProposed = document.getElementById('txtTotalProposed').value;
            var txtContractualEmp = document.getElementById('txtContractualEmp').value;

            if (parseFloat(txtDirectEmp) > parseFloat(txtTotalProposed)) {
                $('#txtDirectEmp').val("");
                $('#txtContractualEmp').val("");
                return false;
            }
            else {
                var result = parseFloat(txtTotalProposed) - parseFloat(txtDirectEmp);
                if (!isNaN(result)) {
                    document.getElementById('txtContractualEmp').value = parseInt(result);
                    document.getElementById("txtContractualEmp").readOnly = true;
                }
            }

            if (txtDirectEmp == "") {
                txtDirectEmp = 0;
                document.getElementById("txtContractualEmp").readOnly = false;
            }
            if (txtTotalProposed == "") {
                txtTotalProposed = 0;
            }
            if (txtContractualEmp == "") {
                txtContractualEmp = 0;
            }
            document.getElementById("txtContractualEmp").readOnly = false;
            document.getElementById("txtDirectEmp").readOnly = false;

            /*-----------------------------------------------------------------------------------------------*/

            $('#txtDirectEmp').change(function () {
                var txtDirectEmp = document.getElementById('txtDirectEmp').value;
                var txtTotalProposed = document.getElementById('txtTotalProposed').value;
                var txtContractualEmp = document.getElementById('txtContractualEmp').value;

                if (parseFloat(txtDirectEmp) > parseFloat(txtTotalProposed)) {
                    $('#txtDirectEmp').val("");
                    $('#txtContractualEmp').val("");
                    return false;
                }
                else {
                    var result = parseFloat(txtTotalProposed) - parseFloat(txtDirectEmp);
                    if (!isNaN(result)) {
                        document.getElementById('txtContractualEmp').value = parseInt(result);
                        document.getElementById("txtContractualEmp").readOnly = true;
                    }
                }

                if (txtDirectEmp == "") {
                    txtDirectEmp = 0;
                    document.getElementById("txtContractualEmp").readOnly = false;
                }
                if (txtTotalProposed == "") {
                    txtTotalProposed = 0;
                }
                if (txtContractualEmp == "") {
                    txtContractualEmp = 0;
                }
            });

            var txtDirectEmp = document.getElementById('txtDirectEmp').value;
            var txtTotalProposed = document.getElementById('txtTotalProposed').value;
            var txtContractualEmp = document.getElementById('txtContractualEmp').value;

            if (parseFloat(txtContractualEmp) > parseFloat(txtTotalProposed)) {
                $('#txtContractualEmp').val("");
                $('#txtDirectEmp').val("");
            }
            else {
                var result = parseFloat(txtTotalProposed) - parseFloat(txtContractualEmp);
                if (!isNaN(result)) {
                    document.getElementById('txtDirectEmp').value = parseInt(result);
                    document.getElementById("txtDirectEmp").readOnly = true;
                }
            }

            if (txtDirectEmp == "") {
                txtDirectEmp = 0;
            }

            if (txtTotalProposed == "") {
                txtTotalProposed = 0;
            }

            if (txtContractualEmp == "") {
                txtContractualEmp = 0;
                document.getElementById("txtDirectEmp").readOnly = false;
            }

            /*-----------------------------------------------------------------------------------------------*/

            $('#txtContractualEmp').change(function () {
                var txtDirectEmp = document.getElementById('txtDirectEmp').value;
                var txtTotalProposed = document.getElementById('txtTotalProposed').value;
                var txtContractualEmp = document.getElementById('txtContractualEmp').value;


                if (parseFloat(txtContractualEmp) > parseFloat(txtTotalProposed)) {
                    $('#txtContractualEmp').val("");
                    $('#txtDirectEmp').val("");
                }
                else {
                    var result = parseFloat(txtTotalProposed) - parseFloat(txtContractualEmp);
                    if (!isNaN(result)) {
                        document.getElementById('txtDirectEmp').value = parseInt(result);
                        document.getElementById("txtDirectEmp").readOnly = true;
                    }
                }

                if (txtDirectEmp == "") {
                    txtDirectEmp = 0;
                }
                if (txtTotalProposed == "") {
                    txtTotalProposed = 0;
                }
                if (txtContractualEmp == "") {
                    txtContractualEmp = 0;
                    document.getElementById("txtDirectEmp").readOnly = false;
                }
            });

            /*-----------------------------------------------------------------------------------------------*/

            $('.backcheck2').hide();

            /*-----------------------------------------------------------------------------------------------*/
            $('#btnNext').click(function () {
                var sessioncal = '<%=Session["proposalno"] %>';
                var Qrvalue = '<%= Request.QueryString["StrPropNo"] %>';
                if ((sessioncal != null) && (sessioncal != "")) {
                    $('.backcheck2').show();
                }
                else if ((Qrvalue != null) && (Qrvalue != "")) {
                    $('.backcheck2').show();
                }
                else {
                    $('.wizard2 .nav-tabs > li').removeClass("backactive");
                    $('.backcheck2').hide();
                }

                if (blankFieldValidation('txtUnitName', 'Name of the unit', projname) == false) {
                    return false;
                }

                if (DropDownValidation('drpEin', '0', 'EIN/IEM/IL', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtregApp', 'EIN/IEM/IL', projname) == false) {
                    return false;
                }

                if (DropDownValidation('ddlSector', '0', 'Sector of activity', projname) == false) {
                    return false;
                }

                if (DropDownValidation('ddlSubSector', '0', 'Sub sector', projname) == false) {
                    return false;
                }

                var grdctrl12 = document.getElementById("grdProduct");
                if ((grdctrl12 == null) || (grdctrl12.rows.length == 1)) {
                    jAlert('<strong>Product detail can not be left blank !</strong>', projname);
                    document.getElementById('txtProductNamedet').focus();
                    return false;
                }

                if (blankFieldValidation('txtlanddev', 'Land including land development', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtBuilding', 'Building & civil construction', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtPlantMachinery', 'Plant & machinery', projname) == false) {
                    return false;
                }

                var ProjCat = '<%=Session["ProjectCategory"] %>';

                if (document.getElementById('txtTotalCapitalInv').value != "") {
                    if (ProjCat == "2") {// 2 for MSME and 1 for Large Project category
                        if (parseFloat(document.getElementById('txtTotalCapitalInv').value) >= 5000) {
                            jAlert('<strong>In case of MSME Total capital investment value should not be greater than 50 crore !</strong>', projname);
                            document.getElementById('txtPlantMachinery').focus();
                            return false;
                        }
                    }
                    else {
                        if (parseFloat(document.getElementById('txtTotalCapitalInv').value) < 5000) {
                            jAlert('<strong>In case of Large Project Total capital investment value should not be less than 50 crore !</strong>', projname);
                            document.getElementById('txtPlantMachinery').focus();
                            return false;
                        }
                    }
                }

                var jj = $('#txtTotalCapitalInv').val();
                if (jj == '0.00') {
                    jAlert('<strong>Total capital investment should not be zero !</strong>', projname);
                    document.getElementById('txtPlantMachinery').focus();
                    return false;
                }

                if (blankFieldValidation('txtTotalCapitalInv', 'Total capital investment', projname) == false) {
                    return false;
                }

                if (DropDownValidation('ddlCommProdInMonth', '0', 'Period to Commence Commercial Production', projname) == false) {
                    return false;
                }

                if (DropDownValidation('ddlPolutionCategory', '0', 'Pollution category', projname) == false) {
                    return false;
                }

                if (blankFieldValidation('txtEquity', 'Equity contribution', projname) == false) {
                    return false;
                }

                var tl1 = $('#txtTotalCapitalInv').val();
                var tl2 = $('#txtTotal').val();

                if (tl1 != tl2) {
                    jAlert('<strong>Total capital investment and sources of finance should match!</strong>', projname);
                    document.getElementById('txtTotal').value = "";
                    document.getElementById('txtTotal').focus();
                    return false;
                }
                if (blankFieldValidation('txtTotal', 'Total', projname) == false) {
                    return false;
                }

                if ((document.getElementById('txtFDI').value != "") && (document.getElementById('txtFDI').value != "0.00")) {
                    if (document.getElementById('hdnOtherFin').value == "") {
                        jAlert('<strong>In case of FDI, please upload relevant document can not be left blank !</strong>', projname);
                        document.getElementById('FileOtherFin').focus();
                        return false;
                    }
                }


                var ProjCat = '<%=Session["ProjectCategory"] %>';
                if (ProjCat == "2") {
                    $("#dvIRR").show();
                    $('#spIRR').show();
                    if (blankFieldValidation('txtIRR', 'IRR', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtDSCR', 'DSCR', projname) == false) {
                        return false;
                    }
                }
                else {
                    $("#dvIRR").hide();
                    $('#spIRR').hide();
                }

                if (blankFieldValidation('txtGroundBreaking', 'Ground breaking', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtCivil', 'Civil and structural completion', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtEquipment', 'Major equipment erection', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtCommissioning', 'Start of commercial production', projname) == false) {
                    return false;
                }


                var jk = $("#drpEin option:selected").text();
                if (jk != '') {
                    if (document.getElementById('hdnIndustryEntMemorandum').value == "") {
                        jAlert('<strong>' + jk + ' can not be left blank !</strong>', projname);
                        document.getElementById('FileIndustryEntMemorandum').focus();
                        return false;
                    }
                }

                if (document.getElementById('hdnFeasibilityReport').value == "") {
                    jAlert('<strong>Feasibility report can not be left blank !</strong>', projname);
                    document.getElementById('FileFeasibilityReport').focus();
                    return false;
                }

                if (('<%=Session["Constitution"] %>' != "1") && ('<%=Session["Constitution"] %>' != "2")) {
                    if ((document.getElementById('hdnBoardResolution').value == "") && (document.getElementById('FileBoardResolution').value == "")) {
                        jAlert('<strong>Board resolution to take up the project can not be left blank !</strong>', projname);
                        document.getElementById('FileBoardResolution').focus();
                        return false;
                    }
                }

                if ('<%=Session["ApplicationFor"] %>' == "1") {//1 for New Unit 2 for Existing Unit
                    if (blankFieldValidation('txtManagerProposed', 'Managerial Proposed', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtSupervisorProposed', 'Supervisory Proposed', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtSkilledProposed', 'Skilled Proposed', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtSemiskilledProposed', 'Semi skilled Proposed', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtUnskilledProposed', 'Un skilled Proposed', projname) == false) {
                        return false;
                    }
                }

                if ('<%=Session["ApplicationFor"] %>' == "2") {//1 for New Unit 2 for Existing Unit

                    if (blankFieldValidation('txtManagerExist', 'Managerial Existing', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtManagerProposed', 'Managerial Proposed', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtSupervisorExist', 'Supervisory Existing', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtSupervisorProposed', 'Supervisory Proposed', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtSkilledExist', 'Skilled Existing', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtSkilledProposed', 'Skilled Proposed', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtSemiskilledExist', 'Semi skilled Existing', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtSemiskilledProposed', 'Semi skilled Proposed', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtUnskilledExist', 'Un skilled Existing', projname) == false) {
                        return false;
                    }
                    if (blankFieldValidation('txtUnskilledProposed', 'Un skilled Proposed', projname) == false) {
                        return false;
                    }
                }

                if (blankFieldValidation('txtDirectEmp', 'Proposed direct employment (On company payroll)', projname) == false) {
                    return false;
                }
                if (blankFieldValidation('txtTotalProposed', 'Total employment)', projname) == false) {
                    return false;
                }
                var txtDirectEmpValue = document.getElementById('txtDirectEmp').value;
                var txtTotalProposedValue = document.getElementById('txtTotalProposed').value;
                if (parseFloat(txtTotalProposedValue) <= 0) {
                    jAlert('<strong>Total employment can not Zero</strong>', projname);
                    document.getElementById('txtManagerProposed').focus();
                    return false;
                }
                if (parseFloat(txtDirectEmpValue) <= 0) {
                    jAlert('<strong>Proposed direct employment can not Zero</strong>', projname);
                    document.getElementById('txtDirectEmp').focus();
                    return false;
                   
                }

                if (blankFieldValidation('txtContractualEmp', 'Proposed contratual employment', projname) == false) {
                    return false;
                }
                if (DropDownValidation('ddlprojloc', '0', 'projects at other locations', projname) == false) {
                    return false;
                }
                if ((document.getElementById('ddlprojloc').value == "1")) {
                    var grdctrl = document.getElementById("gvLOCDetails");
                    if ((grdctrl == null) || (grdctrl.rows.length == 1)) {
                        jAlert('<strong>Location detail can not be left blank !</strong>', projname);

                        document.getElementById('txtUnit').focus();
                        return false;
                    }
                }
                if (DropDownValidation('ddlprojectUnits', '0', 'Is there any unit outside india', projname) == false) {
                    return false;
                }
                if ((document.getElementById('ddlprojectUnits').value == "1")) {
                    var grdctrl = document.getElementById("gvOtherUnits");
                    if ((grdctrl == null) || (grdctrl.rows.length == 1)) {

                        jAlert('<strong>Unit detail can not be left blank !</strong>', projname);
                        document.getElementById('txtOtherUnitname').focus();
                        return false;
                    }
                }
            });
        }

        /*-----------------------------------------------------------------------------------------------*/

        function sum() {
            var txtLandinclude = document.getElementById('txtlanddev').value;
            var txtPlant = document.getElementById('txtPlantMachinery').value;
            var txtBuilding = document.getElementById('txtBuilding').value;
            var txtOthers = document.getElementById('txtOthers').value;

            if (txtLandinclude == "") {
                txtLandinclude = 0;
            }
            if (txtPlant == "") {
                txtPlant = 0;
            }
            if (txtBuilding == "") {
                txtBuilding = 0;
            }
            if (txtOthers == "") {
                txtOthers = 0;
            }

            var result = parseFloat(txtLandinclude) + parseFloat(txtPlant) + parseFloat(txtBuilding) + parseFloat(txtOthers);
            if (!isNaN(result)) {
                document.getElementById('txtTotalCapitalInv').value = parseFloat(result).toFixed(2);
            }
        }

        /*-----------------------------------------------------------------------------------------------*/

        function sumFixed() {
            var txtEquity = document.getElementById('txtEquity').value;
            var txtBankFinance = document.getElementById('txtBankFinance').value;

            if (txtEquity == "") {
                txtEquity = 0;
            }
            if (txtBankFinance == "") {
                txtBankFinance = 0;
            }

            var result = parseFloat(txtEquity) + parseFloat(txtBankFinance);
            if (!isNaN(result)) {
                document.getElementById('txtTotal').value = parseFloat(result).toFixed(2);
            }
        }

        /*-----------------------------------------------------------------------------------------------*/

        function sumExist() {
            var txtManagerExist = document.getElementById('txtManagerExist').value;
            var txtSupervisorExist = document.getElementById('txtSupervisorExist').value;
            var txtSkilledExist = document.getElementById('txtSkilledExist').value;
            var txtSemiskilledExist = document.getElementById('txtSemiskilledExist').value;
            var txtUnskilledExist = document.getElementById('txtUnskilledExist').value;

            if (txtManagerExist == "") {
                txtManagerExist = 0;
            }
            if (txtSupervisorExist == "") {
                txtSupervisorExist = 0;
            }
            if (txtSkilledExist == "") {
                txtSkilledExist = 0;
            }
            if (txtSemiskilledExist == "") {
                txtSemiskilledExist = 0;
            }
            if (txtUnskilledExist == "") {
                txtUnskilledExist = 0;
            }

            var result = parseFloat(txtManagerExist) + parseFloat(txtSupervisorExist) + parseFloat(txtSkilledExist) + parseFloat(txtSemiskilledExist) + parseFloat(txtUnskilledExist);
            if (!isNaN(result)) {
                document.getElementById('txtTotalExist').value = result;
            }
        }

        /*-----------------------------------------------------------------------------------------------*/

        function sumProposed() {
            var txtManagerProposed = document.getElementById('txtManagerProposed').value;
            var txtSupervisorProposed = document.getElementById('txtSupervisorProposed').value;
            var txtSkilledProposed = document.getElementById('txtSkilledProposed').value;
            var txtSemiskilledProposed = document.getElementById('txtSemiskilledProposed').value;
            var txtUnskilledProposed = document.getElementById('txtUnskilledProposed').value;

            if (txtManagerProposed == "")
                txtManagerProposed = 0;
            if (txtSupervisorProposed == "")
                txtSupervisorProposed = 0;
            if (txtSkilledProposed == "")
                txtSkilledProposed = 0;
            if (txtSemiskilledProposed == "")
                txtSemiskilledProposed = 0;
            if (txtUnskilledProposed == "")
                txtUnskilledProposed = 0;

            var result = parseFloat(txtManagerProposed) + parseFloat(txtSupervisorProposed) + parseFloat(txtSkilledProposed) + parseFloat(txtSemiskilledProposed) + parseFloat(txtUnskilledProposed);
            if (!isNaN(result)) {
                document.getElementById('txtTotalProposed').value = result;
                var k = document.getElementById('txtDirectEmp').value;
                var n = document.getElementById('txtContractualEmp').value;
                var m = parseFloat(k) + parseFloat(n);
                if (m != result) {
                    document.getElementById('txtDirectEmp').value = "";
                    document.getElementById('txtContractualEmp').value = "";
                    document.getElementById("txtDirectEmp").readOnly = false;
                    document.getElementById("txtContractualEmp").readOnly = false;
                }
            }
        }

        /*-----------------------------------------------------------------------------------------------*/

        function inputLimiter(e, allow) {
            var AllowableCharacters = '';
            if (allow == 'NameCharacterscomma') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz,';
            }
            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'IEMVAlid') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/-';
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
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#-,./;\'';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
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

        /*-----------------------------------------------------------------------------------------------*/

        $('#txtOtherUnit').keyup(function () {
            this.value = this.value.toUpperCase();
        });
    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 700px;
            height: 450px;
        }
    </style>
</body>
</html>
