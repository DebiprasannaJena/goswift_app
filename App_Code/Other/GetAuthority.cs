using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net;
using System.Data;
using System.IO;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetAuthority" in code, svc and config file together.
public class GetAuthority : IGetAuthority
{
	public void DoWork()
	{
	}


    public string getAuth(string strCode, string strNo)
    {
      //  OR01A 1234
        string strResult = "0|NA";
        WebClient web = new WebClient();
        string url = string.Format("http://as2.ori.nic.in:8080/web/RegRTO?regn=" + strCode + " " + strNo);
        string responses = web.DownloadString(url);
        DataSet ds = new DataSet();
        using (StringReader stringReader = new StringReader(responses))
        {
            ds = new DataSet();
            ds.ReadXml(stringReader);
        }
        DataTable dt = ds.Tables[0];

        if (dt.Rows.Count > 0)
        {
            strResult = dt.Rows[0][0].ToString() + "|" + dt.Rows[0][1].ToString();
        }
        return strResult;
    }
}
