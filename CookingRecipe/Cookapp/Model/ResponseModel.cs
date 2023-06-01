namespace Cookapp.Model
{
    public class ResponseModel<T>
    {
        
        public T result { get; set; }
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
    
}
