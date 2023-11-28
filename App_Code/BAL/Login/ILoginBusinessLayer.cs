using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EntityLayer.Login;
using System.ServiceModel.Web;
using System.Data;

namespace BusinessLogicLayer.Login
{
    [ServiceContract]
    public interface ILoginBusinessLayer
    {
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<LoginDetails> SWPLogin(string strAction, string strUserId, string strPWD);

        [OperationContract]
        // [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<LoginDetails> getUserDetails(LoginDetails objLogin);

        [OperationContract]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string UpdatePassword(LoginDetails objLogin);

        [OperationContract]
        // [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<LoginDetails> ViewChngPwd(LoginDetails objLogin);

        [OperationContract]
        // [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string ManageChngPwd(LoginDetails objLogin);
        [OperationContract]
        string ManageTokenNumber(string strAction, string stremail, string strtoken, string strtokentime);
        [OperationContract]
        // [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<LoginDetails> GetInvDetails(LoginDetails objLogin);
        [OperationContract]
        DataTable GetTokenDetails(string action, string tokenno);
        [OperationContract]
        string UpdateLoginFailedStatus(LoginDetails objLogin);
        [OperationContract]
        DataTable GetLogFailedDetails(LoginDetails objLogin);
    }
}
