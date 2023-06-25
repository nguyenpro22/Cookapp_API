using Cookapp_API.DataAccess.DTO;

namespace Cookapp_API.DataAccess.BLL
{
    public class AccountBLL
    {
        string _connetionString;
        int _timeout;
        ESqlProvider _sqlProvider;
        public AccountBLL(string connetionString, ESqlProvider sqlProvider, int timeout)
        {
            _connetionString = connetionString;
            _sqlProvider = sqlProvider;
            _timeout = timeout;
        }
        private DAL.AccountDAL GetDAL_MSSQLSERVER()
        {
            if (!string.IsNullOrEmpty(_connetionString))
            {
                return new DAL.AccountDAL(_connetionString
                    , _timeout);
            }
            else
            {
                throw new Exception("SqlConnectionString is Empty");
            }
        }
        public List<AccountDTO> GetAccounts()
        {
            try
            {
                if(_sqlProvider == ESqlProvider.SQLSERVER)
                {
                    DAL.AccountDAL dal = GetDAL_MSSQLSERVER();
                    return dal.GetAccounts();
                }
                else { throw new Exception("not support unknown sqlProvider"); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int UpdateAccount(string id, AccountDTO account)
        {
            try
            {
                if (_sqlProvider == ESqlProvider.SQLSERVER)
                {
                    DAL.AccountDAL dal = GetDAL_MSSQLSERVER();
                    return dal.UpdateAccount(id, account);
                }
                else { throw new Exception("not support unknown sqlProvider"); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
