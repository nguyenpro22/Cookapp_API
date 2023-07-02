using Cookapp_API.DataAccess.DTO.AllInOneDTO.AccountDTO;

namespace Cookapp_API.DataAccess.DTO.AllInOneDTO
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public int ResponseCode { get; set; } = 0;
        public string ResponseMessage { get; set; } = string.Empty;       
     
    }
    public class ResponseModel<T> : ResponseModel
    {
        public T Result { get; set; }
    }
    public class ResponseListModel<T> : ResponseModel<IList<T>>
    {

    }
}
