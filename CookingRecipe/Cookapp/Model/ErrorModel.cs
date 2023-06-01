namespace Cookapp.Model
{
    public class ErrorModel
    {
        public const int SUCCESS = 0;
        public const int LOGIN_FAILED = 1;
        public static string GetMessage(int statuscode)
        {
            switch (statuscode)
            {
                case SUCCESS:
                    return "success";
                    break;
                case LOGIN_FAILED:
                    return "username or password is invalid";
                    break;
                default:
                    return "";
                    break;
            }
        }
    }
}
