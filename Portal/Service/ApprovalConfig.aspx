<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="ApprovalConfig.aspx.cs"
    Inherits="Service_ApprovalConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Console/scripts/ajax.js" type="text/javascript"></script>
    <script src="../Console/scripts/AjaxScript.js" type="text/javascript"></script>


    <%--<link href="../css/jQuery.alert.css" rel="stylesheet" type="text/css" media="screen" />--%>
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
                  <h1>Approval Configuration</h1>
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
                              <a class="btn btn-add " href="ApprovalConfig.aspx"> 
                              <i class="fa fa-file"></i>View Approval Configuration</a>  
                           </div>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                            <div class="form-group">
                                <div class="row">
                                 <div class="col-sm-12">
                                
                                 <label>Department</label>
                                   <asp:HiddenField ID="hdnRowid" runat="server"></asp:HiddenField>
                                      <asp:HiddenField ID="hdnHirarchyText" runat="server"></asp:HiddenField>
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                 <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control dpt" Width="350px" onselectedindexchanged="ddldept_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                  </ContentTemplate>
                                  
                                 </asp:UpdatePanel>
                                 </div>
                                 </div>
                                <div class="row">
                               <div class="col-sm-12">
                                 <label>Service Sector</label>
                                  <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                 <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control ser" Width="350px" 
                                            AutoPostBack="True" onselectedindexchanged="ddlService_SelectedIndexChanged"></asp:DropDownList>
                                  </ContentTemplate>
                                  <triggers>
                                    <asp:asyncpostbacktrigger controlid="ddldept"  />
                                   
                                    </triggers>
                                 </asp:UpdatePanel>
                                 </div>
                                 <div class="col-sm-12">
                                    <label>Type</label>
                                    <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" 
                                         Width="350px" onselectedindexchanged="ddltype_SelectedIndexChanged"   AutoPostBack="True" >
                                                   <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                      <asp:ListItem Text="Large" Value="1"></asp:ListItem>
                                                         <asp:ListItem Text="MSME" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="More Than 100cr" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                 </div>
                                 </div>
                                 <div>
                                 
                                 </div>
                              </div>
                              <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                              <asp:GridView ID="gvService" runat="server" 
                                    class="table table-bordered table-hover" 
                                    onrowdatabound="gvService_RowDataBound" AutoGenerateColumns="false" onrowcommand="gvService_RowCommand" 
                                            >
                                <Columns>




                                    <asp:TemplateField HeaderText="Stage No">
                                                    <ItemTemplate>
                                                         <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px"/>
                                               </asp:TemplateField> 
                                                  <asp:TemplateField HeaderText="Level">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("intEscalationId") %>'></asp:Label>
                                                        <%--<%# Container.DataItemIndex + 1 %>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Forword To">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtForward" runat="server" Class="forword" Height="60px" Width="350px" ReadOnly="True" Text='<%# Eval("strUsername") %>'></asp:TextBox>
                                                           <%-- <asp:ImageButton ID="ImgbtnFwd" runat="server"  data-toggle="modal" data-target="#customer1"
                                                                 ImageUrl="~/images/ghhytht.jpg"  Height="40px" Width="40px" 
                                                                 />--%>
                                                            <%-- <img id="myImg" src="~/images/ghhytht.jpg" alt="Trolltunga, Norway" width="40" height="40" runat="server" data-toggle="modal" data-target="#customer1">--%>
                                                            <asp:HiddenField ID="hdnforarduserid" runat="server" Value='<%# Eval("userid") %>'></asp:HiddenField>
                                                             <asp:HiddenField ID="hdndesig" runat="server" Value='<%# Eval("desigid") %>'></asp:HiddenField>
                                                               <asp:HiddenField ID="hdnLoc" runat="server" Value='<%# Eval("LOCATIONID") %>'></asp:HiddenField>
                                                               <asp:HiddenField ID="hdnDept" runat="server" Value='<%# Eval("DEPTID") %>'></asp:HiddenField>
                                                               <asp:HiddenField ID="hdnDirectorate" runat="server" Value='<%# Eval("DirectId") %>'></asp:HiddenField>
                                                               <asp:HiddenField ID="hdnDiv" runat="server" Value='<%# Eval("DivisionId") %>'></asp:HiddenField>
                                                               <asp:HiddenField ID="hdnDist" runat="server" Value='<%# Eval("DISTID") %>'></asp:HiddenField>
                                                            <asp:HiddenField ID="hdntype" runat="server" Value='<%# Eval("Typeid") %>'></asp:HiddenField>

                                                           <%-- <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary" data-toggle="modal" data-target="#customer1"><i class="fa fa-filter"></i></asp:LinkButton>--%>

                                                            <button type="button" class="btn btn-primary" onclick="setvaluesOfrow(this);" data-toggle="modal" data-target= '<%# "#" + Eval("intEscalationId")%>'><i class="fa fa-filter"></i></button>

                                                            <div class="modal fade modalid" tabindex="-1" id='<%#Eval("intEscalationId")%>' role="dialog" aria-hidden="true">

                                                            <div class="modal-dialog modal-lg" runat="server" id="Modal">
                     <div class="modal-content" >
                        <div class="modal-header modal-header-primary">
                           <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                           <h3><i class="fa fa-user m-r-5"></i> Take Action</h3>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                              <div class="col-md-12">   
                      <div class="panel panel-bd ">
                        <div class="panel-heading">
                        Take Action
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
                                 <div class="form-group showdept" runat="server">
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
                                <div class="form-group showdire" runat="server" >
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
                                <div class="form-group showdivi" runat="server">
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
                                <div class="form-group showdist" runat="server">
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
                                    <div id="Div1" class="form-group showdesig" runat="server">
                                    <div class="row">
                                        <label class="col-md-2">Select Designation</label>
                                              <div class="col-md-6"> 
                                                <asp:DropDownList ID="ddldiv" runat="server" CssClass="form-control desig"  ></asp:DropDownList>
                                              </div>
                                    </div>
                                </div>
                                 <%--<div class="form-group">
                                    <div class="row">
                                        <label class="col-md-2">Select User</label>
                                              <div class="col-md-6"> 
                                          
                                              <asp:DropDownList ID="ddluser" runat="server" CssClass="form-control user" ></asp:DropDownList>
                                       
                                              </div>
                                    </div>
                                </div>--%>

                         <br />
                         <div class="col-md-10 col-sm-offset-2 form-group user-form-group">
                                          <div class="row">
                                          
                                           <asp:Button ID="btnsubmit" runat="server" Text="Save" class="btn btn-add btn-sm btnsubmit" OnClientClick="return Validation();"
                                                   onclick="btnsubmit_Click"></asp:Button>
                                   
                                             <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancel</button>
                                            <asp:HiddenField ID="HiddenFieldID" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hdnESCALATIONLEVEL" runat="server"></asp:HiddenField>
                                            
                                          </div>
                                       </div>

                            <div class="clearfix"></div>

                                       </div>
                                     

                         </div>
                        </div>




                                  
                                       <!-- Text input-->
                                      
                                   
                              </div>
                           </div>
                        <div class="modal-footer">
                           <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Close</button>
                        </div>
                        </div>
                     </div>

                                                            </div>

                                                    </ItemTemplate> 
                                                </asp:TemplateField>
                                               

                                                <asp:TemplateField HeaderText="TimeLine In Days">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTimeline" runat="server" CssClass="form-control time" Text='<%# Eval("strExcalationDays") %>' ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnAdd" runat="server" 
                                                            ImageUrl="~/Console/images/plus-4-xxl.png" Height="20px" Width="20px" onclick="imgbtnAdd_Click" CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" 
                                                            ImageUrl="~/Console/images/delete_img.png" Height="20px" Width="20px" onclick="imgbtnDelete_Click" CausesValidation="false" CommandName="Delete"
                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <%-- <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />--%>
                                </Columns>
                                </asp:GridView>
                                 </ContentTemplate>
                                <triggers>
                                   <asp:asyncpostbacktrigger controlid="ddlService"  />
                                    <asp:asyncpostbacktrigger controlid="ddltype"  />
                                    </triggers>
   
                                 </asp:UpdatePanel>
                           </div>
                        </div>

                        <asp:Button ID="btnsave" runat="server" Text="Save" class="btn btn-add btn-sm save" OnClientClick="return Validation123();" 
                                                   onclick="btnsave_Click"></asp:Button>
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
        function setvaluesOfrow(flu) {
            var a = flu.offsetParent.parentNode.rowIndex;
            var rows;
            rows = a - 1
           // alert(rows);
            document.getElementById('ContentPlaceHolder1_hdnRowid').value = rows;
            $(".location").val('0');
            $(".showdept").hide();
            $(".showdire").hide();
            $(".showdivi").hide();
            $(".showdist").hide();
        }
        function pageLoad(sender, args) {
           
            $('.btnsubmit').click(function () {
                $('.modalid').modal('hide');
            });
            $(".showdept").hide();
            $(".showdire").hide();
            $(".showdivi").hide();
            $(".showdist").hide();
            $('#ContentPlaceHolder1_hdnHirarchyText').val('');
            $.ajax({
                type: "POST",
                url: "ApprovalConfig.aspx/FillDesignation",                
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $(".desig").empty().append('<option selected="selected" value="0">-Select-</option>');
                    $.each(r.d, function () {
                        $(".desig").append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });
            $(".location").change(function () {
                //                 debugger;
                //                 alert($(this).val());
                          
                var a = $(this).val();
                if (a == "0") {
                    //$('#ContentPlaceHolder1_hdnHirarchyText').val('');   
                    $(".showdept").hide();
                }
                else {
                    $('#ContentPlaceHolder1_hdnHirarchyText').val($(this).find('option:selected').text());
                    $(".showdept").show();
                    $.ajax({
                        type: "POST",
                        url: "ApprovalConfig.aspx/FillDepartment",
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



//                $.ajax({
//                    type: "POST",
//                    url: "ApprovalConfig.aspx/FillUser",
//                    data: "{'id':" + $(this).val() + "}",
//                    contentType: "application/json; charset=utf-8",
//                    dataType: "json",
//                    success: function (r) {
//                        $(".user").empty().append('<option selected="selected" value="0">-Select-</option>');
//                        $.each(r.d, function () {
//                            $(".user").append($("<option></option>").val(this['Value']).html(this['Text']));
//                        });
//                    }
//                });
            });


            //Directorate

            $(".dept").change(function () {
               
               
                var b = $(this).val();
                if (b == "0") {
                    $(".showdire").hide();
                  //  $('#ContentPlaceHolder1_hdnHirarchyText').val('');

                }
                else {
                    $(".showdire").show();
                    $('#ContentPlaceHolder1_hdnHirarchyText').val($(this).find('option:selected').text());
                    $.ajax({
                        type: "POST",
                        url: "ApprovalConfig.aspx/FillDirectorate",
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


//                $.ajax({
//                    type: "POST",
//                    url: "ApprovalConfig.aspx/FillUser",
//                    data: "{'id':" + $(this).val() + "}",
//                    contentType: "application/json; charset=utf-8",
//                    dataType: "json",
//                    success: function (r) {
//                        $(".user").empty().append('<option selected="selected" value="0">-Select-</option>');
//                        $.each(r.d, function () {
//                            $(".user").append($("<option></option>").val(this['Value']).html(this['Text']));
//                        });
//                    }
//                });
            });

            //Division
            $(".dire").change(function () {
              //  $('#ContentPlaceHolder1_hdnHirarchyText').val('');
             
                var c = $(this).val();
                if (c == '0') {
                    $(".showdivi").hide();
                }
                else {
                    $(".showdivi").show();
                    $('#ContentPlaceHolder1_hdnHirarchyText').val($(this).find('option:selected').text());
                }

                $.ajax({
                    type: "POST",
                    url: "ApprovalConfig.aspx/FillDivision",
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

//                $.ajax({
//                    type: "POST",
//                    url: "ApprovalConfig.aspx/FillUser",
//                    data: "{'id':" + $(this).val() + "}",
//                    contentType: "application/json; charset=utf-8",
//                    dataType: "json",
//                    success: function (r) {
//                        $(".user").empty().append('<option selected="selected" value="0">-Select-</option>');
//                        $.each(r.d, function () {
//                            $(".user").append($("<option></option>").val(this['Value']).html(this['Text']));
//                        });
//                    }
//                });
            });

            //District
            $(".divi").change(function () {
               // $('#ContentPlaceHolder1_hdnHirarchyText').val('');
             
                var d = $(this).val();
                if (d == '0') {
                    $(".showdist").hide();
                }
                else {
                    $(".showdist").show();
                    $('#ContentPlaceHolder1_hdnHirarchyText').val($(this).find('option:selected').text());
                }

                $.ajax({
                    type: "POST",
                    url: "ApprovalConfig.aspx/FillDistrict",
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

//                $.ajax({
//                    type: "POST",
//                    url: "ApprovalConfig.aspx/FillUser",
//                    data: "{'id':" + $(this).val() + "}",
//                    contentType: "application/json; charset=utf-8",
//                    dataType: "json",
//                    success: function (r) {
//                        $(".user").empty().append('<option selected="selected" value="0">-Select-</option>');
//                        $.each(r.d, function () {
//                            $(".user").append($("<option></option>").val(this['Value']).html(this['Text']));
//                        });
//                    }
//                });
            });
            $(".desig").change(function () {
               
                if ($(this).val() != "0") {
                    $('#ContentPlaceHolder1_hdnHirarchyText').val($('#ContentPlaceHolder1_hdnHirarchyText').val() + "/" + $(this).find('option:selected').text());
                }
            });
            //fill User bY District
            $(".dist").change(function () {
               // $('#ContentPlaceHolder1_hdnHirarchyText').val('');
                if ($(this).val() != "0") {
                    $('#ContentPlaceHolder1_hdnHirarchyText').val($(this).find('option:selected').text());
                }
                $.ajax({
                    type: "POST",
                    url: "ApprovalConfig.aspx/FillUser",
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


                if (!chkSACgrd()) {
                    return false;
                }
                return true;
            });

        });


        function chkSACgrd() {//Validate GridView Data
            debugger;
            var gridView = document.getElementById("<%=gvService.ClientID %>");
            var a = gridView.getElementsByTagName("tr");
            var counts = a.length;
            var rows;
            for (var i = 0; i < counts; i++) {


                if ($('#ContentPlaceHolder1_gvService_txtForward_' + i).val() == "") {
                    alert("Please select User !");
                    return false;
                }

                if ($('#ContentPlaceHolder1_gvService_txtTimeline_' + i).val() == "") {
                    alert("Please select TimeLine Day !");
                    return false;
                }
              
                if ($('#ContentPlaceHolder1_ddlService').find('option:selected').text().trim() == "PEAL") {
                    if ($('#ContentPlaceHolder1_gvService_ddltype_' + i).val() == "0") {
                        alert("Please select Type !");
                        $('#ContentPlaceHolder1_gvService_ddltype_' + i).focus();
                        return false;
                    }
                }

            }
            return true;
        }

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
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
--%>
