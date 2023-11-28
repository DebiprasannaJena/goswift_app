<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_Inct_FY.ascx.cs" Inherits="incentives_WUC_Inct_FY" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false"
    class="table table-bordered table-hover" EmptyDataText="No Record Found !!" >
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:RadioButton ID="RadBtn_FY" runat="server" />
                <asp:HiddenField ID="Hid_FY" runat="server" Value='<%# Eval("fy") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="Lbl_Start_Date" runat="server" Text='<%# Eval("fy_start_date") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="Lbl_End_Date" runat="server" Text='<%# Eval("fy_end_date") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
