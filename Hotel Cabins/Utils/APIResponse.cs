using System.Net;

namespace Hotel_Cabins.Utils
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorsList { get; set; }
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
    }
}
