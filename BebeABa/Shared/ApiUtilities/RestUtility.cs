using Newtonsoft.Json;
using Shared.Enums;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiUtilities
{
    public class RestUtility
    {
        public static async Task<object> WebServiceAsync<T>
            (string url, string operation, object requestBodyObject, string method, string username, string password) where T : class
        {
            try
            {
                var webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.Method = method;
                webReq.ContentType = "application/json";
                webReq.MediaType = "application/json";
                webReq.Accept = "application/json";

                //Add basic authentication header if username is supplied
                if (!string.IsNullOrEmpty(username))
                { webReq.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password)); }

                //Add key to header if operation is supplied
                if (!string.IsNullOrEmpty(operation))
                { webReq.Headers["Operation"] = operation; }

                if (requestBodyObject != null)
                {
                    var requestBody = JsonConvert.SerializeObject(requestBodyObject);
                    webReq.ContentLength = requestBody.Length;
                    var streamWriter = new StreamWriter(webReq.GetRequestStream(), Encoding.ASCII);
                    await streamWriter.WriteAsync(requestBody);
                    streamWriter.Close();
                }

                var response = await webReq.GetResponseAsync();
                var streamReader = new StreamReader(response.GetResponseStream());
                var responseContent = (await streamReader.ReadToEndAsync()).Trim();
                var jsonObject = JsonConvert.DeserializeObject<T>(responseContent);

                return jsonObject;
            }
            catch (Exception ex)
            { var msg = ex.Message; }

            return default;
        }

        public static async Task<Response> WebServiceAsync(string url, string operation, object requestBodyObject, string method, string username, string password)
        {
            var response = new Response();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(15);
                    var getResponse = await GetResponse(client, url, method, requestBodyObject);
                    var responseJson = await getResponse.Content.ReadAsStringAsync();

                    response = JsonConvert.DeserializeObject<Response>(responseJson);
                }
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.BadRequest;
                response.Message = ex.Message;
            }

            return response;
        }

        private static async Task<HttpResponseMessage> GetResponse(HttpClient client, string url, string method, object value)
        {
            HttpResponseMessage response = null;
            StringContent content = null;

            if (method.ToUpper() != "GET" && method.ToUpper() != "DELETE" && value != null)
            {
                var request = JsonConvert.SerializeObject(value);
                content = new StringContent(request, Encoding.UTF8, "application/json");
            }

            switch (method.ToUpper())
            {
                case "GET":
                    response = await client.GetAsync(url);
                    break;
                case "POST":
                    response = await client.PostAsync(url, content);
                    break;
                case "PUT":
                    response = await client.PutAsync(url, content);
                    break;
                case "DELETE":
                    response = await client.DeleteAsync(url);
                    break;
            }

            return response;
        }
    }
}
