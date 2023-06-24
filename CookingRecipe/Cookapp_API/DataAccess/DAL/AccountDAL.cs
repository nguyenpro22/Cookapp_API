using Cookapp_API.DataAccess.DTO;
using System.Collections;

namespace Cookapp_API.DataAccess.DAL
{
    public class AccountDAL : MSSQLSERVERDataAccess
    {
        public const string _TABLE_NAME_ACCOUNT = "Accounts";
        public const string _TABLE_NAME_IMAGES = "images";
        public AccountDAL() : base() { }

        public AccountDAL(string connectionString) : base(connectionString) { }

        public AccountDAL(string connectionString, int timeout) : base(connectionString, timeout) { }

        public List<AccountDTO> GetAccounts()
        {
            try
            {
                string query = "select * from " + _TABLE_NAME_ACCOUNT;
                List<Hashtable> arrHsObj;
                arrHsObj = ExecuteArrayHastable(query);
                AccountDTO acc;
                if (arrHsObj != null && arrHsObj.Count > 0)
                {
                    List<AccountDTO> arrRes = new List<AccountDTO>(arrHsObj.Count);
                    for(int i = 0; i < arrHsObj.Count; i++)
                    {
                        acc = new AccountDTO(arrHsObj[i]);
                        arrRes.Add(acc);
                    }
                    return arrRes;
                }
                else
                    return new List<AccountDTO> { };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        

    }
}
