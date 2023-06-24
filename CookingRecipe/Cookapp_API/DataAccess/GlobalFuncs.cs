using System.Text.RegularExpressions;
using System.Text;

namespace Cookapp_API.DataAccess
{
    public class GlobalFuncs
    {
        public static string FixSqlInjection(string svalue)
        {
            //return string.Empty;
            try
            {
                if (svalue == null)
                    return string.Empty;
                //Regex rg = new Regex("(?<name>[a-z,A-Z,_,\\-,0-9]+)");
                Regex rg = new Regex("(?<name>.+)");

                Match m = rg.Match(svalue);

                string result = string.Empty;
                if (m.Success)
                {
                    result = m.Groups["name"].Value;
                }
                result = result.Replace("'", "").Replace("--", "");

                result = Regex.Replace(result, "DECLARE ", "", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, "EXEC ", "", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, "sp_configure", "", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, "DELETE ", "", RegexOptions.IgnoreCase);//<@van CODE_20220322:fix lỗi sql khi table name là HotList_Deleted>

                result = Regex.Replace(result, "DROP ", "", RegexOptions.IgnoreCase);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string ArrayStringToString(List<string> arrStr, string split)
        {
            try
            {
                if (arrStr.Count == 0) return "";
                StringBuilder builder = new StringBuilder();
                int loopCount = arrStr.Count - 1;
                for (int i = 0; i < loopCount; i++)
                {
                    builder.Append(arrStr[i]);
                    builder.Append(split);
                }
                //append last item
                builder.Append(arrStr[arrStr.Count - 1]);
                return builder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ArrayIntToStringFilter(List<int> lstInt)
        {
            if (lstInt == null || lstInt.Count == 0)
                return String.Empty;
            //
            List<int> arrVerified_id = new List<int>();
            foreach (int x in lstInt)//<FORTIFY>
                arrVerified_id.Add(ConvertStringToInt32(x.ToString()));

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < arrVerified_id.Count; i++)
            {
                strBuilder.Append(arrVerified_id[i]);
                if (i < arrVerified_id.Count - 1)
                    strBuilder.Append(",");
            }
            return strBuilder.ToString();
        }

        public static string ArrayIntToStringFilter(List<long> lstInt)
        {
            if (lstInt == null || lstInt.Count == 0)
                return String.Empty;

            List<long> arrVerified_id = new List<long>();
            foreach (long x in lstInt)//<FORTIFY>
                arrVerified_id.Add(ConvertStringToInt64(x.ToString()));

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < arrVerified_id.Count; i++)
            {
                strBuilder.Append(arrVerified_id[i]);
                if (i < arrVerified_id.Count - 1)
                    strBuilder.Append(",");
            }
            return strBuilder.ToString();
        }

        public static string ArrayByteToStringFilter(List<byte> lstByte)
        {
            if (lstByte == null || lstByte.Count == 0)
                return String.Empty;
            //
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < lstByte.Count; i++)
            {
                strBuilder.Append(lstByte[i]);
                if (i < lstByte.Count - 1)
                    strBuilder.Append(",");
            }
            return strBuilder.ToString();
        }

        public static string ArrayStringToStringFilter(List<string> lstString)
        {
            if (lstString == null || lstString.Count == 0)
                return String.Empty;

            List<string> arrVerified_filter = new List<string>();
            foreach (string s in lstString)
                arrVerified_filter.Add(FixSqlInjectionForFilter(s));
            //
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < arrVerified_filter.Count; i++)
            {
                if (arrVerified_filter[i] == null)
                    arrVerified_filter[i] = string.Empty;
                //arrVerified_filter[i] = arrVerified_filter[i].Replace("'", "''");//<fix lỗi không filter được parameter có dấu "'">
                strBuilder.Append("'" + arrVerified_filter[i] + "'");
                if (i < arrVerified_filter.Count - 1)
                    strBuilder.Append(",");
            }
            return strBuilder.ToString();
        }

        public static string ArrayStringToStringFilter(string[] arrString)
        {
            return ArrayStringToStringFilter(arrString.ToList());
        }

        public static long ConvertStringToInt64(string svalue)
        {
            if (string.IsNullOrEmpty(svalue))
                return 0;
            long iValue = 0;
            if (!long.TryParse(svalue, out iValue))
                return 0;
            return iValue;
        }

        public static int ConvertStringToInt32(string svalue)
        {
            if (string.IsNullOrEmpty(svalue))
                return 0;
            int iValue = 0;
            if (!int.TryParse(svalue, out iValue))
                return 0;
            return iValue;
        }

        public static string FixSqlInjectionForFilter(string svalue)
        {
            //return string.Empty;
            try
            {
                if (svalue == null)
                    return string.Empty;
                //Regex rg = new Regex("(?<name>[a-z,A-Z,_,\\-,0-9]+)");
                Regex rg = new Regex("(?<name>.+)");

                Match m = rg.Match(svalue);

                string result = string.Empty;
                if (m.Success)
                {
                    result = m.Groups["name"].Value;
                }
                result = result.ToLower().Replace("'", "").Replace("--", "");

                //<dùng regex để replace words>
                //<phải để dấu cách để phân biệt các value có chứa các special word nay>
                result = Regex.Replace(result, "DECLARE ", "", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, "EXEC ", "", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, "sp_configure", "", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, "DELETE ", "", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, "DROP ", "", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, "CALL ", "", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, " FROM ", "", RegexOptions.IgnoreCase);

                result = result.Replace("invoke-sqlcmd", "");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string ConvertObjectToString(object objvalue)
        {
            if (objvalue == null)
                return string.Empty;
            if (objvalue == DBNull.Value)
                return string.Empty;

            string svalue = Convert.ToString(objvalue);
            return svalue;
        }

        public static int ConvertObjectToInt32(object objvalue)
        {
            if (objvalue == null)
                return 0;
            if (objvalue == DBNull.Value)
                return 0;
            string svalue = Convert.ToString(objvalue);
            return ConvertStringToInt32(svalue);
        }

        public static long ConvertObjectToInt64(object objvalue)
        {
            if (objvalue == null)
                return 0;
            if (objvalue == DBNull.Value)
                return 0;
            string svalue = Convert.ToString(objvalue);
            return ConvertStringToInt64(svalue);
        }

        //2022-08-16 @Tuan
        public static byte ConvertStringToByte(string svalue)
        {
            if (string.IsNullOrEmpty(svalue))
                return 0;
            byte bValue = 0;
            if (!byte.TryParse(svalue, out bValue))
                return 0;
            return bValue;
        }
        public static DateTime ConvertStringToDateTime(string svalue)
        {
            if (string.IsNullOrEmpty(svalue))
                return new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime dValue = new DateTime();
            if (!DateTime.TryParse(svalue, out dValue))
                return new DateTime(1900, 1, 1, 0, 0, 0);
            return dValue;
        }
        public static double ConvertStringToDouble(string svalue)
        {
            if (string.IsNullOrEmpty(svalue))
                return 0;
            double dValue = 0;
            if (!double.TryParse(svalue, out dValue))
                return 0;
            return dValue;
        }
        public static Single ConvertStringToSingle(string svalue)
        {
            if (string.IsNullOrEmpty(svalue))
                return 0;
            Single dValue = 0;
            if (!Single.TryParse(svalue, out dValue))
                return 0;
            return dValue;
        }
        public static float ConvertStringToFloat(string svalue)
        {
            if (string.IsNullOrEmpty(svalue))
                return 0;
            float dValue = 0;
            if (!float.TryParse(svalue, out dValue))
                return 0;
            return dValue;
        }
        public static bool ConvertStringToBoolean(string sValue)
        {
            bool blvalue = false;
            if (bool.TryParse(sValue, out blvalue))
                return blvalue;
            return false;
        }

        //<@van CODE_20220823>
        public static string ReplaceUsernamePassword(string connectionString)
        {
            try
            {
                string nwstr = connectionString;
                string pattern = @";(?<username>uid.*=.*;).*(?<password>pwd=.*;).*Timeout";
                Regex r = new Regex(pattern);

                Match mt = r.Match(connectionString);

                Group gr = mt.Groups["username"];
                if (gr != null)
                {
                    string s1 = gr.Value;
                    nwstr = connectionString.Replace(s1, "");
                }
                Group gr2 = mt.Groups["password"];
                if (gr2 != null)
                {
                    string s2 = gr2.Value;
                    nwstr = nwstr.Replace(s2, "");
                }
                return nwstr;
            }
            catch { }
            return connectionString;
        }
    }
}
