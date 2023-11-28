<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master"
    AutoEventWireup="true" CodeFile="ServiceMaster.aspx.cs" Inherits="Portal_Service_ServiceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>
     <%--<script language="javascript" type="text/javascript">

         function Navigate() {
             var cc = $('#ContentPlaceHolder1_TextBox2').val();
             if (cc == '') {
                 alert("Service Name is Blank");
                 return false;
             }
         }

        $(document).ready(function () {
            $("#ContentPlaceHolder1_Payment").hide();
            $("#<%=rdbPayment.ClientID%> input").change(function () {
                if (($(this).val()) == 0) {
                    $("#ContentPlaceHolder1_Payment").show();
                }
                else {
                    $("#ContentPlaceHolder1_Payment").hide();

                }
            });
        });

       
      
     </script> --%>
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
    
             <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <div class="header-icon">
                        <i class="fa fa-dashboard"></i>
                    </div>
                    <div class="header-title">
                        <h1>Manage Service</h1>
                        <ul class="breadcrumb">
                            <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                            <li><a>Services</a></li>
                            <li><a>Edit Service</a></li>
                        </ul>


                    </div>
                </section>
                <!-- Main content -->
                <section class="content">
                    <%-- <section class="search-sec">--%>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-bd lobidrag">
                                <div class="panel-heading">
                                    <div class="btn-group buttonlist">
                                        <a class="btn btn-add " href="AddServiceMaster.aspx">
                                            <i class="fa fa-plus"></i>Add </a>
                                    </div>
                                    <div class="btn-group buttonlist">
                                        <a class="btn btn-add " href="ViewServiceMaster.aspx">
                                            <i class="fa fa-file"></i>View </a>
                                    </div>
                                    <div class="btn-group buttonlist">
                                        <a class="btn btn-add " href="ServiceMaster.aspx">
                                            <i class="fa fa-plus"></i>Edit Service Name </a>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="search-sec">
                                        <div class="form-group row">
                                            <div class="col-sm-3">
                                                <strong>Department Name :</strong>

                                            </div>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlDepartment" CssClass="form-control" Width="60%" runat="server"></asp:DropDownList>
                                            </div>
                                    
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-sm-9 text-center">
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-success"
                                                    Text="Search" OnClick="Button1_Click"></asp:Button>
                                            </div>
                                       </div>
                                    </div>



                                    <%--    </section>--%>


                                    <div class="table-responsive">
                           
                               
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False"  
                                        OnRowEditing="GridView1_RowEditing"  OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                        OnRowUpdating="GridView1_RowUpdating">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No" >
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="4%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department Name" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDeptName" runat="server" Text='<%# Eval("nvchLevelName") %>'></asp:Label>
                                                    <asp:HiddenField ID="Hid_intDepId" runat="server" Value='<%# Eval("INT_DEPARTMENT_ID") %>' />
                                            
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service Name">
                                                <ItemTemplate>
                                                    
                                                    <asp:Label ID="lblServiceName" runat="server" Text='<%# Eval("VCH_SERVICENAME") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="Hid_ServicedId" runat="server" Value='<%# Eval("INT_SERVICEID") %>' />
                                                    <asp:TextBox ID="txtServiceName" CssClass="form-control" Width="100%" TextMode="MultiLine" runat="server" Text='<%# Eval("VCH_SERVICENAME") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategoryType" runat="server" Text='<%# Eval("VCH_CategoryType") %>'></asp:Label>
                                                   
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlCategory" SelectedValue='<%# Eval("INT_CategoryType") %>' runat="server" CssClass="form-control" Width="100%">
                                                        <asp:ListItem Text="Pre-Establishment" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Pre-Operation" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ShowHeader="False" HeaderText="Action" ItemStyle-Width="10%">
                                              
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-add btn-sm" CausesValidation="False"
                                                    CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                           <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success btn-sm" runat="server" CausesValidation="False"
                                                    CommandName="Update"  Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" CssClass="btn btn-danger btn-sm" runat="server" CausesValidation="False"
                                                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                                        </EditItemTemplate>
                                            </asp:TemplateField>

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
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
