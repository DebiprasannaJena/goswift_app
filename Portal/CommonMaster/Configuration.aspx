<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="Configuration.aspx.cs" Inherits="SWP_Configuration_Configuration"
    EnableEventValidation="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
           
            //select the column name in Hidden field text
            $('#right').click(function () {

                var options = $("[id*=lstLeft] option:selected");

                for (var i = 0; i < options.length; i++) {


                    var opt = $(options[i]).clone();

                    $(options[i]).remove();
                    $("[id*=FirstRight]").append(opt);
                }
                //                    else {
                //                        alert($(options[i]).val() + " is not allowed to Data Field");
                //                    }


                var hiddenTextField = '';
                $('#FirstRight option').each(function () {

                    if (hiddenTextField.length > 0) {
                        hiddenTextField = hiddenTextField + "," + $(this).val();
                    }
                    else {
                        hiddenTextField = $(this).val();
                    }
                });
                $('#HiddentxtSelectedColumn').val(hiddenTextField);
            });
        });




        $(document).ready(function () {

            //select the column name in Hidden field text
            $('#left').click(function () {

                var options = $("[id*=FirstRight] option:selected");

                for (var i = 0; i < options.length; i++) {


                    var opt = $(options[i]).clone();

                    $(options[i]).remove();
                    $("[id*=lstLeft]").append(opt);
                }
                //                    else {
                //                        alert($(options[i]).val() + " is not allowed to Data Field");
                //                    }


                var hiddenTextField = '';
                $('#lstLeft option').each(function () {

                    if (hiddenTextField.length > 0) {
                        hiddenTextField = hiddenTextField + "," + $(this).val();
                    }
                    else {
                        hiddenTextField = $(this).val();
                    }
                });
                $('#HiddentxtSelectedColumn').val(hiddenTextField);

                var varHidden = "";
                $("#selectId > option").each(function () {
                    varHidden = this.text + "_" + varHidden;
                    alert(this.text + ' ' + this.value);
                });
            });
        });


        //function to fill the names in listbox
        //        function CallWMtoGetUsersIdName(txtLike) {
        //            //            document.getElementById("divImg").style.display = "block";
        //            var txtVal = txtLike.value;
        //            if (txtVal != null || txtVal != 'lstLeft') {

        //                //            PageMethods.GetUsers(param, GetUsersIdName);
        //                PageMethods.GetUsers(txtVal, GetUsersIdName);
        //            }
        //            else {
        //                //PageMethods.GetUsers("0", GetUsersIdName);
        //                PageMethods.GetUsers(txtVal, GetUsersIdName);
        //            }
        //        }

    </script>
   
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <input type="hidden" id="hdntextid" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Approval Authority</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Mange Approval Authority</a></li><li><a>Config Approval Authority</a></li></ul>
               </div>
            </section>
                <!-- Main content -->
                <section class="content">
               <div class="row">
                  <!-- Form controls -->
                  <div class="col-sm-12">
                     <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">  
                        </div>
                        <div class="panel-body">
                         
                           

                           <div class="search-sec">
                        <div class="form-group">
                        <div class="row">
                        <label class="col-md-2 col-sm-3"> Select Service *</label>
                         <div class="col-md-3 col-sm-3"><span class="colon">:</span>
                           <asp:DropDownList ID="ddlSrvice" CssClass="form-control" Width="250px" runat="server" 
                                                    >
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Service 1" Value="1" />
                                                    <asp:ListItem Text="Service 2" Value="2" />
                                                    <asp:ListItem Text="Service 3" Value="3" />
                                                </asp:DropDownList>
                                               
                                                 </div>
                                                  
                          
                          </div>
                          </div>
                           <div class="form-group">
                        
                        </div>
                           </div>
                            <div class="table-responsive">
                              <asp:GridView ID="gvService" runat="server" class="table table-bordered table-striped table-hover"
                                            AutoGenerateColumns="False"  EmptyDataText="No Record(s) Found" CellPadding="4"
                                            ForeColor="#333333" GridLines="None" 
                                    onrowdatabound="gvService_RowDataBound"  >
                                            
                                            <Columns>
                                           
                                               <asp:TemplateField HeaderText="Stage No">
                                                    <ItemTemplate>
                                                         <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px"/>
                                               </asp:TemplateField> 
                                                  <asp:TemplateField HeaderText="From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("formname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Forword To">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtForward" runat="server" Height="60px" Width="400px"></asp:TextBox>
                                                        <asp:ImageButton ID="ImgbtnFwd" runat="server"  data-toggle="modal" data-target="#sector1" 
                                                             ImageUrl="~/images/ghhytht.jpg"  Height="40px" Width="40px" OnClientClick="Assign(this);"
                                                             />
                                                    </ItemTemplate> 
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="TimeLine In Days">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTimeline" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Add/Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnAdd" runat="server" 
                                                            ImageUrl="~/Console/images/plus-4-xxl.png" Height="20px" Width="20px" onclick="ImageButton1_Click" CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                            </Columns>                                            
                                        </asp:GridView>
                                 
                           
                        </div>
                       
                     </div>
                     <!-- /.modal-content -->
                  </div>
                  <!-- /.modal-dialog -->
               </div>
                                        <div class="reset-button">
                                 <a href="#" class="btn btn-success">Submit</a>
                                 <a href="#" class="btn btn-danger">Reset</a>
                              </div>

                           </div>
            </div>
            </div> </div> </div>
            <div class="modal fade" id="sector1" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header modal-header-primary">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                            <h3>
                                <i class="fa fa-user m-r-5"></i>Update Service</h3>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <form class="form-horizontal">
                                    <fieldset>
                                        <!-- Text input-->
                                        <div class="col-md-4 form-group">
                                            <div class="search-sec">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-md-8 ">
                                                            Serch By Name
                                                        </label>
                                                        <div class="col-md-2 col-sm-3">
                                                            <asp:TextBox ID="txtNameLike" runat="server"> </asp:TextBox>
                                                        </div>
                                                        <%-- <div id="divImg" runat="server" style="display: none; float: left; width: auto; margin-left: 5px;">
                                                                <asp:Image ID="imgProgress" runat="server" ImageUrl="~/images/1.jpg" />
                                                                   
                                                            </div>--%>
                                                        <%--  <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:ListBox ID="lstApproveEmployee" runat="server" SelectionMode="Multiple" Height="90px"
                                                                        Width="220px">
                                                                        <asp:ListItem Text="Name1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Name2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Name3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Name4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="Name5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="Name6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="Name7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="Name8" Value="8"></asp:ListItem>
                                                                    </asp:ListBox>
                                                                    <span class="mandatory">*</span>
                                                                </td>
                                                                <td>
                                                                    <input type="button" class="btn btn-sm btn-success" name="SubmitA2" value="&gt;&gt;"
                                                                        onclick="jsRepoLstBoxAddParticipant();" />
                                                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-sm btn-danger" OnClientClick="return jsRemoveFromList('form1','lstEmployee','lstApproveEmployee');"
                                                                        Style="cursor: pointer" Text="<<" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                    <asp:ListBox ID="lstEmployee" runat="server" SelectionMode="Multiple" Height="90px"
                                                                        Width="280px"></asp:ListBox>
                                                                    <span class="mandatory">*</span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:HiddenField ID="hdnAprvdUser" runat="server" />
                                                        <asp:HiddenField ID="HdnUserName" runat="server" />--%>
                                                        <div class="row" style="padding-top: 10px;">
                                                            <div class="col-lg-3">
                                                                <asp:ListBox ID="lstLeft" class="form-control" runat="server" SelectionMode="Multiple"
                                                                    Height="90px" Width="220px">
                                                                    <%--onclick="ListBoxClient_SelectionChanged(this, event);">--%>
                                                                    <asp:ListItem Value="StoreID">StoreID</asp:ListItem>
                                                                    <asp:ListItem Value="ItemLookupCode">ItemLookupCode</asp:ListItem>
                                                                    <asp:ListItem Value="ExtendedDescription">ExtendedDescription</asp:ListItem>
                                                                    <asp:ListItem Value="SubDescription1">SubDescription1</asp:ListItem>
                                                                    <asp:ListItem Value="DepartmentName">DepartmentName</asp:ListItem>
                                                                    <asp:ListItem Value="CategoryName">CategoryName</asp:ListItem>
                                                                    <asp:ListItem Value="SupplierCode">SupplierCode</asp:ListItem>
                                                                    <asp:ListItem Value="SupplierName">SupplierName</asp:ListItem>
                                                                    <asp:ListItem Value="TotalQuantity">TotalQuantity</asp:ListItem>
                                                                    <asp:ListItem Value="ExtendedPrice">ExtendedPrice</asp:ListItem>
                                                                    <asp:ListItem Value="ExtendedCost">ExtendedCost</asp:ListItem>
                                                                    <asp:ListItem Value="Profit">Profit</asp:ListItem>
                                                                    <asp:ListItem Value="UnitOfMeasure">UnitOfMeasure</asp:ListItem>
                                                                    <asp:ListItem Value="CustomerAccountNumber">CustomerAccountNumber</asp:ListItem>
                                                                    <asp:ListItem Value="CustomerName">CustomerName</asp:ListItem>
                                                                </asp:ListBox>
                                                            </div>
                                                            <div class="col-lg-1" style="padding-top: 70px; padding-left: 30px;">
                                                                <input type="button" id="right" value=">>" />
                                                                <input type="button" id="left" value="<<" />
                                                            </div>
                                                            <%--Data Field ListBox--%>
                                                            <div class="col-lg-2">
                                                                <asp:ListBox ID="FirstRight" runat="server" SelectionMode="Multiple" Height="90px"
                                                                    Width="280px"></asp:ListBox>
                                                                <asp:HiddenField ID="HiddentxtSelectedColumn" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Text input-->
                                            <%--  <div class="col-md-4 form-group">
                                            <label class="control-label">
                                                Sector Name:</label>
                                            <input type="text" placeholder="Sector Name" class="form-control">
                                        </div>
                                        <!-- Text input-->
                                        <div class="col-md-4 form-group">
                                            <label class="control-label">
                                                Description:</label>
                                            <textarea name="Description" rows="3"></textarea>
                                        </div>--%>
                                            <div class="col-md-12 form-group user-form-group">
                                                <div class="pull-right">
                                                    <asp:Button class="btn btn-add btn-sm" runat="server" Text="Save" ID="btnSave" OnClientClick="buttonclick();">
                                                    </asp:Button>
                                                    <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">
                                                        Cancel</button>
                                                </div>
                                            </div>
                                    </fieldset>
                                    </form>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger pull-left" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            </section>
            <!-- /.content -->
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
