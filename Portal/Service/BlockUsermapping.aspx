<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Application.master"
    CodeFile="BlockUsermapping.aspx.cs" Inherits="Portal_Service_BlockUsermapping" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Console/scripts/ajax.js" type="text/javascript"></script>
    <script src="../Console/scripts/ajax.js" type="text/javascript"></script>
    <script src="../Console/scripts/AjaxScript.js" type="text/javascript"></script>
    <script src="../js/jQuery.alert.js" type="text/javascript"></script>
    <link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />
    <%-- <script src="../js/WebValidation.js" type="text/javascript"></script>--%>
    <script src="../js/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        
    </script>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Block User Configuration</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Service</a></li><li><a>Approval Configuration</a></li></ul>
               </div>
        </section>
        <!-- Main content -->
        <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist" > 
                              <a class="btn btn-add " href="BlockUsermapping.aspx"> 
                              <i class="fa fa-file"></i>Block User Configuration</a>  
                           </div>
                        </div>
                       <div class="panel-body">

                                 <br>
                                 </br>

                                 <div class="form-group">
                                    <div class="row">
                                        <label class="col-md-2">Select Location</label>
                                              <div class="col-md-6"> 
                                              <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                <ContentTemplate>--%>
                                             <%-- <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" 
                                                        AutoPostBack="True" onselectedindexchanged="ddlLocation_SelectedIndexChanged" ></asp:DropDownList>--%>
                                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control location" ></asp:DropDownList>
                                                         <asp:HiddenField ID="hdnlocation" runat="server" ></asp:HiddenField>
                                            <%--  </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                              </div>
                                    </div>
                                </div>
                                 <div id="Div1" class="form-group showdept" runat="server">
                                    <div class="row">
                                        <label class="col-md-2">Select Department</label>
                                              <div class="col-md-6"> 
                                            <%--    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                <ContentTemplate>--%>
                                              <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control dept"  ></asp:DropDownList>
                                                <%--</ContentTemplate>
                                                 <triggers>
                                                    <asp:asyncpostbacktrigger controlid="ddlLocation"  />
                                                 </triggers>
                                            </asp:UpdatePanel>--%>
                                              </div>
                                    </div>
                                </div>
                                <div id="Div2" class="form-group showdire" runat="server" >
                                    <div class="row">
                                        <label class="col-md-2">Select Directorate</label>
                                              <div class="col-md-6"> 
                                               <%-- <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                <ContentTemplate>--%>
                                              <asp:DropDownList ID="ddldirectorate" runat="server" CssClass="form-control dire" ></asp:DropDownList>
                                             <%-- </ContentTemplate>
                                                 <triggers>
                                                    <asp:asyncpostbacktrigger controlid="ddlDepartment"  />
                                                 </triggers>
                                            </asp:UpdatePanel>--%>
                                              </div>
                                    </div>
                                </div>
                                <div id="Div3" class="form-group showdivi" runat="server">
                                    <div class="row">
                                        <label class="col-md-2">Select Division</label>
                                              <div class="col-md-6"> 
                                               <%-- <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                <ContentTemplate>--%>
                                              <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control divi" 
                                                         ></asp:DropDownList>
                                                <%--</ContentTemplate>
                                                 <triggers>
                                                    <asp:asyncpostbacktrigger controlid="ddldirectorate"  />
                                                 </triggers>
                                            </asp:UpdatePanel>--%>
                                              </div>
                                    </div>
                                </div>
                                <div id="Div4" class="form-group showdist" runat="server">
                                    <div class="row">
                                        <label class="col-md-2">Select District</label>
                                              <div class="col-md-6"> 
                                                <%-- <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                <ContentTemplate>--%>
                                              <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control dist"  ></asp:DropDownList>
                                             <%--  </ContentTemplate>
                                                 <triggers>
                                                    <asp:asyncpostbacktrigger controlid="ddlDivision"  />
                                                 </triggers>
                                            </asp:UpdatePanel>--%>
                                              </div>
                                    </div>
                                </div>
                                    <div id="Div5" class="form-group showdesig" runat="server">
                                    <div class="row">
                                        <label class="col-md-2">Select Designation</label>
                                              <div class="col-md-6"> 
                                                <asp:DropDownList ID="ddldiv" runat="server" CssClass="form-control desig"  ></asp:DropDownList>
                                              </div>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <div class="row">
                                        <label class="col-md-2">Select User</label>
                                              <div class="col-md-6"> 
                                          
                                              <asp:DropDownList ID="ddluser" runat="server" CssClass="form-control user" ></asp:DropDownList>
                                       
                                              </div>
                                    </div>
                                </div>
                                   <div class="form-group">
                                    <div class="row">
                                        <label class="col-md-2">District</label>
                                              <div class="col-md-6"> 
                                                  <asp:DropDownList ID="ddldist" runat="server" CssClass="form-control adddist" 
                                                       ></asp:DropDownList>
                                               </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                  <asp:UpdatePanel runat="server" ID="UpdatePannel1">
                                            <ContentTemplate>
                                    <div class="row">
                                        <label class="col-md-2">Block</label>
                                               <div class="col-sm-3">
                                                <asp:ListBox ID = "lstBlock" runat="server" CssClass="form-control addblock"  SelectionMode="Multiple" Width="250px">
                                                    </asp:ListBox>
                                                    </div>
                                                      <div class="col-sm-3"style="width:80px">
                                                        <asp:Button ID="btnAdd" runat="server" Text=">>"  class="btn btn-add btn-sm Add"  ></asp:Button>
                                                         <asp:Button ID="btnRemove" runat="server" Text="<<" class="btn btn-danger btn-sm remove" ></asp:Button>
                                                      </div>
                                                      
                                                     <div class="col-sm-3">
                                                     <%--  <asp:UpdatePanel ID="UpdateAddblock" runat="server">
                                                <ContentTemplate>--%>
                                                     <asp:ListBox ID = "lstBlockadd" runat="server" CssClass="form-control block" Width="250px"  SelectionMode="Multiple">
                                                    </asp:ListBox>
                                                       <%-- </ContentTemplate>
                                                 <triggers>
                                                 
                                                    <asp:PostBackTrigger controlid="btnAdd"  />
                                                 </triggers>
                                            </asp:UpdatePanel>--%>
                                               </div>
                                    </div>
                                    </ContentTemplate>
                                        </asp:UpdatePanel>
                                </div>

                         <br />
                         <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                          <div class="row">
                                          
                                           <asp:Button ID="btnsubmit" runat="server" Text="Save" 
                                                  class="btn btn-add btn-sm btnsubmit" OnClientClick="return Validation();" onclick="btnsubmit_Click"
                                                   ></asp:Button>
                                   
                                             <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancel</button>
                                            <asp:HiddenField ID="HiddenFieldID" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdnESCALATIONLEVEL" runat="server"></asp:HiddenField>
                                             <asp:HiddenField ID="hdnUser" runat="server" ></asp:HiddenField>
                                                <asp:HiddenField ID="hdnDist" runat="server" ></asp:HiddenField>
                                             <asp:HiddenField ID="hdnBlock" runat="server" ></asp:HiddenField>
                                          </div>
                                       </div>

                            <div class="clearfix"></div>

                                       </div>

            <%--            <asp:Button ID="btnsave" runat="server" Text="Save" class="btn btn-add btn-sm save" OnClientClick="return Validation123();" 
                                                   onclick="btnsave_Click"></asp:Button>--%>
                                                   <asp:Label ID="lblconfigid" runat="server" Text="Label" Visible="False"></asp:Label>
<%--
                        <asp:Button ID="btnupdate" runat="server" Text="Update" 
                             class="btn btn-add btn-sm" onclick="btnupdate_Click" 
                                            ></asp:Button>--%>
                     </div>
                  </div>
               </div>
                  <!-- customer Modal1 -->

                <div class="modal fade" id="customer1" tabindex="-1" role="dialog" aria-hidden="true">
                  
                     <!-- /.modal-content -->
                  </div>
                        
                 
                  <!-- /.modal-dialog -->
                   
            <!-- /.modal -->
            </section>
        <!-- /.content -->
    </div>
    <script type="text/javascript">

        
        function addValue() {
            var i = 0;
            var inHTML;
            $("#ContentPlaceHolder1_lstBlock option:selected").each(function () {
                var optionVal = $(this).val();
                var exists = false;
                $('#ContentPlaceHolder1_lstBlockadd option').each(function () {
                    if (this.value == optionVal) {
                        exists = true;
                    }
                });

                if (!exists) {
                    inHTML += '<option value="' + $(this).val() + '">' + $(this).text() + '</option>';
                }
            });
            $("#ContentPlaceHolder1_lstBlockadd").append(inHTML);
            return true;
        }
        function pageLoad(sender, args) {

            $('.btnsubmit').click(function () {
                var strblock = '';
                $('#ContentPlaceHolder1_lstBlockadd option').each(function () {
                    strblock = this.value+',';
                });
                document.getElementById('ContentPlaceHolder1_hdnBlock').value = strblock;
                document.getElementById('ContentPlaceHolder1_hdnDist').value = strblock;
                document.getElementById('ContentPlaceHolder1_hdnUser').value = strblock;
            });
            $('.Add').click(function () {
                var inHTML;
                $("#ContentPlaceHolder1_lstBlock option:selected").each(function () {
                    var optionVal = $(this).val();
                    alert($(this).val());
                    var exists = false;
                    $('#ContentPlaceHolder1_lstBlockadd option').each(function () {
                        if (this.value == optionVal) {
                            exists = true;
                        }
                    });

                    if (!exists) {
                        inHTML += '<option value="' + $(this).val() + '">' + $(this).text() + '</option>';
                        // $(".block").append($("<option></option>").val($(this).val()).html($(this).text()));
                    }
                });
                $("#ContentPlaceHolder1_lstBlockadd").append(inHTML);

                return false;
            });
            $('.remove').click(function () {
                $("#ContentPlaceHolder1_lstBlockadd option:selected").remove();
                return false;
            });
            $('.btnsubmit').click(function () {
                $('.modalid').modal('hide');
            });
            $(".showdept").hide();
            $(".showdire").hide();
            $(".showdivi").hide();
            $(".showdist").hide();

            $.ajax({
                type: "POST",
                url: "BlockUsermapping.aspx/FillDesignation",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $(".desig").empty().append('<option selected="selected" value="0">-Select-</option>');
                    $.each(r.d, function () {
                        $(".desig").append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });
            $(".adddist").change(function () {
                var ad = $(this).val();
                if (ad != "0") {
                    $.ajax({
                        type: "POST",
                        url: "BlockUsermapping.aspx/FillBlock",
                        data: "{'id':" + $(this).val() + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            var i = 0;
                            $(".addblock").empty().append('<option selected="selected" value="0">-Select-</option>');
                            $.each(r.d, function () {
                                if (i < r.d.length) {

                                    $(".addblock").append($("<option></option>").val(r.d[i]['intBlockId']).html(r.d[i]['vchBlockName']));
                                    i = i + 1;
                                }
                            });
                        }
                    });
                }
            });


            $(".location").change(function () {
                //                 debugger;
                //                 alert($(this).val());

                var a = $(this).val();
                if (a == "0") {

                    $(".showdept").hide();
                    $(".showdire").hide();
                    $(".showdivi").hide();
                    $(".showdist").hide();
                }
                else {

                    $(".showdept").show();
                    $.ajax({
                        type: "POST",
                        url: "BlockUsermapping.aspx/FillDepartment",
                        data: "{'id':" + $(this).val() + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            $(".dept").empty().append('<option selected="selected" value="0">-Select-</option>');
                            $.each(r.d, function () {
                                $(".dept").append($("<option></option>").val(this['Value']).html(this['Text']));
                            });
                        }
                    });
                }



                $.ajax({
                    type: "POST",
                    url: "BlockUsermapping.aspx/FillUser",
                    data: "{'id':" + $(this).val() + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $(".user").empty().append('<option selected="selected" value="0">-Select-</option>');
                        $.each(r.d, function () {
                            $(".user").append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    }
                });
            });


            //Directorate

            $(".dept").change(function () {


                var b = $(this).val();
                if (b == "0") {
                    $(".showdire").hide();


                }
                else {
                    $(".showdire").show();

                    $.ajax({
                        type: "POST",
                        url: "BlockUsermapping.aspx/FillDirectorate",
                        data: "{'id':" + $(this).val() + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            $(".dire").empty().append('<option selected="selected" value="0">-Select-</option>');
                            $.each(r.d, function () {
                                $(".dire").append($("<option></option>").val(this['Value']).html(this['Text']));
                            });
                        }
                    });
                }


                $.ajax({
                    type: "POST",
                    url: "BlockUsermapping.aspx/FillUser",
                    data: "{'id':" + $(this).val() + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $(".user").empty().append('<option selected="selected" value="0">-Select-</option>');
                        $.each(r.d, function () {
                            $(".user").append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    }
                });
            });

            //Division
            $(".dire").change(function () {


                var c = $(this).val();
                if (c == '0') {
                    $(".showdivi").hide();
                }
                else {
                    $(".showdivi").show();

                }

                $.ajax({
                    type: "POST",
                    url: "BlockUsermapping.aspx/FillDivision",
                    data: "{'id':" + $(this).val() + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $(".divi").empty().append('<option selected="selected" value="0">-Select-</option>');
                        $.each(r.d, function () {
                            $(".divi").append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "BlockUsermapping.aspx/FillUser",
                    data: "{'id':" + $(this).val() + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $(".user").empty().append('<option selected="selected" value="0">-Select-</option>');
                        $.each(r.d, function () {
                            $(".user").append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    }
                });
            });

            //District
            $(".divi").change(function () {


                var d = $(this).val();
                if (d == '0') {
                    $(".showdist").hide();
                }
                else {
                    $(".showdist").show();

                }

                $.ajax({
                    type: "POST",
                    url: "BlockUsermapping.aspx/FillDistrict",
                    data: "{'id':" + $(this).val() + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $(".dist").empty().append('<option selected="selected" value="0">-Select-</option>');
                        $.each(r.d, function () {
                            $(".dist").append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "BlockUsermapping.aspx/FillUser",
                    data: "{'id':" + $(this).val() + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $(".user").empty().append('<option selected="selected" value="0">-Select-</option>');
                        $.each(r.d, function () {
                            $(".user").append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    }
                });
            });

            //fill User bY District
            $(".dist").change(function () {

                if ($(this).val() != "0") {

                }
                $.ajax({
                    type: "POST",
                    url: "BlockUsermapping.aspx/FillUser",
                    data: "{'id':" + $(this).val() + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $(".user").empty().append('<option selected="selected" value="0">-Select-</option>');
                        $.each(r.d, function () {
                            $(".user").append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    }
                });
            });

        }

        $(function () {
            $(".save").click(function () {
                var ddldept = $(".dpt");
                var ddlService = $(".ser");
                var ddlLocation = $(".location");

                if (ddldept.val() == "0") {
                    alert("Please select Department !");
                    return false;
                }

                if (ddlService.val() == "0") {
                    alert("Please select Service !");
                    return false;
                }


                return true;
            });

        });




        function Validation() {
            //                     $(".btnsubmit").click(function () {
            //                         var ddlLocation = $(".location");
            //                         // var ddluser = $(".user");
            //                         if (ddlLocation.val() == "0") {
            //                             alert("Please select Location !");
            //                             return false;
            //                         }
            //                         else {
            //                             //                         if (ddluser.val() == "0") {
            //                             //                             alert("Please select User !");
            //                             //                             return false;
            //                             //                         }
            //                             return true;
            //                         }
            //                     });
        }

    </script>
</asp:Content>
