//******************************************************************************************************************
// File Name             :   IncentiveApplicationStatus.aspx.cs
// Description           :   Show the status of the Incentive
// Created by            :   AMit Sahoo
// Created on            :   30th June 2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IncentiveApplicationStatus : System.Web.UI.Page
{
    DataTable dtable;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();

    }
    private void BindGrid()
    {
        try
        {
            DataSet ds = new DataSet();
            dtable = new DataTable();
            DataRow dtRow;
            DataColumn Slno = new DataColumn("SlNo", Type.GetType("System.Int32"));
            DataColumn AppNo = new DataColumn("AppNo", Type.GetType("System.String"));
            DataColumn Name = new DataColumn("Name", Type.GetType("System.String"));
            DataColumn Amount = new DataColumn("Amount", Type.GetType("System.Int32"));
            DataColumn Status = new DataColumn("Status", Type.GetType("System.String"));

            dtable.Columns.Add("SlNo");
            dtable.Columns.Add("AppNo");
            dtable.Columns.Add("Name");
            dtable.Columns.Add("IndType");
            dtable.Columns.Add("Amount");
            dtable.Columns.Add("Status");


            dtRow = dtable.NewRow();
            dtRow["SlNo"] = 1;
            dtRow["AppNo"] = "123456789";
            dtRow["Name"] = "Vedanta Aluminium";
            dtRow["IndType"] = "Aluminium";
            dtRow["Amount"] = 200000;
            dtRow["Status"] = "Pending";
            dtable.Rows.Add(dtRow);

            dtRow = dtable.NewRow();
            dtRow["SlNo"] = 2;
            dtRow["AppNo"] = "35464789";
            dtRow["Name"] = "TATA";
            dtRow["IndType"] = "Telecom ";
            dtRow["Amount"] = 1203350;
            dtRow["Status"] = "Pending";
            dtable.Rows.Add(dtRow);

            dtRow = dtable.NewRow();
            dtRow["SlNo"] = 3;
            dtRow["AppNo"] = "123456789";
            dtRow["Name"] = "Birla and Sons";
            dtRow["IndType"] = "IT";
            dtRow["Amount"] = 320510;
            dtRow["Status"] = "Pending";
            dtable.Rows.Add(dtRow);

            dtRow = dtable.NewRow();
            dtRow["SlNo"] = 4;
            dtRow["AppNo"] = "123456789";
            dtRow["Name"] = "Neyveli Industries";
            dtRow["IndType"] = "Aluminium";
            dtRow["Amount"] = 257800;
            dtRow["Status"] = "Pending";

            dtable.Rows.Add(dtRow);
            ds.Tables.Add(dtable);
            gvAppStatus.DataSource = ds.Tables[0];
            gvAppStatus.DataBind();
           


        }
        catch (Exception ex)
        {
            
        }
        finally
        {
            dtable = null;            
        }
    }
    protected void gvAppStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAppStatus.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}