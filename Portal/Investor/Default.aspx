<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Application.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Portal_Investor_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script>
    function Test() {
        jAlert('<strong>Please enter the correct code. !</strong>','SWP');
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<a href="#" onclick="return Test();">Test</a>
</asp:Content>

