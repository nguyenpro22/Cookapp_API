using Cookapp_API.DataAccess.DTO;
using Cookapp_API.DataAccess.DTO.AllInOneDTO;
using System.Collections;

namespace Cookapp_API.DataAccess.DAL
{
    public class AllInOneDAL : MSSQLSERVERDataAccess
    {
        public const string _TABLE_NAME_ACCOUNT = "Accounts";
        public const string _TABLE_NAME_POST = "recipeposts";
        public const string _TABLE_NAME_CATEGORY = "category";
        public const string _TABLE_NAME_BLACKLIST = "blacklist";
        public AllInOneDAL() : base() { }

        public AllInOneDAL(string connectionString) : base(connectionString) { }

        public AllInOneDAL(string connectionString, int timeout) : base(connectionString, timeout) { }
        public List<CategoryDTO> GetCategory()
        {
            try
            {
                string query = "select * from " + _TABLE_NAME_CATEGORY;
                List<Hashtable> arrHsObj;
                arrHsObj = ExecuteArrayHastable(query);
                CategoryDTO acc;
                if (arrHsObj != null && arrHsObj.Count > 0)
                {
                    List<CategoryDTO> arrRes = new List<CategoryDTO>(arrHsObj.Count);
                    for (int i = 0; i < arrHsObj.Count; i++)
                    {
                        acc = new CategoryDTO(arrHsObj[i]);
                        arrRes.Add(acc);
                    }
                    return arrRes;
                }
                else
                    return new List<CategoryDTO> { };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<PostDTO> GetPost()
        {
            try
            {
                string query = "select * from " + _TABLE_NAME_POST;
                List<Hashtable> arrHsObj;
                arrHsObj = ExecuteArrayHastable(query);
                PostDTO acc;
                if (arrHsObj != null && arrHsObj.Count > 0)
                {
                    List<PostDTO> arrRes = new List<PostDTO>(arrHsObj.Count);
                    for (int i = 0; i < arrHsObj.Count; i++)
                    {
                        acc = new PostDTO(arrHsObj[i]);
                        arrRes.Add(acc);
                    }
                    return arrRes;
                }
                else
                    return new List<PostDTO> { };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
