'*******************************************************************************************************************
'' File Name             :   include_Tabbutton.ascx.cs
'' Description           :   To Create Button dynamically on a page on which this user control is register
'' Created by            :   Prabhu Prasad Mishra
'' Created On            :   28-May-2012
'' Modification History  :
''                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
''                              1                       15/10/2013          Mahesh Kumar Nayak          For URL encryption   
''                         
'' Function Name         :   GetAccessRights(),Createbutton(),Gettab()
'' Procedures Used       :   usp_Tab_Btn_Create 
'' User Defined Namespace:  OfficePortal.Kwantify, OfficePortal.ButtonTabCreation.ButtonTab,OfficePortal.ButtonTabCreation,OfficePortal
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports CSMPDK_3_0
Imports System.Collections.Generic
Partial Class tab
    Inherits System.Web.UI.UserControl
#Region "Variable"
    Private tempUser As Integer = 0
    Private glink As String, plink As String = Nothing
    Private btnURL As String, BtnId As String, BtnName As String = Nothing
    Public btn As String, tab As String = Nothing
    Private strBAccess As String
    ' Used to check the access level of user for the selected link and Button
    Private strTAccess As String
    ' Used to check the access level of user for the selected link and tab
    Public TestStr As String = "Test..."
    Private i As Integer = 0
    Private strTabaccess As String, strBtnaccess As String = Nothing
#End Region

    ''' <summary>
    ''' Function to Get the Plink access rights of a particular user.
    ''' </summary>
    ''' <param name="Action"></param>
    ''' <param name="Plinkid"></param>
    ''' <param name="Userid"></param>
    Private Sub GetAccessRights(ByVal Action As String, ByVal Plinkid As Integer, ByVal Userid As Integer)
        ''Dim objlst As IList(Of IButtonTabCreate) = New List(Of IButtonTabCreate)()
        ''objTabbtncreate.chr_ActionCode = Action
        ''objTabbtncreate.IntPlinkID = Plinkid
        ''objTabbtncreate.IntUserid = Userid
        ''objlst = objTabbtncreate.GetAccessRights(objTabbtncreate)
        ''For Each data As IButtonTabCreate In objlst
        ''    strTabaccess = data.vchTabAccess
        ''    strBtnaccess = data.vchBtnAccess
        ''Next
        Try
            Dim strPath As String = "~/UserXML/" + Session("UserId").ToString() + ".xml"
            Dim plinks = From plink In XElement.Load(Server.MapPath(strPath)).Elements("PLinkMaster") _
                         Where plink.Elements("INT_PLINK_ID").Value = Plinkid And plink.Elements("INT_USER_ID").Value = Userid _
                   Select New With _
                   {.permission = plink.Elements("CHR_PERMISSION").Value}
            If (plinks.Count > 0) Then
                For Each item In plinks
                    strTabaccess = item.permission
                    strBtnaccess = item.permission
                Next
            Else
                Throw New Exception("no access rights")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
    End Sub

    Private Function GetButtons(ByVal strAccess As String, ByVal intPlinkId As Integer) As DataTable
        Dim objDt As New DataTable
        Dim strAcs As String = ""
        Dim strField As String = ""
        If strAccess = "M" Then
            strAccess = "Y"
            strField = "CHR_MANAGE"
        ElseIf strAccess = "A" Then
            strAccess = "Y"
            strField = "CHR_ADD"
        ElseIf strAccess = "V" Then
            strAccess = "Y"
            strField = "CHR_VIEW"
        End If
        Dim strPath As String = "~/UserXML/" + Session("UserId").ToString() + ".xml"
        Dim pbtns = From pbtn In XElement.Load(Server.MapPath(strPath)).Elements("ButtonMaster") _
                     Where pbtn.Elements("INT_PLINK_ID").Value = intPlinkId _
                     And (pbtn.Elements(strField).Value = strAccess) _
                     Select New With _
                    {.btnId = pbtn.Elements("INT_BUTTON_ID").Value, _
        .btnName = pbtn.Elements("VCH_BUTTON_NAME").Value, _
        .url = pbtn.Elements("VCH_FILE_NAME").Value, _
        .tabAvail = pbtn.Elements("INT_TAB_AVAIL").Value _
        }
        objDt.Columns.Add("int_ButtonId")
        objDt.Columns.Add("nvch_Button")
        objDt.Columns.Add("vch_Url")
        objDt.Columns.Add("int_TabAvail")

        For Each item In pbtns
            Dim dtRow As DataRow = objDt.NewRow()
            dtRow(0) = item.btnId
            dtRow(1) = item.btnName
            dtRow(2) = item.url
            dtRow(3) = item.tabAvail
            objDt.Rows.Add(dtRow)
        Next
        Return objDt
    End Function
    Private Function GetTabs(ByVal strAccess As String, ByVal intBtnId As Integer) As DataTable
        Dim objDt As New DataTable
        Dim strField As String = ""

        If strAccess = "M" Then
            strAccess = "Y"
            strField = "CHR_MANAGE"
        ElseIf strAccess = "A" Then
            strAccess = "Y"
            strField = "CHR_ADD"
        ElseIf strAccess = "V" Then
            strAccess = "Y"
            strField = "CHR_VIEW"
        End If

        Dim strPath As String = "~/UserXML/" + Session("UserId").ToString() + ".xml"
        Dim ptabs = From ptab In XElement.Load(Server.MapPath(strPath)).Elements("TabMaster") _
                     Where ptab.Elements("INT_BUTTON_ID").Value = intBtnId _
                     And (ptab.Elements(strField).Value = strAccess) _
                     Select New With _
                    {.tabId = ptab.Elements("INT_TAB_ID").Value, _
        .tabName = ptab.Elements("VCH_TAB_NAME").Value, _
        .url = ptab.Elements("VCH_FILE_NAME").Value _
        }
        objDt.Columns.Add("int_TabId")
        objDt.Columns.Add("nvch_Tab")
        objDt.Columns.Add("vch_Url")

        For Each item In ptabs
            Dim dtRow As DataRow = objDt.NewRow()
            dtRow(0) = item.tabId
            dtRow(1) = item.tabName
            dtRow(2) = item.url
            objDt.Rows.Add(dtRow)
        Next
        Return objDt
    End Function
    ''' <summary>
    ''' Function To find the tab of a concern button depending on the tab access and set the tab URL.
    ''' </summary>
    ''' <param name="Action"></param>
    ''' <param name="Tabaccess"></param>
    ''' <param name="btnid"></param>
    ''' <returns></returns>
    Protected Function Gettab(ByVal Action As String, ByVal Tabaccess As String, ByVal btnid As Integer) As String
        Dim Dttab1 As DataTable = Nothing
        Dim inttabID As Integer = 0
        Dim strTabURL As String
        Dim strTabName As String = ""
        Try
            ''objTabbtncreate.chr_ActionCode = "T"
            ''objTabbtncreate.vchTabAccess = strTabaccess
            ''objTabbtncreate.intBtnid = Convert.ToInt32(btn)
            ''Dttab1 = objTabbtncreate.GetTab(objTabbtncreate)
            Dttab1 = GetTabs(strTabaccess, btnid)
            If Dttab1.Rows.Count > 0 Then
                For tabcnt As Integer = 0 To Dttab1.Rows.Count - 1
                    inttabID = Convert.ToInt32(Dttab1.Rows(tabcnt).ItemArray(0))
                    If Dttab1.Rows(tabcnt).ItemArray(2).ToString().Contains("?") Then
                        strTabURL = Dttab1.Rows(tabcnt).ItemArray(2).ToString() & "&linkm=" & Admin.CommonFunction.CommonFunction.EncryptData(glink) & "&linkn=" & Admin.CommonFunction.CommonFunction.EncryptData(plink) & "&btn=" & Admin.CommonFunction.CommonFunction.EncryptData(btn) & "&tab=" & Admin.CommonFunction.CommonFunction.EncryptData(inttabID) & "&RNum=" & Session("RandomNo")
                    Else
                        strTabURL = Dttab1.Rows(tabcnt).ItemArray(2).ToString() & "?linkm=" & Admin.CommonFunction.CommonFunction.EncryptData(glink) & "&linkn=" & Admin.CommonFunction.CommonFunction.EncryptData(plink) & "&btn=" & Admin.CommonFunction.CommonFunction.EncryptData(btn) & "&tab=" & Admin.CommonFunction.CommonFunction.EncryptData(inttabID) & "&RNum=" & Session("RandomNo")
                    End If
                    strTabName = Dttab1.Rows(tabcnt).ItemArray(1).ToString()
                    Me.hdnTabVal.Value += "../" + strTabURL & "," & Admin.CommonFunction.CommonFunction.EncryptData(inttabID) & "," & strTabName & "|"
                    ' Me.hdnTabVal.Value += strTabURL & "," & inttabID & "," & strTabName & "|"
                Next
            End If
        Catch ex As Exception

            Response.Redirect("~/LogOut.aspx")
        End Try
        Return strTabName
    End Function
   
    Protected Sub Createbutton()
        Dim strFrstBtnID As String = ""
        Dim DTbtn As DataTable, DTtab As DataTable = Nothing
        Dim objcomn As New CommonDLL()
        Dim strTabName As String = Nothing
        Dim inttabID As Integer = 0

        glink = Admin.CommonFunction.CommonFunction.DecryptData(Request.QueryString("linkm"))
        plink = Admin.CommonFunction.CommonFunction.DecryptData(Request.QueryString("linkn"))
        If Session("UserId") IsNot "" AndAlso Session("UserId") IsNot Nothing Then
            tempUser = Convert.ToInt32(Session("UserId"))
        End If
        Try
            GetAccessRights("A", Convert.ToInt32(plink), tempUser)
            'Code for Get button
            ''objTabbtncreate.chr_ActionCode = "V"
            ''objTabbtncreate.IntPlinkID = Convert.ToInt32(plink)
            ''objTabbtncreate.vchBtnAccess = strBtnaccess
            ''DTbtn = objTabbtncreate.GetButtons(objTabbtncreate)
            DTbtn = GetButtons(strBtnaccess, Convert.ToInt32(plink))
            If DTbtn.Rows.Count > 0 Then
                For counter As Integer = 0 To DTbtn.Rows.Count - 1
                    If Convert.ToInt32(DTbtn.Rows(counter).ItemArray(3)) = 1 Then
                        If i <> 1 Then
                            strFrstBtnID = DTbtn.Rows(counter).ItemArray(0).ToString()
                            i += 1
                        End If
                        'Code To Get the URL of first tab
                        BtnId = DTbtn.Rows(counter).ItemArray(0).ToString()
                     
                        DTtab = GetTabs(strBtnaccess, Convert.ToInt32(BtnId))
                        If DTtab.Rows.Count > 0 Then
                            For countertab As Integer = 0 To DTtab.Rows.Count - 1
                                inttabID = Convert.ToInt32(DTtab.Rows(countertab).ItemArray(0))
                                If DTtab.Rows(countertab).ItemArray(2).ToString().Contains("?") Then
                                    btnURL = DTtab.Rows(countertab).ItemArray(2).ToString() & "&linkm=" & Admin.CommonFunction.CommonFunction.EncryptData(glink) & "&linkn=" & Admin.CommonFunction.CommonFunction.EncryptData(plink) & "&btn=" & Admin.CommonFunction.CommonFunction.EncryptData(BtnId) & "&tab=" & Admin.CommonFunction.CommonFunction.EncryptData(inttabID) & "&RNum=" & Session("RandomNo")
                                Else
                                    btnURL = DTtab.Rows(0).ItemArray(2).ToString() & "?linkm=" & Admin.CommonFunction.CommonFunction.EncryptData(glink) & "&linkn=" & Admin.CommonFunction.CommonFunction.EncryptData(plink) & "&btn=" & Admin.CommonFunction.CommonFunction.EncryptData(BtnId) & "&tab=" & Admin.CommonFunction.CommonFunction.EncryptData(inttabID) & "&RNum=" & Session("RandomNo")
                                End If
                            Next
                        Else
                            If DTbtn.Rows(counter).ItemArray(2).ToString().Contains("?") Then
                                btnURL = DTbtn.Rows(counter).ItemArray(2).ToString() & "&linkm=" & Admin.CommonFunction.CommonFunction.EncryptData(glink) & "&linkn=" & Admin.CommonFunction.CommonFunction.EncryptData(plink) & "&btn=" & Admin.CommonFunction.CommonFunction.EncryptData(DTbtn.Rows(counter).ItemArray(0).ToString()) & "&tab=" & Admin.CommonFunction.CommonFunction.EncryptData(inttabID) & "&RNum=" & Session("RandomNo")
                            Else
                                btnURL = DTbtn.Rows(counter).ItemArray(2).ToString() & "?linkm=" & Admin.CommonFunction.CommonFunction.EncryptData(glink) & "&linkn=" & Admin.CommonFunction.CommonFunction.EncryptData(plink) & "&btn=" & Admin.CommonFunction.CommonFunction.EncryptData(DTbtn.Rows(counter).ItemArray(0).ToString()) & "&tab=" & Admin.CommonFunction.CommonFunction.EncryptData(inttabID) & "&RNum=" & Session("RandomNo")
                            End If
                        End If
                        BtnId = Admin.CommonFunction.CommonFunction.EncryptData(DTbtn.Rows(counter).ItemArray(0).ToString())
                        BtnName = DTbtn.Rows(counter).ItemArray(1).ToString()
                        Me.hdnBtn.Value += "../" + btnURL & "," & BtnId & "," & BtnName & "|"
                        ' Me.hdnBtn.Value += btnURL & "," & BtnId & "," & BtnName & "|"
                    End If
                Next
            End If
            'Code To Get Tab of concern button depending on the tab access and set the tab url if tab available

            If Convert.ToInt32(btn) <> 0 AndAlso Convert.ToInt32(tab) = 0 Then
                strTabName = Gettab("T", strTabaccess, Convert.ToInt32(btn))
            End If
            If Convert.ToInt32(btn) <> 0 AndAlso Convert.ToInt32(tab) <> 0 Then
                strTabName = Gettab("T", strTabaccess, Convert.ToInt32(btn))
            End If

            'Code to set a default button if any plink has no tab or button

            'If Convert.ToInt32(tab) = 0 AndAlso Convert.ToInt32(tab) = 0 AndAlso Convert.ToInt32(IIf(strFrstBtnID = "", 0, strFrstBtnID)) <> 0 Then
            '    strTabName = Gettab("T", strTabaccess, Convert.ToInt32(btn))
            'End If
        Catch ex As Exception
            If ex.Message.ToString() = "no access rights" Then
                Response.Write("<script>alert('You donot have an access right to this page');window.location.href='../LogOut.aspx';</script>")
            Else
                Throw New Exception(ex.Message.ToString())
            End If
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("UserId") = Nothing) Then
                Response.Redirect("~/LogOut.aspx", False)
            End If
         
            If Not IsPostBack = True Then
                btn = Admin.CommonFunction.CommonFunction.DecryptData(Request.QueryString("btn"))
                tab = Admin.CommonFunction.CommonFunction.DecryptData(Request.QueryString("tab"))
                Createbutton()
            End If
        Catch ex As Exception
            Response.Redirect("~/LogOut.aspx", False)
            ''Response.Write(ex.Message)

        End Try
    End Sub
End Class
