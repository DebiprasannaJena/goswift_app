
Partial Class includes_header
    Inherits System.Web.UI.UserControl


    Protected Sub lnkBOdia_Click(sender As Object, e As System.EventArgs) Handles lnkBOdia.Click

        'Response.Redirect("Default.aspx?lang=or-IN")
        'Dim RawUrls As String = Request.RawUrl.ToString()
        'If Not Request.QueryString("lang") = Nothing Then
        '    RawUrls = RawUrls.Replace("?lang=en-US", "?lang=or-IN")
        'Else
        '    RawUrls = RawUrls & "?lang=or-IN"

        'End If
        Session("language") = "or-IN"
        Response.Redirect(Request.RawUrl)
    End Sub

    Protected Sub lnkBeng_Click(sender As Object, e As System.EventArgs) Handles lnkBeng.Click

        Session("language") = "en-US"
        Response.Redirect(Request.RawUrl)
        'Dim RawUrls As String = Request.RawUrl.ToString()
        'If Not Request.QueryString("lang") = Nothing Then
        '    RawUrls = RawUrls.Replace("?lang=or-IN", "?lang=en-US")
        'Else
        '    RawUrls = RawUrls & "?lang=en-US"
        'End If

        'Response.Redirect(RawUrls)
    End Sub
End Class
