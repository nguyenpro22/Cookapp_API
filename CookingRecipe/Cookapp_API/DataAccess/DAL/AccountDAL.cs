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
        public int UpdateAccount(string id, AccountDTO account)
        {
            try
            {
                string query = "update " + _TABLE_NAME_ACCOUNT;
                string filed = " SET ";
                if(account != null)
                {
                    if(!string.IsNullOrEmpty(account.username))
                    {                        
                        filed += " username='" + account.username + "'";
                    }
                    if(!string.IsNullOrEmpty(account.Password))
                    {
                        filed += (filed != " SET " ? "," : "") + " password='" + account.Password + "'";
                    }
                    if(account.Dob != DateTime.MinValue) 
                    {
                        filed += (filed != " SET " ? "," : "") + " dob='" + account.Dob.ToString("yyyy/MM/dd HH:mm:ss") + "'";
                    }
                    if(!string.IsNullOrEmpty(account.FullName)) 
                    {
                        filed += (filed != " SET " ? "," : "") + " fullname='" + account.FullName + "'";
                    }
                }
                if (filed != " SET ")
                {
                    query += filed + " where id='" + id + "'";
                    return ExecuteNonQuery(query);                    
                }
                else
                    return 0;
                                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
