//******************************************************************************************************************
// File Name             :   SingleWindow/FinancialDetailShow.aspx
// Description           :   To Financial details against a project id
// Created by            :   Surya Prakash Barik
// Created on            :   30-OCT-2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//******************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_FinancialDetailShow : System.Web.UI.Page
{
    #region "Member Variable"
    static int rowIndex = -1;
    AMS objams = null;
    Agenda objcs = null;
    DataTable dt = null;
    List<FinanceDoc> objFinanceDoc;
    FinanceDoc objFinDoc;
    #endregion

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
               FillFinDoc();
            }
        }
    }

    private void FillFinDoc()
    { 
      objams = new AMS();
      try
      {
          objams.Action = "FD";  //Fetch Documents
          objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
          dt = new DataTable();
          dt = AMServices.ViewFinace(objams);
          if (dt.Rows.Count > 0)
          {
              objFinanceDoc = new List<FinanceDoc>();
              foreach (DataRow dr in dt.Rows)
              {
                  objFinDoc = new FinanceDoc();
                  objFinDoc.KeyId = Convert.ToInt32(dr[0]);
                  objFinDoc.ProjectId = Convert.ToInt32(dr[1]);
                  objFinDoc.FinOriDoc = Convert.ToString(dr[2]);
                  objFinDoc.FinNewDoc = Convert.ToString(dr[3]);
                  objFinDoc.Type = Convert.ToInt32(dr[4]);
                  objFinanceDoc.Add(objFinDoc);
              }
              grdFinDoc.DataSource = objFinanceDoc;
              grdFinDoc.DataBind();
          }
          else
          {
              lblMessage1.Visible = true;
          }
      }
      catch (Exception)
      {
          grdFinDoc.DataSource = null;
          grdFinDoc.DataBind();
      }
        finally { objams = null; }
    }
    
    protected void grdFinDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlinkFinDoc = (HyperLink)e.Row.FindControl("hlDoc");
            HiddenField hdhDoc = (HiddenField)e.Row.FindControl("hdnFinDoc");
            string strDocs = hdhDoc.Value;
            if (hdhDoc.Value != "")
            {
                hlinkFinDoc.NavigateUrl = "../AMS/FinDoc/" + Request.QueryString["ID"] + "/" + strDocs;
            }
            else
            {
                hlinkFinDoc.ImageUrl = "";
            }
        }

    }

    public class FinanceDoc
    {
        public int SlNo { get; set; }
        public int KeyId { get; set; }
        public int ProjectId { get; set; }
        public string FinOriDoc { get; set; }
        public string FinNewDoc { get; set; }
        public int Type { get; set; }
    }

}