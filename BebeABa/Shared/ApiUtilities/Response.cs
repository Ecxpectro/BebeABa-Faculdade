using Shared.Enums;

namespace Shared.ApiUtilities
{
    public class Response
    {
        public StatusCode Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
