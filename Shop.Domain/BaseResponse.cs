using Shop.Domain.Enum;

namespace Shop.Domain
{
    public class BaseResponse
    {
        public string Description { get; set; }
        public object Data { get; set; }
        
        public StatusCode StatusCode { get; set; }
    }
}