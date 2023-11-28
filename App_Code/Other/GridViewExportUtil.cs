using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// Summary description for ExportUtil
/// </summary>
public class GridViewExportUtil
{
    public GridViewExportUtil()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Export(string fileName, GridView gv)
    {
        try
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();
                    //table.Width = 900;
                    table.CellPadding = 1;
                    table.CellSpacing = 1;
                    table.GridLines = GridLines.Both;
                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {

                        GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                        table.Rows[0].BackColor = System.Drawing.Color.Gray;
                        table.Rows[0].Height = Unit.Pixel(60);
                    }
                    int cnt = 0;
                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        cnt = cnt + 1;
                        GridViewExportUtil.PrepareControlForExport(row);
                        table.Rows.Add(row);
                        for (int i = 0; i < table.Rows[cnt].Cells.Count; i++)
                        {
                            table.Rows[cnt].Cells[i].VerticalAlign = VerticalAlign.Top;
                        }
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();                   
                }
            }
        }
        catch (Exception ex)
        {
            
        }
        finally
        {
 
        }
    }
    private static void PrepareControlForExport(Control control)
    {
        try
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is HiddenField)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HiddenField).Value = ""));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    GridViewExportUtil.PrepareControlForExport(current);
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
 
        }
    }

    public static Table GridViewTable(GridView gv, Table tbl) 
    {
       Table table = new Table();
        //table.Width = 900;
        table.CellPadding = 1;
        table.CellSpacing = 1;
        table.GridLines = GridLines.Both;
        //  add the header row to the table
        //table.Rows[0].Cells.
        table.Rows.Add(tbl.Rows[0]);

       // table.Rows.Add(tbl.Rows[1]);
        if (gv.HeaderRow != null)
        {

            GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
            table.Rows.Add(gv.HeaderRow);
         
        }
        int cnt = 0;
        //  add each of the data rows to the table
        foreach (GridViewRow row in gv.Rows)
        {
            cnt = cnt + 1;
            GridViewExportUtil.PrepareControlForExport(row);
            table.Rows.Add(row);
            for (int i = 0; i < table.Rows[cnt].Cells.Count; i++)
            {
                table.Rows[cnt].Cells[i].VerticalAlign = VerticalAlign.Top;
            }
        }

        //  add the footer row to the table
        if (gv.FooterRow != null)
        {
            GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
            table.Rows.Add(gv.FooterRow);
        }
        return table;
    }

    public static void ExporthtmlTable(string fileName, Table table,GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
        "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        GridViewExportUtil.PrepareControlForExport(table);
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
               

               table=GridViewTable(gv,table);

                int cnt = 0;

                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    public static void ExportWord(string fileName, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
        "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/msword";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a form to contain the grid
                Table table = new Table();
                //table.Width = 900;
                table.CellPadding = 1;
                table.CellSpacing = 1;
                table.GridLines = GridLines.Both;
                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                    GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    GridViewExportUtil.PrepareControlForExport(row);
                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                //  render the table into the htmlwriter
                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    public static void ExporthtmlTableDOC(string fileName, Table table, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
        "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/vnd.ms-docx";
        GridViewExportUtil.PrepareControlForExport(table);
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {


                table = GridViewTable(gv, table);

                int cnt = 0;

                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

}
