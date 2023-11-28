<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Application.master"
    CodeFile="AddChallanDtls.aspx.cs" Inherits="Portal_Service_AddChallanDtls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Console/scripts/ajax.js" type="text/javascript"></script>
    <script src="../Console/scripts/AjaxScript.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function InIEvent() {
            $(".fldChallan").change(function () {
                debugger;
                var filename = $(".fldChallan").val().replace(/C:\\fakepath\\/i, '');
                var fileExtension = ['jpeg', 'jpg', 'png', 'pdf'];

                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    jAlert('<strong>>Please Upload  PDF,PNG,JPG,JPEG file Only!!', projname);
                    $(".fldChallan").val('');
                    return false;
                }

            });
        }

        $(document).ready(function () {
            $('.datePicker').datepicker({
                autoclose: true,
                format: 'dd-M-yyyy'
            });
            $('.ddlApplicationNo').chosen({ allow_single_deselect: true, no_results_text: 'No Item found for ' });
            var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>'
            //            $(".fldChallan").change(function (e) {
            //                var filename = $(".fldChallan").val().replace(/C:\\fakepath\\/i, '');
            //                var fileExtension = ['pdf'];

            //                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
            //                    jAlert('<strong>Please upload pdf file Only!', projname);
            //                    $(".fldChallan").val('');
            //                    return false;
            //                }
            //            });
            $('.RWM').click(function () {

                debugger;
                var tlamt = $(".tlm");

                var txtChallanNo = $(".txtChallanNo");
                var txtBankTransctionNo = $(".txtBankTransctionNo");
                var txtChallanAmount = $(".txtChallanAmount");

                alert($('.fldChallan').val());
                if (txtChallanNo.val() == "") {
                    jAlert('<strong>Challan No. should not be left blank.', projname);
                    $(".txtChallanNo").focus();
                    return false;
                }
                if (txtBankTransctionNo.val() == "") {
                    jAlert('<strong>Bank Transaction No. should not be left blank.', projname);
                    $(".txtBankTransctionNo").focus();
                    return false;
                }
                if (txtChallanAmount.val() == "") {
                    jAlert('<strong>Challan Amount should not be left blank.', projname);
                    $(".txtChallanAmount").focus();
                    return false;
                }
                if ($('.fldChallan').val() == '') {
                    jAlert('<strong>Please attach challan file</strong>', projname);
                    $(".fldChallan").focus();
                    return false;
                }
//                if (parseFloat(tlamt.val()) > 1) {
//                    if (parseFloat(txtChallanAmount.val()) < parseFloat(tlamt.val())) {
//                        jAlert('<strong>Challan amount should not be less than total amount.', projname);
//                        $(".txtChallanAmount").focus();
//                        return false;
//                    }
//                }

            });


        });
        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbersTest') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/';
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

        function sumAllAmount() {
            var txtAmount = document.getElementById('ContentPlaceHolder1_txtAmount').value;
            var txtPenaltyAmount = document.getElementById('ContentPlaceHolder1_txtPenaltyAmount').value;
            var txtPaymentOverdue = document.getElementById('ContentPlaceHolder1_txtPaymentOverdue').value;
            var txtDemandAmount = document.getElementById('ContentPlaceHolder1_txtDemandAmount').value;
            if (txtAmount == "")
                txtAmount = 0;
            if (txtPenaltyAmount == "")
                txtPenaltyAmount = 0;
            if (txtPaymentOverdue == "")
                txtPaymentOverdue = 0;
            if (txtDemandAmount == "")
                txtDemandAmount = 0;
            var result = (parseFloat(txtAmount) + parseFloat(txtPenaltyAmount) + parseFloat(txtDemandAmount)) - (parseFloat(txtPaymentOverdue));
            if (!isNaN(result)) {
                document.getElementById('ContentPlaceHolder1_txtTotalAmt').value = result;

            }

        }
    </script>
    <style type="text/css" media="all">
        /* fix rtl for demo */
        .chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
        .chosen-container .chosen-container-single .chosen-single
        {
            width: 100% !important;
        }
        .searchbox
        {
            background-color: #def3ff;
            padding: 8px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Challan Configuration</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Service</a></li><li><a>Challan Configuration</a></li></ul>
               </div>
        </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                    <asp:UpdatePanel ID="upd1" runat="server">
                                <ContentTemplate>
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="ApprovalConfig.aspx"> 
                              <i class="fa fa-file"></i></a>  
                           </div>
                        </div>
                       
                        <div class="panel-body">
                            <div class="table-responsive">
                            <div class="form-group">
                              <div class="row">
                              
                             
                                 <label class="col-sm-2">Department</label>  <div class="col-sm-3">
                                 <span class="colon">:</span>
                                   <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>--%>
                                  <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt" onselectedindexchanged="ddldept_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                 <%--    </ContentTemplate>
                                     </asp:UpdatePanel>--%>
                                          </div>
                          
                                        
                                         
                                 </div>
                                 <br />
                                 <div class="row">
                              
                                 <label class="col-sm-2">Application No</label>    <div class="col-sm-3">
                                 <span class="colon">:</span>
                                   <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>--%>
                                 <asp:DropDownList ID="ddlApplicationNo" runat="server" 
                                             CssClass="chosen-select-width ddlApplicationNo" AutoPostBack="True"  
                                             style="width:100%" 
                                             onselectedindexchanged="ddlApplicationNo_SelectedIndexChanged" > </asp:DropDownList>
                                        <%--          </ContentTemplate>
                                  <triggers>
                                    <asp:asyncpostbacktrigger controlid="ddldept"  />
                                   
                                    </triggers>
                                 </asp:UpdatePanel>--%>
                                  
                                          </div>
                                
                                        
                                         
                                 </div>
                                  <br />
                                 <div class="row">
                              
                                 <label class="col-sm-2">Amount</label>    <div class="col-sm-3">
                                 <span class="colon">:</span>
                                 
                                 <asp:TextBox ID="txtAmount" ReadOnly="true" MaxLength="20" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Decimal')"></asp:TextBox>
                                  
                                          </div>
                                
                                        
                                         
                                 </div>
                                   <br />
                                  <div class="row">
                              
                                 <label class="col-sm-2">Penalty Amount</label>    <div class="col-sm-3">
                                 <span class="colon">:</span>
                                 
                                 <asp:TextBox ID="txtPenaltyAmount" MaxLength="20" CssClass="form-control" runat="server" onkeyup = "sumAllAmount();"
                                        Onkeypress="return inputLimiter(event,'Decimal')"></asp:TextBox>
                                  
                                          </div>
                                
                                        
                                         
                                 </div>
                                   <br />
                                  <div class="row">
                              
                                 <label class="col-sm-2">Payment Over Due</label>    <div class="col-sm-3">
                                 <span class="colon">:</span>
                                 
                                 <asp:TextBox ID="txtPaymentOverdue"     onkeyup = "sumAllAmount();" MaxLength="20" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Decimal')"></asp:TextBox>
                                  
                                          </div>
                                
                                        
                                         
                                 </div>
                                   <br />
                                    <div class="row">
                              
                                 <label class="col-sm-2">Demand Amount</label>    <div class="col-sm-3">
                                 <span class="colon">:</span>
                                 
                                 <asp:TextBox ID="txtDemandAmount"  ReadOnly="true" MaxLength="20" CssClass="form-control" runat="server"
                                        Onkeypress="return inputLimiter(event,'Decimal')"></asp:TextBox>
                                  
                                          </div>
                                
                                        
                                         
                                 </div>
                                   <br />
                                    <div class="row">
                              
                                 <label class="col-sm-2">Total Amount</label>    <div class="col-sm-3">
                                 <span class="colon">:</span>
                                 
                                 <asp:TextBox ID="txtTotalAmt" ReadOnly="true" MaxLength="20" CssClass="form-control tlm" runat="server"
                                        Onkeypress="return inputLimiter(event,'Decimal')"></asp:TextBox>
                                  
                                          </div>
                                
                                        
                                         
                                 </div>
                                 </div>
                                 <div class="form-group">
                                            <div class="row ">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>
                                                               Challan No.
                                                            </th>
                                                            <th>
                                                             Bank Transaction No.
                                                            </th>
                                                            <th>
                                                             Amount
                                                            </th>
                                                            <th>
                                                             Challan File
                                                            </th>
                                                              <th>
                                                             Challan Deposit Date
                                                            </th>
                                                            <th>
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtChallanNo" TabIndex="80" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbersTest')"
                                                                    MaxLength="100" CssClass="form-control txtChallanNo" runat="server"></asp:TextBox>
                                                            </td>
                                                           <td>
                                                                <asp:TextBox ID="txtBankTransctionNo" TabIndex="80" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbersTest')"
                                                                    MaxLength="100" CssClass="form-control txtBankTransctionNo" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtChallanAmount" TabIndex="80" Onkeypress="return inputLimiter(event,'Decimal')"
                                                                    MaxLength="100" CssClass="form-control txtChallanAmount" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                   <asp:FileUpload ID="fldChallan" TabIndex="68" CssClass="form-control fldChallan" runat="server" />
                                                            </td>
                                                              <td>
                                                              <div class="input-group  date datePicker" id="datePicker1">
                                                                   <asp:TextBox ID="txtChallanDate"  CssClass="form-control" runat="server"></asp:TextBox>
                                                              <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                               </div>
                                                            </td>
                                                            <td width="100px">
                                                                <asp:Button runat="server" Text="Add" ID="btnAddMoreRWM" 
                                                                    CssClass="btn btn-mini btn-primary RWM" onclick="btnAddMoreRWM_Click"
                                                                  ></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="GrvChallan" runat="server" AutoGenerateColumns="false" 
                                                        CssClass="table table-bordered margin-bottom0" 
                                                        onrowdatabound="GrvChallan_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSlno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdpid2" Value='<%#Eval("intProId2") %>' runat="server" />
                                                                     <asp:HiddenField ID="hdnTransMode" Value='<%#Eval("TransMode") %>' runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="vchChallanNo" HeaderText="Challan No." />
                                                            <asp:BoundField DataField="vchTranscationNo" HeaderText="Bank Transaction No." />
                                                             <asp:BoundField DataField="vchAmount" HeaderText="Amount" />
                                                             <asp:BoundField DataField="vchChallanFile" HeaderText="Challan File" />
                                                              <asp:BoundField DataField="vchChallanDate" HeaderText="Challan Date" />
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnDelete2" CssClass="btn btn-danger btn-sm" runat="server"
                                                                        ImageUrl="~/images/rubbish.png" OnClientClick="return confirm('Are you sure you want to delete?');"
                                                                        ToolTip="Click To Delete !"  OnClick="imgbtnDelete2_Click" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                            <asp:Button ID="btnsave" runat="server" Text="Submit" 
                                    class="btn btn-add btn-sm save" onclick="btnsave_Click"
                                                   ></asp:Button>
                                                   <br />
                                                   <br />
                                 </div>
                              
                           </div>
                        
                        </div>
                        </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnAddMoreRWM" />
                                </Triggers>
                            </asp:UpdatePanel>
                     </div>
                  </div>
    </div>
    <!-- customer Modal1 -->
    <!-- /.modal-dialog -->
    <!-- /.modal -->
    </section>
    <!-- /.content -->
    <link href="../Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../Chosen/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {

            var txtAmount = document.getElementById('ContentPlaceHolder1_txtAmount').value;
            var txtPenaltyAmount = document.getElementById('ContentPlaceHolder1_txtPenaltyAmount').value;
            var txtPaymentOverdue = document.getElementById('ContentPlaceHolder1_txtPaymentOverdue').value;
            var txtDemandAmount = document.getElementById('ContentPlaceHolder1_txtDemandAmount').value;
            if (txtAmount == "")
                txtAmount = 0;
            if (txtPenaltyAmount == "")
                txtPenaltyAmount = 0;
            if (txtPaymentOverdue == "")
                txtPaymentOverdue = 0;
            if (txtDemandAmount == "")
                txtDemandAmount = 0;
            var result = (parseFloat(txtAmount) + parseFloat(txtPenaltyAmount) + parseFloat(txtDemandAmount)) - (parseFloat(txtPaymentOverdue));
            if (!isNaN(result)) {
                document.getElementById('ContentPlaceHolder1_txtTotalAmt').value = result;

            }
        }
    </script>
    
  <script type="text/javascript">
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      if (prm != null) {
          prm.add_endRequest(function (sender, e) {
              if (sender._postBackSettings.panelsToUpdate != null) {

                  for (var selector in config) {
                      $(selector).chosen(config[selector]);
                  }

                  $('.datePicker').datepicker({
                      autoclose: true,
                      format: 'dd-M-yyyy'
                  });
                  InIEvent();
              }
          });
      };
                                    </script>



       <script type="text/javascript">
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
        

    </div>
</asp:Content>
