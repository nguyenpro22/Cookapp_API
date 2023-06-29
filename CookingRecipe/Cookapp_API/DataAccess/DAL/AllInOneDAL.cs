using Cookapp_API.Data;
using Cookapp_API.DataAccess.DTO;
using Cookapp_API.DataAccess.DTO.AllInOneDTO;
using Cookapp_API.DataAccess.DTO.AllInOneDTO.PostDTO;
using NuGet.Protocol;
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
                string query = "Select a.id,a.title,a.content,a.create_time,a.update_time, a.cooktime,a.addtime,a.preptime,a.totaltime,a.image, b.catetitle, c.FullName, STRING_AGG(g.tagname,',') as tag, STRING_AGG(i.name,',') as ingredient, STRING_AGG(k.name,',') as nutrition from recipeposts a " +
                    "left join category b on a.ref_cate = b.id " +
                    "left join accounts c on a.ref_account = c.id " +
                    "left join tag_post f on a.id = f.ref_post " +
                    "left join tags g on f.ref_tag = g.id " +
                    "left join ingre_post h on a.id = h.ref_post " +
                    "left join ingredients i on h.ref_ingredient = i.id "+
                    "left join nutri_post j on a.id = j.ref_post "+
                    "left join nutrition k on j.ref_nutri = k.Id " +
                    "group by a.id,a.title,a.content,a.create_time,a.update_time, a.cooktime,a.addtime,a.preptime,a.totaltime,a.image, b.catetitle, c.FullName";

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
        public List<PostDTO> GetPostbyID(string id)
        {
            try
            {
                string query = "Select a.id,a.title,a.content,a.create_time,a.update_time, a.cooktime,a.addtime,a.preptime,a.totaltime,a.image, b.catetitle, c.FullName, STRING_AGG(g.tagname,',') as tag, STRING_AGG(i.name,',') as ingredient, STRING_AGG(k.name,',') as nutrition from recipeposts a " +
                    "left join category b on a.ref_cate = b.id " +
                    "left join accounts c on a.ref_account = c.id " +
                    "left join tag_post f on a.id = f.ref_post " +
                    "left join tags g on f.ref_tag = g.id " +
                    "left join ingre_post h on a.id = h.ref_post " +
                    "left join ingredients i on h.ref_ingredient = i.id " +
                    "left join nutri_post j on a.id = j.ref_post " +
                    "left join nutrition k on j.ref_nutri = k.Id " +
                    "where a.id='" + id + "' " +
                    "group by a.id,a.title,a.content,a.create_time,a.update_time, a.cooktime,a.addtime,a.preptime,a.totaltime,a.image, b.catetitle, c.FullName";

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
            
        
        public int UpdatePost(string id, UpdatePostDTO post)
        {
            try
            {
                string query = "update " + _TABLE_NAME_POST;
                string filed = " SET ";
                if (post != null)
                {
                    if (!string.IsNullOrEmpty(post.Title))
                    {
                        filed += " title='" + post.Title + "'";
                    }
                    
                    if (!string.IsNullOrEmpty(post.Content))
                    {
                        filed += (filed != " SET " ? "," : "") + " content='" + post.Content + "'";
                    }
                        filed += (filed != " SET " ? "," : "") + " update_time='" + DateTime.Now + "'";

                    if (post.preptime > 0)
                    {
                        filed += (filed != " SET " ? "," : "") + " preptime='" + post.preptime + "'";
                    }
                    if (post.cooktime > 0)
                    {
                        filed += (filed != " SET " ? "," : "") + " cooktime='" + post.cooktime + "'";
                    }
                    if (post.addtime > 0)
                    {
                        filed += (filed != " SET " ? "," : "") + " addtime='" + post.addtime + "'";
                    }
                    filed += (filed != " SET " ? "," : "") + " totaltime='" + (post.preptime + post.cooktime + post.addtime).ToString() + "'";
                    //if (!string.IsNullOrEmpty(post.tag))
                    //{
                    //    filed += (filed != " SET " ? "," : "") + " tag='" + post.tag + "'";
                    //}    


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
        public int CreatePost(CreatePostDTO post)
        {
            try
            {
                string query = "insert into " + _TABLE_NAME_POST;
                string filed = " values ";
                if (post != null)
                {
                    filed+= "('" + Guid.NewGuid().ToString() + "'";
                    if (!string.IsNullOrEmpty(post.Title))
                    {
                        filed += (filed != " values " ? "," : "") + "'" + post.Title + "'";
                    }

                    if (!string.IsNullOrEmpty(post.RefTag))
                    {
                        filed += (filed != " values " ? "," : "") + "'" + post.RefTag + "'";
                    }
                     if (!string.IsNullOrEmpty(post.Content))
                    {
                        filed += (filed != " values " ? "," : "") + "'" + post.Content + "'";
                    }
                        filed += (filed != " values " ? "," : "") + "'" + DateTime.Now + "'";
                    
                    
                        filed += (filed != " values " ? "," : "") + "null";
                    
                    if (!string.IsNullOrEmpty(post.RefAccount))
                    {
                        filed += (filed != " values " ? "," : "") + "'" + post.RefAccount + "'";
                    }
                   if (!string.IsNullOrEmpty(post.RefCategory))
                    {
                        filed += (filed != " values " ? "," : "") + " CONVERT(varbinary(max), '" + post.Image + "'";
                    }

                    if (!string.IsNullOrEmpty(post.RefCategory))
                    {
                        filed += (filed != " values " ? "," : "") + "'" + post.RefCategory + "'";
                    }
                    if (post.preptime > 0)
                    {
                        filed += (filed != " values " ? "," : "") + "'" + post.preptime + "'";
                    }
                    if (post.addtime > 0)
                    {
                        filed += (filed != " values " ? "," : "") + "'" + post.addtime + "'";
                    }

                    if (post.cooktime > 0)
                    {
                        filed += (filed != " values " ? "," : "") + "'" + post.cooktime + "'";
                    }
                    
                    filed += (filed != " values " ? "," : "") + "'" + (post.preptime + post.cooktime + post.addtime).ToString() + "')";
                    


                }
                if (filed != " values ")
                {
                    query += filed;
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
