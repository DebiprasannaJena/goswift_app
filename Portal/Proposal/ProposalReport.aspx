<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Application.master"
    CodeFile="ProposalReport.aspx.cs" Inherits="Mastermodule_ProposalReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                autoclose: true,
                format: 'dd-M-yyyy'
            });
            $(".ap").change(function () {
                var a = $(this).val();
                if (a == '') {
                    a = "0";
                }
                var b = $(".at").val();
                if (b == '') {
                    b = "0";
                }
                if (parseInt(b) > "0") {
                    if (parseInt(a) > parseInt(b)) {
                        jAlert('<strong>Proposed employment from should not be greater than proposed employment to</strong>', 'SWP');
                        $(".ap").val('0');
                        $(".ap").focus();
                    }
                }
            });
            $(".at").change(function () {
                var a = $(this).val();
                if (a == '') {
                    a = "0";
                }
                var b = $(".ap").val();
                if (b == '') {
                    b = "0";
                }
                if (parseInt(b) > "0") {
                    if (parseInt(a) < parseInt(b)) {
                        jAlert('<strong>Proposed employment to should not be less than proposed employment from</strong>', 'SWP');
                        $(".at").val('0');
                        $(".at").focus();
                    }
                }
            });
            $(".PIF").change(function () {
                var a = $(this).val();
                if (a == '') {
                    a = "0";
                }
                var b = $(".PIFT").val();
                if (b == '') {
                    b = "0";
                }
                if (parseInt(b) > "0") {
                    if (parseInt(a) > parseInt(b)) {
                        jAlert('<strong>Proposed Capital Investment (INR in Lakhs) From should not be greater than Proposed Capital Investment (INR in Lakhs) To</strong>', 'SWP');
                        $(".PIF").val('0');
                        $(".PIF").focus();
                    }
                }
            });

            $(".PIFT").change(function () {
                var a = $(this).val();
                if (a == '') {
                    a = "0";
                }
                var b = $(".PIF").val();
                if (b == '') {
                    b = "0";
                }
                if (parseInt(b) > "0") {
                    if (parseInt(a) < parseInt(b)) {
                        jAlert('<strong>Proposed Capital Investment (INR in Lakhs) To should not be less than Proposed Capital Investment (INR in Lakhs) From</strong>', 'SWP');
                        $(".PIFT").val('0');
                        $(".PIFT").focus();
                    }
                }
            });

            $(".ATOF").change(function () {
                var a = $(this).val();
                if (a == '') {
                    a = "0";
                }
                var b = $(".ATOT").val();
                if (b == '') {
                    b = "0";
                }
                if (parseInt(b) > "0") {
                    if (parseInt(a) > parseInt(b)) {
                        jAlert('<strong>Last Annual Turn Over From should not be greater than Last Annual Turn Over To</strong>', 'SWP');
                        $(".ATOF").val('0');
                        $(".ATOF").focus();
                    }
                }
            });
            $(".ATOT").change(function () {
                var a = $(this).val();
                if (a == '') {
                    a = "0";
                }
                var b = $(".ATOF").val();
                if (b == '') {
                    b = "0";
                }
                if (parseInt(b) > "0") {
                    if (parseInt(a) < parseInt(b)) {
                        jAlert('<strong>Last Annual Turn Over To should not be less than Last Annual Turn Over From</strong>', 'SWP');
                        $(".ATOT").val('0');
                        $(".ATOT").focus();
                    }
                }
            });

            $('#ContentPlaceHolder1_btnSearch').click(function () {

                var dateFrom = $('.kk').val();
                var dateTo = $('.kk1').val();
                var StrMonth1 = new Array();
                StrMonth1["Jan"] = "01"; StrMonth1["Feb"] = "02"; StrMonth1["Mar"] = "03"; StrMonth1["Apr"] = "04"; StrMonth1["May"] = "05"; StrMonth1["Jun"] = "06"; StrMonth1["Jul"] = "07"; StrMonth1["Aug"] = "08"; StrMonth1["Sep"] = "09"; StrMonth1["Oct"] = "10"; StrMonth1["Nov"] = "11"; StrMonth1["Dec"] = "12";

                var splDateFrom = dateFrom.split("-");
                var fromDate = splDateFrom[2] + StrMonth1[splDateFrom[1]] + splDateFrom[0];
                var splDateTo = dateTo.split("-");
                var date_To = splDateTo[2] + StrMonth1[splDateTo[1]] + splDateTo[0];
                if ((fromDate != NaN) && (date_To != NaN)) {
                    if (fromDate > date_To) {
                        jAlert('<strong>Application date from should not be greater than application date to</strong>', 'SWP');
                        $('#' + date_To).focus();
                        return false;
                    }
                    
                }
            });
        });

      
        
        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'GSTINDET') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZ';
            }

            if (allow == 'OtherSpecify') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'Profit') {
                AllowableCharacters = '1234567890-.';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._-';
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
    </script>
    <style>
        /* Comments
---------------------------------- */
        .comments
        {
            margin-top: 60px;
        }
        .comments h2.title
        {
            margin-bottom: 40px;
            border-bottom: 1px solid #d2d2d2;
            padding-bottom: 10px;
        }
        .comment
        {
            font-size: 14px;
        }
        .comment .comment
        {
            margin-left: 75px;
        }
        .comment-avatar
        {
            margin-top: 5px;
            width: 55px;
            float: left;
        }
        .comment-content
        {
            border-bottom: 1px solid #d2d2d2;
            margin-bottom: 25px;
        }
        .comment h3
        {
            margin-top: 0;
            margin-bottom: 5px;
        }
        .comment-meta
        {
            margin-bottom: 15px;
            color: #999999;
            font-size: 12px;
        }
        .comment-meta a
        {
            color: #666666;
        }
        .comment-meta a:hover
        {
            text-decoration: underline;
        }
        .comment .btn
        {
            font-size: 12px;
            padding: 7px;
            min-width: 100px;
            margin-top: 5px;
            margin-bottom: -1px;
        }
        .btn-gray
        {
            color: #ffffff;
            background-color: #666666;
            border-color: #666666;
        }
        .comment .btn i
        {
            padding-right: 5px;
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
                  <h1>Proposal MIS Report</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Proposal</a></li><li><a>View</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        <section class="content">
      
               <div class="row">
                  <!-- Form controls -->
                 
                                              
                                                
                                          
                  <div class="col-sm-12">

               <div class="form-group row">
                                                    <div class="col-sm-4">
                                                        <label for="Country">
                                                            Project Type</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlProjrctType" runat="server"
                                                          >
                                                           <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Project Cost >= 50 crore" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Project cost upto < 50 crore" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    
                                                      <div class="col-sm-4">
                                                        <label for="Country">
                                                           District</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="16" ID="ddlDistrict" runat="server"
                                                          >
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                         <div class="col-sm-4" runat="server" id="st3">
                                                        <label for="State">
                                                            Status</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpStatus" runat="server">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                         <div class="col-sm-4" runat="server" id="Div1">
                                                        <label for="State">
                                                             Sector of activity</label>
                                                            <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control" AutoPostBack="true"
                                                TabIndex="5" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                                    </div>
                                                      <div class="col-sm-4" runat="server" id="Div2">
                                                        <label for="State">
                                                             Sub sector</label>
                                                               <asp:DropDownList ID="ddlSubSector" runat="server" CssClass="form-control" TabIndex="6">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                         
                                                    </div>
                                                    <div class="col-sm-4" runat="server" id="Div3">
                                                        <label for="State">
                                                            Priority Sector</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="17" ID="ddlPrioritySector" runat="server">
                                                             <asp:ListItem Value="3">---Select---</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                      <div class="col-sm-4">
                                 <label>Application Date From</label>
                                 <div class="input-group  date datePicker" id="datePicker1">
                                        <asp:TextBox ID="txtApplicationfrom" Onkeypress="return inputLimiter(event,'DateFormat')"  CssClass="form-control kk" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                
                                          </div>
                                <div class="col-sm-4">
                                          <label>Application Date To</label>
                                     <div class="input-group  date datePicker" >
                                        <asp:TextBox ID="txtApplicationTo" Onkeypress="return inputLimiter(event,'DateFormat')" CssClass="form-control kk1" runat="server"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                   
                                 </div>
                               <div class="col-sm-4">
                                 <label>Proposed Employment From</label>
                               <%--  <div class="input-group">--%>
                                        <asp:TextBox ID="txtFromdate" MaxLength="4"  CssClass="form-control ap" Onkeypress="return inputLimiter(event,'Numbers')" runat="server"></asp:TextBox>
                                     
                                  <%--  </div>--%>
                                
                                          </div>
                                              <div class="col-sm-4">
                                 <label>Proposed Employment To</label>
                                 <%--<div class="input-group">--%>
                                        <asp:TextBox ID="txtEmployemntTo" MaxLength="4"  CssClass="form-control at" Onkeypress="return inputLimiter(event,'Numbers')" runat="server"></asp:TextBox>
                                     
                                 <%--   </div>--%>
                                
                                          </div>
                                <div class="col-sm-4">
                                          <label>Proposed Capital Investment (INR in Lakhs) From</label>
                                     <%--<div class="input-group" >--%>
                                        <asp:TextBox ID="txtAmount" MaxLength="10" CssClass="form-control PIF" Onkeypress="return inputLimiter(event,'Numbers')" runat="server"></asp:TextBox>
                                     
                                   <%-- </div>--%>
                                   
                                 </div>
                                  <div class="col-sm-4">
                                          <label>Proposed Capital Investment (INR in Lakhs) To</label>
                                 <%--    <div class="input-group" >--%>
                                        <asp:TextBox ID="txtProposedTo" MaxLength="10" CssClass="form-control PIFT" Onkeypress="return inputLimiter(event,'Numbers')" runat="server"></asp:TextBox>
                                     
                                   <%-- </div>--%>
                                   
                                 </div>
                                  
                                                     <div class="col-sm-4" runat="server" id="Div5">
                                                        <label for="State">
                                                            Query Status</label>
                                                        <asp:DropDownList CssClass="form-control" TabIndex="17" ID="drpQueryStatus" runat="server">
                                                             <asp:ListItem Value="0">---All---</asp:ListItem>
                                                <asp:ListItem Value="1">1st Query Raised</asp:ListItem>
                                                <asp:ListItem Value="2">1st Query Response</asp:ListItem>
                                                 <asp:ListItem Value="3">2nd Query Raised</asp:ListItem>
                                                <asp:ListItem Value="4">2nd Query Response</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                      <div class="col-sm-4">
                                          <label>Last Annual Turn Over From</label>
                                 
                                        <asp:TextBox ID="txtAnnualturnOverFrom" MaxLength="10" CssClass="form-control ATOF" Onkeypress="return inputLimiter(event,'Numbers')" runat="server"></asp:TextBox>
                                     
                                 
                                   
                                 </div>
                                  <div class="col-sm-4">
                                          <label>Last Annual Turn Over To</label>
                                 
                                        <asp:TextBox ID="txtAnnualturnOverTo" MaxLength="10" CssClass="form-control ATOT" Onkeypress="return inputLimiter(event,'Numbers')" runat="server"></asp:TextBox>
                                     
                                 
                                   
                                 </div>
                                   <div class="col-sm-4">
                                          <label> Land Required From Government</label>
                                 
                                     <asp:RadioButtonList ID="rdnLandRqd" CssClass="form-control" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                               
                                              >
                                                <asp:ListItem class="radio-inline" Text="Yes" Value="1" />
                                                <asp:ListItem class="radio-inline" Text="No" Value="0" />
                                            </asp:RadioButtonList>
                                     
                                 
                                   
                                 </div>
                                                     <div class="col-sm-4">
                                                       
                                                     <asp:Button ID="btnSearch" style="margin-top:30px" 
                                                             CssClass="btn btn btn-add btn-sm" runat="server" Text="Search" 
                                                             onclick="btnSearch_Click"></asp:Button>

                                                    </div>
                                                </div>


                     <div class="panel panel-bd lobidisable">
                     
                        <div class="panel-body">
                             <div align="right" >
                                    <asp:LinkButton ID="lbtnAll" runat="server" Visible="false" CssClass="" Text="All"   OnClick="lbtnAll_Click"
                                  ></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblPaging" runat="server"></asp:Label>
                                </div>
                            <div class="table-responsive">
                            

                                <asp:GridView ID="gvService" class="table table-bordered table-hover" runat="server"
                                    AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" OnPageIndexChanging="gvService_PageIndexChanging"
                                    Width="100%" EmptyDataText="No Record(s) Found..."
                                    DataKeyNames="intProposalId,intActionToBeTakenBy,strFileName,intQueryStatus,strQueryStatus" PageSize="10" OnRowDataBound="gvService_RowDataBound">
                                    <Columns>


                                        <asp:BoundField HeaderText=" Sl#" />
                                        <asp:TemplateField HeaderText="Proposal No">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hypLink" runat="server" NavigateUrl="ProposalDetails.aspx" Text='<%# Eval("strFileName") %>'></asp:HyperLink>
                                                <asp:HiddenField ID="hdnTextVal1" runat="server" Value='<%# Eval("strFileName")%>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="decAmount" HeaderText="Industry Type" />
                                        <asp:BoundField DataField="strRemarks" HeaderText="Name Of Company/Enterprises" />

                                        <asp:BoundField DataField="strAppliedDistBlock" HeaderText="District" />
                                        <asp:BoundField DataField="intForwardIDCO" HeaderText="Total Proposed Employment" />
                                        <asp:BoundField DataField="compName" HeaderText="Capital Investment" />
                                        <asp:BoundField DataField="ActionAuthority" HeaderText="Annual Turn Over" />
                                        <asp:BoundField DataField="strIdcoDocs" HeaderText="Sector" />
                                        <asp:BoundField DataField="strFrom" HeaderText="Sub Sector" />
                                        <asp:BoundField DataField="strDeptSMSContent" HeaderText="Priority Sector" />
                                        <asp:BoundField DataField="EmailBody" HeaderText="Land Required" />
                                        <asp:BoundField DataField="dtmCreatedOn" HeaderText="Application Date" />
                                        <%--<asp:TemplateField HeaderText="Status" >
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_status" Text='<%# Eval("strStatus") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="strStatus" HeaderText="Status" />




                                    </Columns>
                                </asp:GridView>
                                       
                           
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
              
            </section>
        <!-- /.content -->
    </div>
    
</asp:Content>
