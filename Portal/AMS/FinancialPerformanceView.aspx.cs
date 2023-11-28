//******************************************************************************************************************
// File Name             :   SingleWindow/FinancialPerformanceView.aspx
// Description           :   To View Propsal details against a project id
// Created by            :   Tapan Kumar Mishra
// Created on            :   23-July-2016
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_FinancialPerformanceView : System.Web.UI.Page
{
    #region "Member Variable"
    static int rowIndex = -1;
    AMS objams = null;
    Agenda objcs = null;
    string strVal = "";
    DataTable dt = null;
    List<Finance> objFinance;
    Finance objFin;
    #endregion

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                FillDetails();
            }
        }
    }
   
    #endregion


    #region "User function"

    private class Finance
    {
        public int SlNo { get; set; }
        public int KeyId { get; set; }
        public int FinanceId { get; set; }
        public string ComapnyName { get; set; }
        public string Remark { get; set; }
        public string Particulars { get; set; }
        public string FinYear1 { get; set; }
        public string FinYear2 { get; set; }
        public string FinYear3 { get; set; }
        public string FinDoc { get; set; }
    }

    private void FillDetails()
    {
        AMS objams = new AMS();
        DataTable dt = new DataTable();
        try
        {
            objams.Action = "E";
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);     
            dt = new DataTable();
            dt = AMServices.ViewFinace(objams);
            if (dt.Rows.Count > 0)
            {
                objFinance = new List<Finance>();
                foreach (DataRow dr in dt.Rows)
                {
                    objFin = new Finance();
                    objFin.FinanceId = Convert.ToInt32(dr[0]);
                    objFin.ComapnyName = Convert.ToString(dr[1]);
                    objFin.Particulars = Convert.ToString(dr[2]);
                    objFin.FinYear1 = Convert.ToString(dr[3]);
                    objFin.FinYear2 = Convert.ToString(dr[4]);
                    objFin.FinYear3 = Convert.ToString(dr[5]);
                    objFin.Remark = Convert.ToString(dr[6]);
                    objFin.FinDoc = Convert.ToString(dr[7]);
                    objFinance.Add(objFin);
                }

                GrdFinanace.DataSource = objFinance;
                GrdFinanace.Columns[3].HeaderText = dt.Columns[3].ColumnName;
                GrdFinanace.Columns[4].HeaderText = dt.Columns[4].ColumnName;
                GrdFinanace.Columns[5].HeaderText = dt.Columns[5].ColumnName;
                GrdFinanace.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null; dt = null; }

    }

    #endregion

    protected void OnDataBound(object sender, EventArgs e)
    {
        int[] a = new int[2] { 1, 2 };
        for (int i = GrdFinanace.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = GrdFinanace.Rows[i];
            GridViewRow previousRow = GrdFinanace.Rows[i - 1];
            foreach (var j in a)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        if (j == 1)
                        {
                            if (row.Cells[6].RowSpan == 0)
                            {
                                previousRow.Cells[6].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[6].RowSpan = row.Cells[6].RowSpan + 1;
                            }
                            row.Cells[6].Visible = false;

                            if (row.Cells[7].RowSpan == 0)
                            {
                                previousRow.Cells[7].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[7].RowSpan = row.Cells[7].RowSpan + 1;
                            }
                            row.Cells[7].Visible = false;
                        }
                        row.Cells[j].Visible = false;
                    }
                }
            }
        }
    }

    protected void GrdFinanace_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlinkFinDoc = (HyperLink)e.Row.FindControl("hlDoc");
            HiddenField hdhDoc = (HiddenField)e.Row.FindControl("hdnFinDoc");
            string strDocs = hdhDoc.Value;
            if (hdhDoc.Value != "")
            {
                hlinkFinDoc.NavigateUrl = "../SingleWindow/FinDoc/" + Request.QueryString["ID"] + "/" + strDocs;
            }
            else
            {
                hlinkFinDoc.ImageUrl = "";
            }
        }
    }
}