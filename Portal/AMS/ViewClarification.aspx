<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewClarification.aspx.cs"
    Inherits="SingleWindow_ViewClarification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/custom.js" type="text/javascript"></script>
    <link href="../css/agenda.css" rel="stylesheet" type="text/css" />
    <style>
        .cmd-sec
        {
            background-color: #f1efef;
            margin-bottom: 5px;
            padding: 5px;
            border-radius: 3px;
        }
        sub, sup
        {
            font-size: 55%;
        }
        .text-blue
        {
            color: #08c;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <table class="table table-bordered" id="tblFeedback">
            <tr>
                <td>
                    <asp:GridView ID="grdComments" runat="server" Width="100%" AutoGenerateColumns="False"
                        OnRowCancelingEdit="grdComments_RowCancelingEdit" OnRowEditing="grdComments_RowEditing"
                        OnRowUpdating="grdComments_RowUpdating" DataKeyNames="INTFEEDBACKID,intTp" OnRowDataBound="grdComments_OnRowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SLFC Memebrs">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("VCH_FULLNAME") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SLFC Memebrs Comment">
                                <ItemTemplate>
                                    <asp:Label ID="lblComment" runat="server" Text='<%# Bind("VCHCOMMENT") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modified By GM(SLNA)">
                                <ItemTemplate>
                                    <asp:Label ID="lblFeedback" runat="server" 
                                        Text='<%# Bind("VCH_COMMENT_GMSLNA") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFeedback" runat="server" 
                                        Text='<%# Bind("VCH_COMMENT_GMSLNA") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" />                            
                        </Columns>
                        <FooterStyle Font-Bold="True" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="grdClarification" runat="server" Width="100%" AutoGenerateColumns="False"
                        ShowFooter="true" GridLines="None" OnRowCancelingEdit="grdClarification_RowCancelingEdit" 
                        OnRowEditing="grdClarification_RowEditing" OnRowDataBound="grdClarification_OnRowDataBound"
                        OnRowUpdating="grdClarification_RowUpdating"  DataKeyNames="intSendId,intTp">
                       
                        <Columns>
                         
                             <asp:TemplateField HeaderText="SLFC Memebrs">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("vchFullName") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SLFC Memebrs Clarification">
                                <ItemTemplate>
                                    <asp:Label ID="lblComment" runat="server" Text='<%# Bind("vchClarificationSLFC") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modified By GM(SLNA)">
                                <ItemTemplate>
                                    <asp:Label ID="lblClarification" runat="server" 
                                        Text='<%# Bind("vchClarificationGM") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtClarification" runat="server" 
                                        Text='<%# Bind("vchClarificationGM") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                              <asp:CommandField ShowEditButton="True" />      
                        </Columns>
                        <FooterStyle Font-Bold="True" />
                    </asp:GridView>
                </td>
            </tr>
        </table>        
    </div>
    </form>
</body>
</html>
