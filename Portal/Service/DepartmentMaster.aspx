<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="DepartmentMaster.aspx.cs" Inherits="Portal_Service_DepartmentMaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <script language="javascript" type="text/javascript">
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

       
       
    </script>--%>
     <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
               <div class="header-icon">
                  <i class="fa fa-dashboard"></i>
               </div>
               <div class="header-title">
                  <h1>Manage Departments</h1>
                  <ul class="breadcrumb"><li><a href="javscript:void(0)"><i class="fa fa-home"></i></a></li><li><a>Mange Departments</a></li></ul>
               </div>
            </section>
        <!-- Main content -->
        
        <section class="content">
        
               <div class="row">
                
                  <!-- Form controls -->
<asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" 
                       DataKeyNames="intLevelDetailId" DataSourceID="SQLService" 
                      >
    <Columns>
        <asp:TemplateField HeaderText="Sl No" SortExpression="intLevelDetailId">
            <ItemTemplate>
                <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# Bind("intLevelDetailId") %>'></asp:Label>
                <%# Container.DataItemIndex +1%>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="Label1" runat="server" Visible="false"  Text='<%# Eval("intLevelDetailId") %>'></asp:Label>
<%# Container.DataItemIndex +1%>

            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Department Name" SortExpression="nvchLevelName">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("nvchLevelName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" CssClass="form-control"  Width="100%" TextMode="MultiLine" runat="server" Text='<%# Bind("nvchLevelName") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ShowHeader="true" HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-add btn-sm" CausesValidation="False" 
                    CommandName="Edit" Text="Edit"></asp:LinkButton>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="True" 
                    CommandName="Update" Text="Update"></asp:LinkButton>
                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-danger btn-sm" CausesValidation="False" 
                    CommandName="Cancel" Text="Cancel"></asp:LinkButton>
            </EditItemTemplate>
        </asp:TemplateField>
    </Columns>
                   </asp:GridView>
                   <asp:SqlDataSource ID="SQLService" runat="server" 
                       ConnectionString="<%$ ConnectionStrings:AdminAppConnectionProd %>" 
                       SelectCommand="SELECT [nvchLevelName], [intLevelDetailId] FROM [M_ADM_LevelDetails] WHERE ([intLevelId] = @intLevelId)" 
                       
                       UpdateCommand="UPDATE [M_ADM_LevelDetails] SET [nvchLevelName] = @nvchLevelName WHERE [intLevelDetailId] = @original_intLevelDetailId" 
                       DeleteCommand="DELETE FROM [M_ADM_LevelDetails] WHERE [intLevelDetailId] = @original_intLevelDetailId" 
                       InsertCommand="INSERT INTO [M_ADM_LevelDetails] ([nvchLevelName], [intLevelDetailId]) VALUES (@nvchLevelName, @intLevelDetailId)" 
                       OldValuesParameterFormatString="original_{0}">
                       <DeleteParameters>
                           <asp:Parameter Name="original_intLevelDetailId" Type="Int32" />
                       </DeleteParameters>
                       <InsertParameters>
                           <asp:Parameter Name="nvchLevelName" Type="String" />
                           <asp:Parameter Name="intLevelDetailId" Type="Int32" />
                       </InsertParameters>
                       <SelectParameters>
                           <asp:Parameter DefaultValue="2" Name="intLevelId" Type="Int32" />
                       </SelectParameters>
                       <UpdateParameters>
                           <asp:Parameter Name="nvchLevelName" Type="String" />
                           <asp:Parameter Name="original_intLevelDetailId" Type="Int32" />
                       </UpdateParameters>
                   </asp:SqlDataSource>
               </div>
            </section>
        <!-- /.content -->
    </div>
</asp:Content>