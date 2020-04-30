using System.Net;

namespace Core
{
    public class WebRequestResult
    {
        public bool Seccesful => ErrorMessage == null;

        public string ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public string ContentType { get; set; }

        public WebHeaderCollection Headers { get; set; }

        public CookieCollection Cookies { get; set; }

        public string RawServerResponse { get; set; }

        public object ServerResponse { get; set; }
    }

    public class WebRequestResult<T> : WebRequestResult
    {
        public new T ServerResponse { get; set; }
    }
}
