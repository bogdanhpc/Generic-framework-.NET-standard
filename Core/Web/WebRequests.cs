using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core
{
    public static class WebRequests
    {
        public static HttpWebResponse Post(string url, object content = null, KnownContentSerializers sendType = KnownContentSerializers.Json, KnownContentSerializers returnType = KnownContentSerializers.Json)
        {
            var request = WebRequest.CreateHttp(url);

            request.Method = HttpMethod.Post.ToString();

            request.Accept = returnType.ToMimeString();

            request.ContentType = sendType.ToMimeString();

            var contentString = string.Empty;

            if (content == null)
            {
                request.ContentLength = 0;
            }
            else
            {

                if (sendType == KnownContentSerializers.Xml)
                {
                    var xmlSerializer = new XmlSerializer(content.GetType());
                    using (var stringWriter = new StringWriter())
                    {
                        xmlSerializer.Serialize(stringWriter, content);
                        contentString = stringWriter.ToString();
                    }

                }
                else if (sendType == KnownContentSerializers.Json)
                {
                    contentString = JsonConvert.SerializeObject(content);
                }
            }

            using (var requestString = request.GetRequestStream())
            {
                using (var streamWriter = new StreamWriter(requestString))
                {
                    streamWriter.Write(contentString);
                }
            }

            return request.GetResponse() as HttpWebResponse;
        }

        public static WebRequestResult<TResponse> Post<TResponse>(string url, object content = null, KnownContentSerializers sendType = KnownContentSerializers.Json, KnownContentSerializers returnType = KnownContentSerializers.Json)
        {
            var serverResposne = Post(url, content, sendType, returnType);

            var result = serverResposne.CreateWebRequestResult<TResponse>();

            if (result.StatusCode != HttpStatusCode.OK)
            {
                result.ErrorMessage = $"Server returned unsuccesful status code. {serverResposne.StatusCode} {serverResposne.StatusDescription}";
                return result;
            }

            if (result.RawServerResponse.IsNullOrEmpty())
            {
                return result;
            }

            try
            {
                if (!serverResposne.ContentType.ToLower().Contains(returnType.ToMimeString().ToLower()))
                {
                    result.ErrorMessage = $"Server did not return data in expected type. Expected {returnType.ToMimeString()}, returned {serverResposne.ContentType}";
                    return result;
                }
                if (returnType == KnownContentSerializers.Json)
                {
                    result.ServerResponse = JsonConvert.DeserializeObject<TResponse>(result.RawServerResponse);
                }
                else if (returnType == KnownContentSerializers.Xml)
                {
                    var xmlSerializer = new XmlSerializer(typeof(TResponse));
                    using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result.RawServerResponse)))
                    {
                        result.ServerResponse = (TResponse)xmlSerializer.Deserialize(memoryStream);
                    }
                }
                else
                {
                    result.ErrorMessage = "Unknown return type, cannot deserialize to the expected type";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Failed to deseserialize server response to the expected type";
                return result;
            }
            return result;
        }

        public static async Task<HttpWebResponse> PostAsync(string url, object content = null, KnownContentSerializers sendType = KnownContentSerializers.Json, KnownContentSerializers returnType = KnownContentSerializers.Json)
        {
            var request = WebRequest.CreateHttp(url);

            request.Method = HttpMethod.Post.ToString();

            request.Accept = returnType.ToMimeString();

            request.ContentType = sendType.ToMimeString();

            var contentString = string.Empty;

            if (content == null)
            {
                request.ContentLength = 0;
            }
            else
            {

                if (sendType == KnownContentSerializers.Xml)
                {
                    var xmlSerializer = new XmlSerializer(content.GetType());
                    using (var stringWriter = new StringWriter())
                    {
                        xmlSerializer.Serialize(stringWriter, content);
                        contentString = stringWriter.ToString();
                    }

                }
                else if (sendType == KnownContentSerializers.Json)
                {
                    contentString = JsonConvert.SerializeObject(content);
                }
            }
            using (var requestString = await request.GetRequestStreamAsync())
            {
                using (var streamWriter = new StreamWriter(requestString))
                {
                    await streamWriter.WriteAsync(contentString);
                }
            }

            return await request.GetResponseAsync() as HttpWebResponse;
        }

        public static async Task<WebRequestResult<TResponse>> PostAsync<TResponse>(string url, object content = null, KnownContentSerializers sendType = KnownContentSerializers.Json, KnownContentSerializers returnType = KnownContentSerializers.Json)
        {
            var serverResposne = await PostAsync(url, content, sendType, returnType);

            var result = serverResposne.CreateWebRequestResult<TResponse>();

            if (result.StatusCode != HttpStatusCode.OK)
            {
                result.ErrorMessage = $"Server returned unsuccesful status code. {serverResposne.StatusCode} {serverResposne.StatusDescription}";
                return result;
            }

            if (result.RawServerResponse.IsNullOrEmpty())
            {
                return result;
            }

            try
            {
                if (!serverResposne.ContentType.ToLower().Contains(returnType.ToMimeString().ToLower()))
                {
                    result.ErrorMessage = $"Server did not return data in expected type. Expected {returnType.ToMimeString()}, returned {serverResposne.ContentType}";
                    return result;
                }
                if (returnType == KnownContentSerializers.Json)
                {
                    result.ServerResponse = JsonConvert.DeserializeObject<TResponse>(result.RawServerResponse);
                }
                else if (returnType == KnownContentSerializers.Xml)
                {
                    var xmlSerializer = new XmlSerializer(typeof(TResponse));
                    using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result.RawServerResponse)))
                    {
                        result.ServerResponse = (TResponse)xmlSerializer.Deserialize(memoryStream);
                    }
                }
                else
                {
                    result.ErrorMessage = "Unknown return type, cannot deserialize to the expected type";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Failed to deseserialize server response to the expected type";
                return result;
            }
            return result;

        }
    }
}
