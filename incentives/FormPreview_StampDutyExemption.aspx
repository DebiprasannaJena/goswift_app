<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormPreview_StampDutyExemption.aspx.cs" Inherits="incentives_FormPreview_StampDutyExemption" %>

<!DOCTYPE html>
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="pealmenu" TagPrefix="uc5" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script>
        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });

            $('.Pioneersec,.attorneysec,.adhardetails').hide();
            $(".applyby").on("click", function () {
                if ($("input:checked").val() == 'Self') {
                    $('.adhardetails').show();
                    $('.attorneysec').hide();
                }
                else {
                    $('.attorneysec').show();
                    $('.adhardetails').hide();
                }
            });
            $(".optradioPriority").on("click", function () {
                if ($("input:checked").val() == 'Yes') {
                    $('.Pioneersec').show();
                }
                else {
                    $('.Pioneersec').hide();
                }
            });
        });


        function readURL(input) {
            if (input.files && input.files[0]) {//Check if input has files.
                var reader = new FileReader(); //Initialize FileReader.

                reader.onload = function (e) {
                    $('#PreviewImage').attr('src', e.target.result);
                    $('#PreviewImage').attr('style', 'display:block');
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div id="divHeader1">
            <uc2:header ID="header" runat="server" />
        </div>
        <div class="registration-div investors-bg">
            <div id="exTab1" class="">
                <div class="investrs-tab">
                   <%-- <uc5:pealmenu ID="Peal" runat="server" />--%>
                </div>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1a">
                        <div class="form-sec">
                            <div class="innertabs m-b-10">
                                <ul class="nav nav-pills pull-right">
                                    <li><a href="incentiveoffered.aspx">Incentive Offered</a></li>
                                    <li class="active"><a href="Basic_Details.aspx">Apply For incentive</a></li>
                                    <li><a href="ViewApplicationStatus.aspx">View Application Status</a></li>
                                </ul>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-header">
                                <div class="iconsdiv">
                                    <%--<h1>
                                        APPLICATION FOR GRANT
                                    </h1>--%>
                                     <h4 class="panel-title">
                                                        <a>APPLICATION FOR GRANT OF PRE AND POST PRODUCTION THRUST OR PRIORITY SECTOR STATUS UDER INDUSTRIAL POLICY RESOLUTION-2022</a>
                                                    </h4>
                                    <a href="javascript:void(0);" title="Print" id="printbtn" class="pull-right printbtn">
                                        <i class="fa fa-print"></i></a>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="form-body">
                                <div class="incentivepreiview">
                                    <div class="preiviewheader text-center">
                                        <h4>
                                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                        </h4>
                                    </div>
                                    <div class="prieviewdatasec">
                                        <h4>
                                            From</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                M/s :<asp:Label ID="lblMr" runat="server"></asp:Label></p>
                                            <p>
                                                At/PO :<asp:Label ID="lblAddress" runat="server"></asp:Label></p>
                                            <p>
                                                Dist.(Location / proposed location of the Industrial Unit) :<asp:Label ID="Label5" runat="server"></asp:Label></p>
                                        </div>
                                        <h4>
                                            To</h4>
                                        <div id="MGD" runat="server" visible="false">
                                        <div class="padding-left-20">
                                            <p>
                                                The Managing Director,IPICOL,Bhubaneswar
                                                <asp:Label ID="lblMG"  Text="IPICOL" runat="server"></asp:Label>
                                                 
                                            </p>
                                           
                                        </div>
                                            </div>

                                        <div id="GMD" runat="server" visible="false">
                                        <div class="padding-left-20">
                                            <p>
                                                The General Manager, Regional lndustries Centre / District lndustries Centre
                                                <asp:Label ID="lblGM" runat="server"></asp:Label></p>
                                        </div>
                                            </div>
                                        <h4>
                                            Sub: - Application for Grant of (Provisional / Post production)Thrust / Priority Sector status under Industrial
                                            Policy Resolution 2022.
                                        </h4>
                                        <h4>
                                            Sir,</h4>
                                        <div class="padding-left-20">
                                            <p>
                                                In accordance with the provisions laid down in Industrials Policy Resolution 2022
                                                and its operational guidelines, I do hereby submit the application for grant of
                                                Provisional Thrust / Priority Sector  or Thrust / Priority Sector status.
                                            </p>
                                        </div>
                                    </div>
                                    <div class="prievewdynamicdata">
                                        <div class="panel-group padding-20" id="accordion" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingOne">
                                                    <h4 class="panel-title">
                                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                            aria-expanded="true" aria-controls="collapseOne">Industrial Unit's Details </a>
                                                    </h4>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                            <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                1.</label>
                                                        </div>
                                                    </div>
                                                        
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                Name of Enterprise/Industrial Unit</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span><asp:Label ID="lbl_EnterPrise_Name" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                 2.</label>                                                           
                                                        </div>

                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                Category of the Unit</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Unit_Cat" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                     <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                 3.</label>                                                           
                                                        </div>

                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                Address of Registered Office Unit</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Industry_Address" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>


                                                     <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                4.</label>                                                           
                                                        </div>

                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                Type of Organization</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Org_Type" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                     <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                               5.</label>                                                           
                                                        </div>
                                                   
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                <asp:Label ID="Lbl_Org_Name_Type" runat="server" Text="Name of Managing Partner"></asp:Label>
                                                            </label>
                                                            <div class="col-sm-6" style="padding-right: 0px">
                                                                <span class="colon">:</span>
                                                                <asp:Label runat="server" ID="lbl_Gender_Partner" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                 6.</label>                                                           
                                                        </div>

                                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                 Date of first fixed capital investment i.e. land / building / plant & machinary and balacing equipment</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_First_Capital_Invst" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                     <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                               7.</label>                                                           
                                                        </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                EIN/ PC/ IEM/PEAL approval letter & Production Certificate / IL No.</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_EIN_IL_NO" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                EIN/ PC/ IEM/PEAL approval letter & Production Certificate / IL Date </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_EIN_IL_Date" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-12">
                                                                              10.
                                                                            </label>
                                                                          
                                                                        </div>

                                                         <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-12">
                                                                                Proposed items or Items of manufacture / activities with proposed capacity / installed capacity 
                                                                            </label>
                                                                            <div class="col-sm-12  margin-bottom10">
                                                                                <asp:GridView ID="Grd_Production_Before" OnRowDataBound="Grd_Production_Before_RowDataBound" runat="server" CssClass="table table-bordered" 
                                                                                    DataKeyNames="vchProductName" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Slno.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Sl_No_Product_Before" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="5%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Product/Service Name">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Product_Name_Before" runat="server" Text='<%# Eval("vchProductName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="35%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Quantity_Before" runat="server" Text='<%# Eval("intQuantity") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="20%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Units">
                                                                                            <ItemTemplate>
                                                                                                <asp:HiddenField ID="hdnUnitType" runat="server" Value='<%# Eval("intUnitType") %>' />
                                                                                                <asp:Label ID="Lbl_Unit_Before" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Other Units">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Other_Unit_Before" runat="server" Text='<%# Eval("vchOtherUnit") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Value">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_Value_Before" runat="server" Text='<%# Eval("decValue") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="20%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                      <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                 11.</label>                                                           
                                                        </div>

                                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                Proposed date of production / Date of Production</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_date_production" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                     <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                               12.</label>                                                           
                                                        </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                               Proposed location of the Project</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_location" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                              Present status of the Project   </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_status" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                      <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                 13.</label>                                                           
                                                        </div>

                                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                Type of Deed / Agreement to be executed</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_Deed" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>


                                                      <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                 14.</label>                                                           
                                                        </div>

                                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                Amount of Stamp Duty Exemption claimed</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_claimed" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                     <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                 15.</label>                                                           
                                                        </div>

                                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                Amount of Stamp Duty Exemption availed under any scheme of State Govt / Central Govt (Gol) / Govt. Agencies / Financial institutions </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_availed" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                     <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                 16.</label>                                                           
                                                        </div>

                                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                Amount of deferential claim to be exempted </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_deferential" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                       <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                 17.</label>                                                           
                                                        </div>

                                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4">
                                                                Statutory clearances, if any </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:Label ID="lbl_clearances" runat="server" CssClass="form-control-static"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>



                                                     
                                                 
                                                </div>
                                            </div>
                                                                                                                        
                                            <div class="panel panel-default">
                                              
                                                <div class="panel panel-default">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading" role="tab">
                                                            <h4 class="panel-title">
                                                                <a class="tect-center">Document CheckList</a>
                                                            </h4>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">                                                               
                                                                <div class="col-sm-12">
                                                                    <asp:GridView ID="GridView_CheckList" OnRowDataBound="GridView_CheckList_RowDataBound" runat="server"  CssClass="table table-bordered" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Slno.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="4%"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Document Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_Document_Name" runat="server" Text='<%# Eval("vchDocName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="View">
                                                                                <ItemTemplate>
                                                                                     <div class="input-group">
                                                                        <asp:HiddenField ID="Hid_Document_File_Name" runat="server" Value='<%# Eval("vchFileName") %>' />
                                                                        <asp:HyperLink ID="Hy_Document_Doc" runat="server" CssClass="input-group-addon bg-blue"
                                                                            ToolTip="Click to View Document" Target="_blank" NavigateUrl='<%# "~/incentives/Files/InctBasicDoc/"+ Eval("vchFileName") %>'><i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                                  
                                                                                </ItemTemplate>
                                                                              
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
                                    </div>
                                </div>                             
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc3:footer ID="footer" runat="server" />
        <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
        <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'; //  'Incentive';

            $(function () {
                $('.datePicker').datepicker({
                    dateFormat: 'dd:mm:yyyy', separator: ' @ ', minDate: new Date(), autoclose: true
                });
            });


            function pageLoad() {
                var hdval = $('#HdnValueFlag').val();
                if (hdval == 1) {
                    $('.innertabs ,.investrs-tab').hide();
                    $('#divHeader1').hide();
                    $('#divUpload').hide();
                    $('#btnApply').hide();
                }

                getAllLinks();
            }


            function getAllLinks() {
                $("div.ss a").each(function () {
                    //                alert($(this).text());
                    var attr = $(this).attr('href');
                    if (attr == undefined) {
                        //$(this).closest('tr').hide();
                        $(this).text('No Document');
                    }
                });
            }

        </script>
    </form>
</body>
</html>
