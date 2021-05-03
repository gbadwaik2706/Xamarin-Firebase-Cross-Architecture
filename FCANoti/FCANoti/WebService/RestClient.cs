using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FCANoti.WebService
{
    public class RestClient<T, R> : IRestClient<T, R> where T : class, new()
    {
        private HttpClient HttpClient;

        public RestClient()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri($"{Locator.BaseApiUrl}/");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// JSON Support Only
        /// </summary>
        /// <param name="apiRoute"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public async Task<R> Get(string apiRoute, MediaType mediaType)
        {
            var httpResponse = await HttpClient.GetAsync(apiRoute);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                R response = JsonConvert.DeserializeObject<R>(responseContent);

                return response;
            }
            else
            {
                return default(R);
            }
        }

        /// <summary>
        /// Only JSON Supported
        /// </summary>
        /// <param name="parameterModel"></param>
        /// <param name="apiRoute"></param>
        /// <param name="mediaType"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public async Task<R> Post(T parameterModel, string apiRoute, MediaType mediaType, Encoding encoding)
        {
            try
            {
                var strJson = JsonConvert.SerializeObject(parameterModel);
                var httpContent = new StringContent(strJson, encoding, GetEnumDescription(mediaType));

                var httpResponse = await HttpClient.PostAsync(apiRoute, httpContent);
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    R response = JsonConvert.DeserializeObject<R>(responseContent);
                    return response;
                }
                else
                {
                    return default(R);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<R> Put(T parameter)
        {

            throw new NotImplementedException();
        }
        public Task<R> Delete(T parameter)
        {

            throw new NotImplementedException();
        }

        string GetEnumDescription(MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.Html:
                    return "text/html";
                case MediaType.Json:
                    return "application/json";
                case MediaType.Xml:
                    return "application/xml";
                case MediaType.ApiJson:
                    return "application/vnd.api+json";
            }
            return "";
        }

    }

    public enum MediaType
    {
        [Description("text/html")]
        Html,
        [Description("application/json")]
        Json,
        [Description("application/xml")]
        Xml,
        [Description("application/vnd.api+json")]
        ApiJson
    }

    public enum DownloadMediaType
    {
        [Description("image/gif")]
        GIF,
        [Description("audio/vorbis")]
        Vorbis,
        [Description("video/mpeg")]
        Mpeg
    }
}
