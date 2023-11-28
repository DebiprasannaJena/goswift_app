<%--'*******************************************************************************************************************
' File Name         : HolidayMaster.aspx
' Description       : This page is to create holiday 
' Created by        : Dharmasis sahoo
' Created On        : 06th Dec 2021
' Modification History:
                                                           
'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                    

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Application.master"
    CodeFile="HolidayMaster.aspx.cs" Inherits="SWP_HolidayMaster_HolidayMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/WebValidation.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*--------------------------------------------------------------*/

        function ValidateAtSaveBtn() {
            if (blankFieldValidation('ContentPlaceHolder1_Txt_Holiday_Title', 'Holiday title', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Holiday_Title").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_Txt_Holiday_Title', 'Holiday title', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Holiday_Title").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_Txt_Holiday_From', 'Holiday from date', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Holiday_From").focus();
                return false;
            };
            if (blankFieldValidation('ContentPlaceHolder1_Txt_Holiday_To', 'Holiday to date', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Holiday_To").focus();
                return false;
            };

            //alert(Date(from));
            <%--var a = $("#<%=Txt_Holiday_From.ClientID%>").val();
            var b = Date.parse($("#<%=Txt_Holiday_From.ClientID%>").val());
            var c = Date.parse($("#<%=Txt_Holiday_To.ClientID%>").val());
            alert(b);
            alert(c);

            var from =$("#Txt_Holiday_From").val();
            var to = $("#Txt_Holiday_To").val();--%>
            if (Date.parse($("#<%=Txt_Holiday_From.ClientID%>").val()) > Date.parse($("#<%=Txt_Holiday_To.ClientID%>").val())) {
                jAlert('<strong>From date should not be greater than To date.</strong>', projname);
                $("#ContentPlaceHolder1_Txt_Holiday_From").focus();
                return false;
            };

            if (blankFieldValidation('ContentPlaceHolder1_Txt_Holiday_Description', 'Holiday description', projname) == false) {
                $("#ContentPlaceHolder1_Txt_Holiday_Description").focus();
                return false;
            };

        }
    </script>


    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <input type="hidden" id="hdntextid" />

    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="header-icon">
                <i class="fa fa-dashboard"></i>
            </div>
            <div class="header-title">
                <h1>Holiday Configuration</h1>
                <ul class="breadcrumb">
                    <li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li>
                    <li><a>Configuration Setting</a></li>
                    <li><a>Holiday Config</a></li>
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

                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Holiday Title</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Holiday_Title" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Holiday From</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <div class="input-group  date datePicker" id="datePicker1">
                                                <asp:TextBox ID="Txt_Holiday_From" CssClass="form-control" runat="server" AutoCompleteType="None"
                                                    autoComplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Holiday To</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <div class="input-group  date datePicker" id="datePicker2">
                                                <asp:TextBox ID="Txt_Holiday_To" CssClass="form-control" runat="server" AutoCompleteType="None"
                                                    autoComplete="off"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>

                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2">
                                            Description</label>
                                        <div class="col-sm-4">
                                            <span class="colon">:</span>
                                            <asp:TextBox ID="Txt_Holiday_Description" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-2"></div>
                                            <div class="col-sm-8">
                                                <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-success" OnClientClick="return ValidateAtSaveBtn();" Text="Save" OnClick="BtnSave_Click" />
                                                <asp:Button runat="server" ID="BtnReset" Text="Reset" OnClick="BtnReset_Click" CssClass="btn btn-danger" />
                                            </div>
                                        </div>
                                    </div>

                                    <%--<%--<div class="table-responsive">
                                        <asp:DetailsView ID="DetailsView1" runat="server"
                                            class="table table-bordered table-striped table-hover" Width="100%"
                                            Height="50px" AutoGenerateRows="False"
                                            DataKeyNames="HolidayId" DataSourceID="SQLHoliday" DefaultMode="Insert"
                                            OnItemInserted="DetailsView1_ItemInserted">
                                            <Fields>
                                                <asp:TemplateField HeaderText="Holiday Title" SortExpression="HolidayTitle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("HolidayTitle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" Text='<%# Bind("HolidayTitle") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <InsertItemTemplate>
                                                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" Text='<%# Bind("HolidayTitle") %>'></asp:TextBox>
                                                    </InsertItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Holiday Form" SortExpression="HolidayForm">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("HolidayForm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <div class="input-group  date datePicker" id="datePicker1">
                                                            <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" Text='<%# Bind("HolidayForm") %>'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <InsertItemTemplate>
                                                        <div class="input-group  date datePicker" id="datePicker2">
                                                            <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" Text='<%# Bind("HolidayForm") %>'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </InsertItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Holiday To" SortExpression="HolidayTo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("HolidayTo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <div class="input-group  date datePicker" id="datePicker3">
                                                            <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" Text='<%# Bind("HolidayTo") %>'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <InsertItemTemplate>
                                                        <div class="input-group  date datePicker" id="datePicker4">
                                                            <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" Text='<%# Bind("HolidayTo") %>'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </InsertItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                        <br />
                                                        <asp:Button ID="lnkUpdate" runat="server" CssClass="btn btn-success" CausesValidation="True" CommandName="Update" Text="Update" />
                                                        &nbsp; 
                                                        <asp:HyperLink ID="HyperLink1" CssClass="btn btn-danger" NavigateUrl="~/Portal/CommonMaster/HolidayView.aspx" runat="server">Cancel</asp:HyperLink>
                                                        <%--<asp:Button ID="lnkUpdateCancel" runat="server" CssClass="btn btn-danger" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                    </EditItemTemplate>
                                                    <InsertItemTemplate>
                                                        <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                        <br />
                                                        <asp:Button ID="LinkButton1" runat="server" CssClass="btn btn-success" CausesValidation="True" CommandName="Insert" Text="Save" />
                                                        &nbsp;<asp:Button ID="LinkButton2" runat="server" CssClass="btn btn-danger" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                                    </InsertItemTemplate>

                                                </asp:TemplateField>
                                            </Fields>
                                        </asp:DetailsView>
                                        <asp:SqlDataSource ID="SQLHoliday" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:AdminAppConnectionProd %>"
                                            SelectCommand="SELECT HolidayId, HolidayTitle, int_LocationId, HolidayForm, HolidayTo, Description, Type, dtUpdateDate, intUpdatedBy, intDeletedFlag FROM HolidayMaster WHERE (HolidayId = @Param1)"
                                            DeleteCommand="DELETE FROM [HolidayMaster] WHERE [HolidayId] = @HolidayId"
                                            InsertCommand="INSERT INTO [HolidayMaster] ( [HolidayTitle], [HolidayForm], [HolidayTo], [Description]) VALUES (@HolidayTitle, @HolidayForm, @HolidayTo, @Description)"
                                            UpdateCommand="UPDATE [HolidayMaster] SET [HolidayTitle] = @HolidayTitle, [HolidayForm] = @HolidayForm, [HolidayTo] = @HolidayTo, [Description] = @Description WHERE [HolidayId] = @HolidayId">
                                            <DeleteParameters>
                                                <asp:Parameter Name="HolidayId" Type="Int32" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:Parameter Name="HolidayTitle" Type="String" />
                                                <asp:Parameter DbType="Date" Name="HolidayForm" />
                                                <asp:Parameter DbType="Date" Name="HolidayTo" />
                                                <asp:Parameter Name="Description" Type="String" />
                                            </InsertParameters>
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="Param1" QueryStringField="id" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="HolidayTitle" Type="String" />
                                                <asp:Parameter DbType="Date" Name="HolidayForm" />
                                                <asp:Parameter DbType="Date" Name="HolidayTo" />
                                                <asp:Parameter Name="Description" Type="String" />
                                                <asp:Parameter Name="HolidayId" Type="Int32" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                    </div>--%>
                                </ContentTemplate>
                                <%--<Triggers>
                                    <asp:PostBackTrigger ControlID="BtnSave" />
                                </Triggers>--%>
                            </asp:UpdatePanel>

                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>


        </section>
    </div>
    <!-- /.content -->


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
                $('.datePicker').datepicker({
                    format: 'dd-M-yyyy',
                    autoclose: true

                });
            }
        });


    </script>
</asp:Content>
