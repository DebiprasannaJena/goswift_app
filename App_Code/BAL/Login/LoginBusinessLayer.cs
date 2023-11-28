using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLayer.Login;
using DataAcessLayer.Login;
using System.Data;
namespace BusinessLogicLayer.Login
{
    public class LoginBusinessLayer : ILoginBusinessLayer
    {
        LoginDataLayer objDataAccess = new LoginDataLayer();
        public List<LoginDetails> SWPLogin(string strAction, string strUserId, string strPWD)
        {
            return objDataAccess.SWPLogin(strAction, strUserId, strPWD);
        }
        public List<LoginDetails> getUserDetails(LoginDetails objLogin)
        {
            return objDataAccess.getUserDetails(objLogin);
        }
        public string UpdatePassword(LoginDetails objLogin)
        {
            return objDataAccess.UpdatePassword(objLogin);
        }
        public List<LoginDetails> ViewChngPwd(LoginDetails objLogin)
        {
            return objDataAccess.ViewChngPwd(objLogin);
        }
        public string ManageChngPwd(LoginDetails objLogin)
        {
            return objDataAccess.ManageChngPwd(objLogin);
        }

        public List<LoginDetails> getDeptUserDetails(LoginDetails objLogin)
        {
            return objDataAccess.getDeptUserDetails(objLogin);
        }
        public string UpdateDeptPassword(LoginDetails objLogin)
        {
            return objDataAccess.UpdateDeptPassword(objLogin);
        }

        public List<LoginDetails> ViewDeptChngPwd(LoginDetails objLogin)
        {
            return objDataAccess.ViewDeptChngPwd(objLogin);
        }
        public string ManageMobileAndEmail(LoginDetails objLogin)
        {
            return objDataAccess.ManageMobileAndEmail(objLogin);
        }
        public List<LoginDetails> ViewEmailAndMobile(LoginDetails objLogin)
        {
            return objDataAccess.ViewEmailAndMobile(objLogin);
        }
        public string ManageDeptChngPwd(LoginDetails objLogin)
        {
            return objDataAccess.ManageDeptChngPwd(objLogin);
        }

        public string DeptEditProfile(LoginDetails objLogin)
        {
            return objDataAccess.DeptEditProfile(objLogin);
        }
        public string ManageTokenNumber(string strAction, string stremail, string strtoken, string strtokentime)
        {
            return objDataAccess.ManageTokenNumber(strAction, stremail, strtoken, strtokentime);
        }
        public List<LoginDetails> GetInvDetails(LoginDetails objLogin)
        {
            return objDataAccess.GetInvDetails(objLogin);
        }
        public DataTable GetTokenDetails(string action, string tokenno)
        {
            return objDataAccess.GetTokenDetails(action, tokenno);
        }
        public string UpdateLoginFailedStatus(LoginDetails objLogin)
        {
            return objDataAccess.UpdateLoginFailedStatus(objLogin);
        }
        public DataTable GetLogFailedDetails(LoginDetails objLogin)
        {
            return objDataAccess.GetLogFailedDetails(objLogin);
        }

        /// <summary>
        /// Added by Sushant Jena on Dt. 07-Aug-2018
        /// For new PAN based login process
        /// </summary>
        /// <param name="objLogin"></param>
        /// <returns></returns>
        public DataTable LoginGOSWIFT(LoginDetails objLogin)
        {
            return objDataAccess.LoginGOSWIFT(objLogin);
        }


        #region PAN Updation

        public string checkPanAvailStatus(LoginDetails objLogin)
        {
            return objDataAccess.checkPanAvailStatus(objLogin);
        }
        public string PAN_AED(LoginDetails objLogin)
        {
            return objDataAccess.PAN_AED(objLogin);
        }

        #endregion

        public DataTable forgotUserId(LoginDetails objLogin)
        {
            return objDataAccess.forgotUserId(objLogin);
        }
        public DataTable viewInvestorDetails(LoginDetails objLogin)
        {
            return objDataAccess.viewInvestorDetails(objLogin);
        }
        public string createAliasName(LoginDetails objLogin)
        {
            return objDataAccess.createAliasName(objLogin);
        }
        public string resetAliasName(LoginDetails objLogin)
        {
            return objDataAccess.resetAliasName(objLogin);
        }
    }
}
