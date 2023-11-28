<%--'*******************************************************************************************************************
' File Name         : HolidayView.aspx
' Description       : This page is to view all holiday details 
' Created by        : Dharmasis sahoo
' Created On        : 06th Dec 2021
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="HolidayView.aspx.cs"
    Inherits="Portal_CommonMaster_HolidayView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datePicker').datepicker({ format: 'dd-M-yyyy',
              autoclose: true
            });
          }
        });
    </script>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*--------------------------------------------------------------*/

        function ValidateAtSaveBtn() {
            if (blankFieldValidation('ContentPlaceHolder1_TxtHolidayTitle', 'Holiday titlele', projname) == false) {
                $("#ContentPlaceHolder1_TxtHolidayTitle").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_TxtHolidayFrom', 'Holiday from dateee', projname) == false) {
                $("#ContentPlaceHolder1_TxtHolidayFrom").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_TxtHolidayTo', 'Holiday to dateee', projname) == false) {
                $("#ContentPlaceHolder1_TxtHolidayTo").focus();
                return false;
            };
               <%-- var a = Date.parse($("#<%=ContentPlaceHolder1_TxtHolidayFrom.ClientID%>").val()
                var b = Date.parse($("#<%=ContentPlaceHolder1_TxtHolidayTo.ClientID%>").val();
                if (Date.parse($("#<%=TxtHolidayFrom.ClientID%>").val()) > Date.parse($("#<%=ContentPlaceHolder1_TxtHolidayTo.ClientID%>").val())) {
                    jAlert('<strong>From date should not be greater than To date.</strong>', projname);
                    $("#ContentPlaceHolder1_Txt_Holiday_From").focus();
                    return false;
                };--%>


            if (blankFieldValidation('ContentPlaceHolder1_TxtDescription', 'Holiday descriptionnn', projname) == false) {
                $("#ContentPlaceHolder1_TxtDescription").focus();
                return false;
            };
        }
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
                <h1>View Holiday</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Configuration Setting</a></li>
                    <li><a>View Holiday</a></li>
                </ul>
            </div>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- Form controls -->
                <div class="col-sm-12">
                    <div class="panel panel-bd lobidisable">
                        <div class="panel-heading">
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="HolidayMaster.aspx"><i class="fa fa-plus"></i>Add
                                </a>
                            </div>
                            <div class="btn-group buttonlist">
                                <a class="btn btn-add " href="HolidayView.aspx"><i class="fa fa-file"></i>View
                                </a>
                            </div>
                        </div>
                        <div class="panel-body">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                                    <div class="search-sec">
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-2">
                                                    Select Year <span class="mandetory-y">*</span></label>
                                                <div class="col-sm-3">
                                                    <span class="colon">:</span>
                                                    <asp:DropDownList ID="DdlYear" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DdlYear_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="LblTotalHolidays" runat="server" CssClass="form-control-static" Font-Italic="true" ForeColor="#003399"></asp:Label>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="table-responsive">
                                        <asp:GridView ID="Gv_Holiday_Details" class="table table-bordered table-striped table-hover" runat="server" AutoGenerateColumns="False" DataKeyNames="HolidayId" OnRowEditing="Gv_Holiday_Details_RowEditing" OnRowCancelingEdit="Gv_Holiday_Details_RowCancelingEdit" OnRowUpdating="Gv_Holiday_Details_RowUpdating" OnRowDataBound="Gv_Holiday_Details_RowDataBound" ShowFooter="true" Width="100%">
                                            <%--DataSourceID="SQLHolidaySearch"--%>
                                            <Columns>
                                                <%-- <asp:TemplateField HeaderText="HolidayId" InsertVisible="False" Visible="false" SortExpression="HolidayId"></asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Sl No" InsertVisible="False" Visible="true" ItemStyle-Width="4%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblSlNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        <asp:HiddenField ID="hdnslno" Value='<%#Eval("HolidayId") %>' runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Holiday Title" SortExpression="HolidayTitle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblHolidayTitle" runat="server" Text='<%# Bind("HolidayTitle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TxtHolidayTitle" runat="server" Text='<%# Bind("HolidayTitle") %>' CssClass="form-control"></asp:TextBox>

                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Holiday Form" SortExpression="vchHolidayFrom" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblHolidayfrom" runat="server" Text='<%# Bind("vchHolidayFrom") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <div class="input-group  date datePicker" id="datePicker1">
                                                            <asp:TextBox ID="TxtHolidayFrom" ToolTip="Enter Date in DD-MMM-YYYY Format" placeholder="DD-MMM-YYYY"   CssClass="form-control" runat="server" AutoCompleteType="None" Text='<%# Bind("vchHolidayFrom") %>'
                                                                autoComplete="off"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Holiday To" SortExpression="vchHolidayTo" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblHolidayTo" runat="server" Text='<%# Bind("vchHolidayTo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>

                                                        <div class="input-group  date datePicker" id="datePicker1">
                                                            <asp:TextBox ID="TxtHolidayTo" ToolTip="Enter Date in DD-MMM-YYYY Format" placeholder="DD-MMM-YYYY"   CssClass="form-control" runat="server" AutoCompleteType="None" Text='<%# Bind("vchHolidayTo") %>'
                                                                autoComplete="off"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>

                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No Of Days" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblNoOfDays" runat="server" Text='<%# Bind("intNoOfDays") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdndays" Value='<%#Eval("intNoOfDays") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowEditButton="True" HeaderText="Edit" ItemStyle-Width="15%" ControlStyle-CssClass="btn btn-success" />
                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False" ItemStyle-Width="5%">

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="BtnDelete" CssClass="btn btn-danger" runat="server" CausesValidation="False" Text="Delete" OnClick="BtnDelete_Click" OnClientClick="return confirm('Are you sure to delete this record ?');"></asp:LinkButton>

                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                    <h4 style="color: red"><b>No Record(s) Available</b></h4>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <%-- <asp:SqlDataSource ID="SQLHolidaySearch" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:AdminAppConnectionProd %>"
                                            DeleteCommand="DELETE FROM [HolidayMaster] WHERE [HolidayId] = @HolidayId"
                                            InsertCommand="INSERT INTO [HolidayMaster] ([HolidayTitle], [int_LocationId], [HolidayForm], [HolidayTo], [Description], [Type], [dtUpdateDate], [intUpdatedBy], [intDeletedFlag]) VALUES (@HolidayTitle, @int_LocationId, @HolidayForm, @HolidayTo, @Description, @Type, @dtUpdateDate, @intUpdatedBy, @intDeletedFlag)"
                                            SelectCommand="SELECT * FROM [HolidayMaster] WHERE (YEAR([HolidayForm]) = @HolidayForm)"
                                            UpdateCommand="UPDATE [HolidayMaster] SET [HolidayTitle] = @HolidayTitle, [int_LocationId] = @int_LocationId, [HolidayForm] = @HolidayForm, [HolidayTo] = @HolidayTo, [Description] = @Description, [Type] = @Type, [dtUpdateDate] = @dtUpdateDate, [intUpdatedBy] = @intUpdatedBy, [intDeletedFlag] = @intDeletedFlag WHERE [HolidayId] = @HolidayId">
                                            <DeleteParameters>
                                                <asp:Parameter Name="HolidayId" Type="Int32" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:Parameter Name="HolidayTitle" Type="String" />
                                                <asp:Parameter Name="int_LocationId" Type="Int32" />
                                                <asp:Parameter DbType="Date" Name="HolidayForm" />
                                                <asp:Parameter DbType="Date" Name="HolidayTo" />
                                                <asp:Parameter Name="Description" Type="String" />
                                                <asp:Parameter Name="Type" Type="String" />
                                                <asp:Parameter Name="dtUpdateDate" Type="DateTime" />
                                                <asp:Parameter Name="intUpdatedBy" Type="Int32" />
                                                <asp:Parameter Name="intDeletedFlag" Type="Int32" />
                                            </InsertParameters>
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlYear" DbType="String" Name="HolidayForm"
                                                    PropertyName="SelectedValue" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="HolidayTitle" Type="String" />
                                                <asp:Parameter Name="int_LocationId" Type="Int32" />
                                                <asp:Parameter DbType="Date" Name="HolidayForm" />
                                                <asp:Parameter DbType="Date" Name="HolidayTo" />
                                                <asp:Parameter Name="Description" Type="String" />
                                                <asp:Parameter Name="Type" Type="String" />
                                                <asp:Parameter Name="dtUpdateDate" Type="DateTime" />
                                                <asp:Parameter Name="intUpdatedBy" Type="Int32" />
                                                <asp:Parameter Name="intDeletedFlag" Type="Int32" />
                                                <asp:Parameter Name="HolidayId" Type="Int32" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                    </div>--%>
                                    </div>

                                </ContentTemplate>

                            </asp:UpdatePanel>

                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                </div>
            </div>
        </section>
    </div>
    <!-- /.content -->

</asp:Content>
