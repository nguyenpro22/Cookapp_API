using Cookapp_API.DataAccess.DTO;
using Cookapp_API.DataAccess.DTO.AllInOneDTO;
using Cookapp_API.DataAccess.DTO.AllInOneDTO.PostDTO;

namespace Cookapp_API.DataAccess.BLL
{
    public class AllInOneBLL
    {
        string _connetionString;
        int _timeout;
        ESqlProvider _sqlProvider;
        public AllInOneBLL(string connetionString, ESqlProvider sqlProvider, int timeout)
        {
            _connetionString = connetionString;
            _sqlProvider = sqlProvider;
            _timeout = timeout;
        }
        private DAL.AllInOneDAL GetDAL_MSSQLSERVER()
        {
            if (!string.IsNullOrEmpty(_connetionString))
            {
                return new DAL.AllInOneDAL(_connetionString
                    , _timeout);
            }
            else
            {
                throw new Exception("SqlConnectionString is Empty");
            }
        }
        public List<CategoryDTO> GetCategory()
        {
            try
            {
                if (_sqlProvider == ESqlProvider.SQLSERVER)
                {
                    DAL.AllInOneDAL dal = GetDAL_MSSQLSERVER();
                    return dal.GetCategory();
                }
                else { throw new Exception("not support unknown sqlProvider"); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PostDTO> GetPosts(List<string> ids)
        {
            try
            {
                if (_sqlProvider == ESqlProvider.SQLSERVER)
                {
                    DAL.AllInOneDAL dal = GetDAL_MSSQLSERVER();
                    return dal.GetPosts(ids);
                }
                else { throw new Exception("not support unknown sqlProvider"); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int UpdatePost(string id, UpdatePostDTO post)
        {
            try
            {
                if (_sqlProvider == ESqlProvider.SQLSERVER)
                {
                    DAL.AllInOneDAL dal = GetDAL_MSSQLSERVER();
                    return dal.UpdatePost(id, post);
                }
                else { throw new Exception("not support unknown sqlProvider"); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<PostDTO> GetPostByID(string id)
        {
            try
            {
                if (_sqlProvider == ESqlProvider.SQLSERVER)
                {
                    DAL.AllInOneDAL dal = GetDAL_MSSQLSERVER();
                    return dal.GetPostbyID(id);
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
