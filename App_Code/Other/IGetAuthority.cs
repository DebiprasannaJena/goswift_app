using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGetAuthority" in both code and config file together.
[ServiceContract]
public interface IGetAuthority
{
	[OperationContract]
	void DoWork();


    //*************************************************************************************************//
    //                              GET Authority
    //                              CREATED BY:Smruti Ranjan Nayak
    //                              CREATED ON:28th Oct 2015
    //*************************************************************************************************//
    /// <summary>
    /// This method is used for get News data.
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        UriTemplate = "getAuth/{strCode}/{strNo}")]
    string getAuth(string strCode,string strNo);

}
