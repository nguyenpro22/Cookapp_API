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
        
        public List<PostDTO> GetPosts(List<string> ids)
        {
            try
            {
                string query = "Select a.id,a.title,a.content,a.create_time,a.update_time, a.cooktime,a.addtime,a.preptime, b.catetitle, d.image, c.FullName, g.tagname from recipeposts a " +
                    "left join category b on a.ref_cate = b.id " +
                    "left join accounts c on a.ref_account = c.id " +
                    "left join images d on a.ref_image = d.id " +
                    "left join tag_post f on a.id = f.ref_post " +
                    "inner join tags g on f.ref_tag = g.id " ;

                //if (ids != null && ids.Count > 0)
                //    query += "where a.id in(" + GlobalFuncs.ArrayStringToStringFilter(ids) + ")";
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
        //public int UpdatePost(string id, PostDTO post)
        //{
        //    try
        //    {
        //        string query = "update " + _TABLE_NAME_POST;
        //        string filed = " SET ";
        //        if (post != null)
        //        {
        //            if (!string.IsNullOrEmpty(post.Content))
        //            {
        //                filed += " content='" + post.Content + "'";
        //            }
        //            if (post.preptime >0 )
        //            {
        //                filed += (filed != " SET " ? "," : "") + " preptime='" + post.preptime + "'";
        //            }
        //            if (post.cooktime > 0)
        //            {
        //                filed += (filed != " SET " ? "," : "") + " cooktime='" + post.cooktime + "'";
        //            }
        //            if (post.addtime > 0)
        //            {
        //                filed += (filed != " SET " ? "," : "") + " addtime='" + post.addtime + "'";
        //            }
                    
        //                filed += (filed != " SET " ? "," : "") + " update_time='" + DateTime.Now + "'";
                    
        //            if (post.image.)
        //        }
        //        if (filed != " SET ")
        //        {
        //            query += filed + " where id='" + id + "'";
        //            return ExecuteNonQuery(query);
        //        }
        //        else
        //            return 0;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
