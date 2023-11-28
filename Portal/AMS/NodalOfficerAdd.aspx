<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodalOfficerAdd.aspx.cs"
    Inherits="SingleWindow_NodalOfficerAdd" EnableEventValidation="false" %>

<%@ Register Src="~/Include/IncludeScript.ascx" TagName="IncludeScript" TagPrefix="ucIncludeScript" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Include/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/includes/Leftmenupanel.ascx" TagName="leftMenu" TagPrefix="ucLeftMenu" %>
<%@ Register Src="~/include/AMSfooter.ascx" TagName="footer" TagPrefix="ucfooter" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>SWP</title>
    <!-- Favicon and touch icons -->
    <link rel="shortcut icon" href="../images/favicon.ico" type="image/x-icon">
    <!-- Start Styles
         =====================================================================-->
    <script src="../../js/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../js/jQuery.alert.js" type="text/javascript"></script>
    <link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../../PortalCSS/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/lobipanel.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/flash.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/pe-icon-7-stroke.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/themify-icons.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/emojionearea.min.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/monthly.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/stylecrm.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/override.css" rel="stylesheet" type="text/css" />
    <link href="../../PortalCSS/jquery.timepicker.css" rel="stylesheet" type="text/css" />
    <!-- End Styles
         =====================================================================-->
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.timepicker.js" type="text/javascript"></script>
     <script src="../../js/Validator.js" type="text/javascript"></script>
    <script src="../../js/custom.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        pageHeader = "Manage Stakeholders "
        indicate = "yes"
        backMe = "yes"
        printMe = "yes"

        window.onload = function () {
            configTab();
            configButton();
            configTitleBar()
        }
    </script>
    <script type="text/javascript">
        $(document).ready(
            function () {
                var option = '';

                $('#btnAdd').click(
                    function (e) {
                        if ($('#lbEmployee option').size() > 0) {
                            if ($('#lbEmployee option:selected').val() == null) {
                                alert('Select an oficer name to add');
                                return false;
                            }
                            else {
                                $('#lbEmployee > option:selected').appendTo('#lbOfficer');
                                e.preventDefault();
                                //rearrangeList('#lbOfficer');
                                option = "";
                                $('#lbOfficer option').map(function () {
                                    option = option + '`' + $(this).val();
                                });

                                $('#hdnOfficer').val(option);
                                // alert($('#hdnOfficer').val());
                            }
                        }
                        else {
                            alert('Sorry, no officer(s) to add');
                            return false;
                        }
                    });

                $('#btnRemove').click(
                function (e) {
                    if ($('#lbOfficer option').size() > 0) {
                        if ($('#lbOfficer option:selected').val() == null) {
                            alert('Select an oficer name to remove');
                            return false;
                        }
                        else {
                            $('#lbOfficer > option:selected').appendTo('#lbEmployee');
                            e.preventDefault();
                            //rearrangeList('#lbEmployee');
                            option = "";
                            $('#lbOfficer option').map(function () {
                                option = option + '`' + $(this).val();
                            });

                            $('#hdnOfficer').val(option);
                            //alert($('#hdnOfficer').val());
                        }
                    }
                    else {
                        alert('Sorry, no officer(s) to remove');
                        return false;
                    }
                });


                //SEARCH NAME IN TEXT BOX
                $('#txtSearch').keyup(function () {
                   // debugger;
                    if ($("#txtSearch").val().length >= 3) {
                        srchText = $("#txtSearch").val();
                        var characterReg = /^\s*[a-zA-Z0-9,\s]+\s*$/; // FOR NO SPECIAL CHARACTERS
                        if (characterReg.test(srchText)) {
                            loadEmployeeDtls(srchText);
                        }
                    }
                });

                $('#btnSubmit').click(function () {
                    if (!ValidateDropdown('ddlType', 'Stakeholders Type')) { return false; }
                    var length = $('#lbOfficer option').size();
                    if (length == 0) {
                        alert('Please Add Officers Name');
                        return false;
                    }
                })

            });

        //            function byValue(a, b) {
        //                return a.value > b.value ? 1 : -1;
        //            };

        //            function rearrangeList(list) {
        //                //alert(list);
        //                $(list).find("option").sort(byValue).appendTo(list);
        //            }

        //VIEW EMPLOYEE DETAILS
        function loadEmployeeDtls(srchText, deptId) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../CICG_SWP_Service.asmx/GetEmployeeName",
                data: '{"SearchText":"' + srchText + '"}',
                dataType: "json",
                success: function (r) {
                    var lstEmployee = $("[id*=lbEmployee]");
                    lstEmployee.empty();
                    $.each(r.d, function () {
                        //lstEmployee.append($("<option></option>").val('1').html('Tapan'));
                        lstEmployee.append($("<option></option>").val(this['Id']).html(this['Name']));
                    });
                },
                error: function (r) {
                    alert('Error Occured');
                    // AjaxFailed;
                }
            });
        }

    </script>
</head>
<body class="hold-transition sidebar-mini">
    <div class="wrapper">
        <form id="form1" runat="server">
        <!--Header-->
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <ucheader:header ID="header1" runat="server" />
        <aside class="main-sidebar">
            <!-- sidebar -->
            <div class="sidebar">
                <!-- sidebar menu -->           
            <ucLeftMenu:leftMenu ID="LeftPannel1" runat="server" />               
            </div>
            <!-- /.sidebar -->
     </aside>
        <div class="content-wrapper">
            <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>
                    Manage Stakeholders</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>AMS</a></li><li><a>Manage Officers</a></li>
                </ul>
            </div>
        </section>
           
                

                 <section class="content">
                <div class="row">
                        <div class="col-md-12">
                        
                            <div class="panel panel-bd  lobidisable">
                            <div class="panel-heading">
                    <div class="btn-group buttonlist">
                        <a class="btn btn-add active" href="NodalOfficerAdd.aspx"><i class="fa fa-plus"></i>Add
                        </a>
                    </div>
                    <div class="btn-group buttonlist">
                        <a class="btn btn-add " href="NodalOfficerView.aspx"><i class="fa fa-file"></i>View
                        </a>
                    </div>
                </div>

                                <div class="panel-body">
                                    
                                    <div class="form-group row">
                                        <label class="col-sm-2"> Stakeholders Type <span class="text-danger">*</span></label>
                                        <div class="col-sm-4"><span class="colon">:</span>
                                         <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                            <asp:ListItem Value="3" Text="CDM, IPICOL "></asp:ListItem>
                                            <asp:ListItem Value="4" Text="GM (SLNA) "></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Nodal Officer"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="SLFC Member"></asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="form-group row">
                                        <label class="col-sm-2"> Officers Name <span class="text-danger">*</span></label>
                                        <div class="col-sm-4"><span class="colon">:</span>
                                          <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FEtxtSearch" runat="server" Enabled="True" TargetControlID="txtSearch"
                                                FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                ValidChars=" .">
                                            </cc1:FilteredTextBoxExtender>
                                            (<span class="mandatory">Search Officers Name To Add</span>)
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                     <div class="col-sm-8 col-sm-offset-2"> 
                                         <asp:ListBox ID="lbEmployee" runat="server" cssClass="form-control pull-left" style="width:280px;" ></asp:ListBox> 
                                          <div class="text-center pull-left" style="width:100px; margin-top: 20px;" >
                                            <input type="button" id="btnAdd" class="btn btn-xs btn-info" value="Add >>" style="width: 80px; margin-bottom: 5px;"/>
                                            <input type="button" id="btnRemove" class="btn btn-xs btn-danger" value="<< Remove" style="width:80px;" />
                                          </div>      
                                         
                                           <asp:ListBox ID="lbOfficer" runat="server" cssClass="form-control pull-left" EnableViewState="true" style="width:280px;"></asp:ListBox>
                                            
                                            <asp:HiddenField ID="hdnOfficer" runat="server" />
                                          </div>    
                                                                                            
                                     </div>
                                    <div class="row">
                                            <div class="col-sm-4 col-sm-offset-2">  <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success"
                                             OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                                            </div>      
                                    </div>  
                                </div>
                            </div>
                        </div>
                    </div>
               </section>
            
           
        </div>
        <!--Footer-->
        <ucfooter:footer ID="footer1" runat="server" />
        </form>
    </div>
</body>
</html>
