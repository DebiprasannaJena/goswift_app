<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IncludeScript.ascx.cs"
    Inherits="includes_IncludeScript" %>
<meta http-equiv="Cache-control" content="public">
<link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />

<link href="../css/bootstrap-overrides.css" rel="stylesheet" type="text/css" />
<link href="../css/portal.css" rel="stylesheet" type="text/css" />
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<link href="../css/datepicker.css" rel="stylesheet" type="text/css" />

<script src="../js/jquery.js" type="text/javascript"></script>
<script src="../js/bootstrap.min.js" type="text/javascript" language="javascript"></script>
<script src="../js/loadComponent.js" language="javascript" type="text/javascript"></script>
<script src="../js/Validator.js" type="text/javascript" language="javascript"></script>
<script src="../js/bootstrap-datepicker.js" type="text/javascript" charset="UTF-8"></script>
<script src="../js/custom.js" type="text/javascript" language="javascript"></script>
<%--<script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.3.12/angular.min.js"></script>
<script type="text/javascript" src="/scripts/moment.min.js"></script>--%>

<script type="text/javascript">
    var leftgLink = '<%=strGL%>';
    var leftpLink = '<%=strPL%>';
    var leftg = '<%=linkm%>';
    var leftp = '<%=linkn%>';
    var RandomNo = '<%=Session["RandomNo"]%>'
    var PL=<%="'"+HttpUtility.UrlEncode(String.Format("{0}",Request.QueryString["linkn"]))+"'" %>;
    var GL=<% ="'"+ HttpUtility.UrlEncode(String.Format("{0}",Request.QueryString["linkm"])) +"'"%>;
</script>
