using InsuranceApi.Domain.BusinessObjects.MessagesService;
using InsuranceApi.Domain.Common.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace InsuranceApi.Services.Rest.Client {
    public class HttpRestClient {

        private string _baseAddress;
        private string _mediaTypeAccept;

        private Encoding _encoding;

        //private const string ERROR_AUTHORIZATION_MESSAGE = "Authorization has been denied for this request.";

        public HttpMessageHandler HttpMessageHandler { get; set; }

        private enum RequestVerb {
            Post,
            Put,
            Delete,
            Post2
        }

        private CookieContainer _cookies;
        public CookieContainer Cookies {
            get {
                return _cookies;
            }
        }

        public HttpRestClient(string baseAddress)
            : this(baseAddress, "application/json") {
        }

        public HttpRestClient(string baseAddress, string mediaTypeAccept)
            : this(baseAddress, mediaTypeAccept, new CookieContainer()) {
        }

        public HttpRestClient(string baseAddress, string mediaTypeAccept, CookieContainer cookies)
            : this(baseAddress, mediaTypeAccept, cookies, Encoding.UTF8) {
        }

        public HttpRestClient(string baseAddress,
                              string mediaTypeAccept,
                              CookieContainer cookies,
                              Encoding encoding) {
            _baseAddress = baseAddress;
            _mediaTypeAccept = mediaTypeAccept;
            _cookies = cookies;
            _encoding = encoding;
        }




        public TResponse Get<TResponse>(string requestUri)
            where TResponse : BaseResponse, new() {
            return Get<BaseRequest, TResponse>(requestUri, default(BaseRequest), true);
        }

        public TResponse Get<TRequest, TResponse>(string requestUri, TRequest request)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest {
            return Get<TRequest, TResponse>(requestUri, request, true);
        }

        public TResponse Get<TRequest, TResponse>(string requestUri, TRequest request, bool autoFormat)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest {
            return Get<TRequest, TResponse>(requestUri, request, true, 0);
        }

        public TResponse Get<TRequest, TResponse>(string requestUri, TRequest request, bool autoFormat, int timeOut)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest {
            //bool needRefreshToken;
            //int i = 0;
            //do {
            using (var handler = GetMessageHandler()) {
                using (var httpClient = new HttpClient(handler)) {
                    if (timeOut > 0 && httpClient.Timeout != TimeSpan.FromMilliseconds(timeOut))
                        httpClient.Timeout = TimeSpan.FromMilliseconds(timeOut);

                    httpClient.BaseAddress = new Uri(_baseAddress);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaTypeAccept));


                    if (request != null) {
                        AddRequestHeaders(request, httpClient);
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "Keep-Alive");
                        if (autoFormat) {
                            var querystring = new StringBuilder();
                            var jsonObject = Newtonsoft.Json.Linq.JObject.FromObject(request);
                            foreach (var obj in jsonObject) {
                                if (obj.Value != null && (obj.Value.Type == JTokenType.String || obj.Value.Type == JTokenType.Integer))
                                    querystring.Append(string.Format("{0}={1}&", obj.Key, HttpUtility.UrlEncode(obj.Value.ToString())));
                                else if (obj.Value != null && obj.Value.Type == JTokenType.Boolean)
                                    querystring.Append(string.Format("{0}={1}&", obj.Key, obj.Value.Value<bool>() == true ? "1" : "0"));
                            }
                            if (querystring.Length > 0)
                                requestUri += (requestUri.Contains("?") ? "&" : "?") + querystring.Remove(querystring.Length - 1, 1).ToString();
                        }
                    }
                    HttpResponseMessage responseMessage = httpClient.GetAsync(requestUri).Result;
                    string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                    TResponse response = new TResponse();
                    try {


                        //-- Caso seja um RawResponse devolve os dados sem deserializar, pois senão dá erro.
                        //-- Caso por exemplo dos motivos de endosso, que são retornados como array de string e não o objeto padrão
                        if (typeof(TResponse) != typeof(RawResponse)) {
                            var converter = new JsonConverter();
                            if (converter != null) {
                                response = JsonConvert.DeserializeObject<TResponse>(responseContent, converter);
                            } else {
                                response = JsonConvert.DeserializeObject<TResponse>(responseContent);
                            }
                        }
                        //response.RequestHeader = httpClient.DefaultRequestHeaders;
                    } catch (Exception ex) {
                        StringBuilder builder = new StringBuilder();
                        builder.AppendFormat("Exception:{0}", ex.Message);
                        builder.AppendLine();
                        builder.AppendFormat("Type:{0}", typeof(TResponse).FullName);
                        builder.AppendLine();
                        builder.AppendFormat("Content:{0}", responseContent);
                        response.Erro += builder.ToString();
                    }
                    response.Conteudo = responseContent;
                    response.StatusCode = string.Concat(Convert.ToInt32(responseMessage.StatusCode).ToString(), '-', responseMessage.StatusCode);
                    response.ResponseHeader = responseMessage.Headers.ToString();
                    response.RequestHeader = httpClient.DefaultRequestHeaders.ToString();
                    response.Method = responseMessage.RequestMessage.Method.ToString();
                    response.requestUri = responseMessage.RequestMessage.RequestUri.ToString();

                    AddResponseHeaders<TResponse>(response, responseMessage);
                    //needRefreshToken = VerifyNeedRefreshToken(responseContent, request, i++);
                    //if (!needRefreshToken)
                    return VerifyError(response, responseMessage, responseContent);
                }
            }
            //} while (needRefreshToken);
            //return null; //
        }

        public TResponse Post<TRequest, TResponse>(string requestUri, TRequest body)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest {
            return DoRequest<TRequest, TResponse>(requestUri, body, RequestVerb.Post, 0);
        }
        public TResponse Post<TRequest, TResponse>(string requestUri, TRequest body, int timeOut)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest {
            return DoRequest<TRequest, TResponse>(requestUri, body, RequestVerb.Post, timeOut);
        }

        public TResponse Put<TRequest, TResponse>(string requestUri, TRequest body)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest {
            return DoRequest<TRequest, TResponse>(requestUri, body, RequestVerb.Put, 0);
        }
        public TResponse Put<TRequest, TResponse>(string requestUri, TRequest body, int timeOut)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest {
            return DoRequest<TRequest, TResponse>(requestUri, body, RequestVerb.Put, timeOut);
        }

        public TResponse Delete<TRequest, TResponse>(string requestUri, TRequest body)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest {
            return DoRequest<TRequest, TResponse>(requestUri, body, RequestVerb.Delete, 0);
        }
        public TResponse Delete<TRequest, TResponse>(string requestUri, TRequest body, int timeOut)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest {
            return DoRequest<TRequest, TResponse>(requestUri, body, RequestVerb.Delete, timeOut);
        }

        private TResponse DoRequest<TRequest, TResponse>(string requestUri, TRequest body, RequestVerb verb, int timeOut)
            where TResponse : BaseResponse, new()
            where TRequest : BaseRequest {
            string jsonObject = JsonConvert.SerializeObject(body.BodyObject, new JsonConverter());
            //string jsonObject = JsonConvert.SerializeObject(body.BodyObject,
            //                Newtonsoft.Json.Formatting.None,
            //                new JsonSerializerSettings {
            //                    NullValueHandling = NullValueHandling.Ignore
            //                });

            using (var handler = GetMessageHandler()) {
                using (var httpClient = new HttpClient(handler)) {
                    if (timeOut > 0 && httpClient.Timeout != TimeSpan.FromMilliseconds(timeOut))
                        httpClient.Timeout = TimeSpan.FromMinutes(10);
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                    httpClient.BaseAddress = new Uri(_baseAddress);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaTypeAccept));

                    AddRequestHeaders(body, httpClient);
                    HttpResponseMessage responseMessage = new HttpResponseMessage();
                    StringContent stringContentJson = null;
                    if (_encoding == null) {
                        stringContentJson = new StringContent(jsonObject);
                        stringContentJson.Headers.ContentType = new MediaTypeWithQualityHeaderValue(_mediaTypeAccept);
                    } else {
                        stringContentJson = new StringContent(jsonObject, Encoding.UTF8, _mediaTypeAccept);
                    }

                    switch (verb) {
                        case RequestVerb.Post:
                            responseMessage = httpClient.PostAsync(requestUri, stringContentJson).Result;
                            break;

                        case RequestVerb.Put:
                            responseMessage = httpClient.PutAsync(requestUri, stringContentJson).Result;
                            break;

                        case RequestVerb.Delete:
                            responseMessage = httpClient.DeleteAsync(requestUri).Result;
                            break;
                    }

                    //TODO: Verificar uma melhor forma de checar o retorno em caso de falha, e obter uma mensagem mais detalhada
                    //if (!responseMessage.IsSuccessStatusCode) {
                    //    throw new Exception("Falha na chamada do serviço.");
                    //}
                    //TODO: Foi comentado para que o serviço de Assinatura Digital da Ebix funcione junto com os demais serviços

                    string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                    TResponse response = new TResponse();
                    try {
                        if (!responseContent.IsNullOrEmpty())
                            response = JsonConvert.DeserializeObject<TResponse>(responseContent, new JsonConverter());

                    } catch (Exception ex) {
                        StringBuilder builder = new StringBuilder();
                        builder.AppendFormat("Exception:{0}", ex.Message);
                        builder.AppendLine();
                        builder.AppendFormat("Type:{0}", typeof(TResponse).FullName);
                        builder.AppendLine();
                        builder.AppendFormat("Content:{0}", responseContent);
                        response.Erro += builder.ToString();

                    }
                    response.Conteudo = responseContent;
                    response.StatusCode = string.Concat(Convert.ToInt32(responseMessage.StatusCode).ToString(), '-', responseMessage.StatusCode);
                    response.ResponseHeader = responseMessage.Headers.ToString();
                    response.RequestHeader = httpClient.DefaultRequestHeaders.ToString();
                    response.Method = responseMessage.RequestMessage.Method.ToString();
                    response.requestUri = responseMessage.RequestMessage.RequestUri.ToString();

                    AddResponseHeaders<TResponse>(response, responseMessage);
                    return VerifyError(response, responseMessage, responseContent);
                }
            }
        }

        private HttpMessageHandler GetMessageHandler() {
            if (HttpMessageHandler != null)
                return HttpMessageHandler;

            return new HttpClientHandler() { CookieContainer = _cookies };
        }

        private void AddRequestHeaders<TRequest>(TRequest request, HttpClient httpClient)
            where TRequest : BaseRequest {
            request.DefaultRequestHeaders.ToList().ForEach(keyValue => {
                if (!string.IsNullOrEmpty(keyValue.Value)) {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(keyValue.Key, keyValue.Value);
                }
            });
        }

        private void AddResponseHeaders<TResponse>(TResponse response, HttpResponseMessage responseMessage)
          where TResponse : BaseResponse, new() {
            string[] headersDefaults = new string[] { "Cache-Control", "Date", "Expires", "Pragma", "Content-Length", "Content-Type", "Server", "X-AspNet-Version", "X-Powered-By", "X-SourceFiles" };
            if (responseMessage.Headers != null) {
                responseMessage.Headers.ToList().ForEach(keyValue => {
                    if (!headersDefaults.Contains(keyValue.Key)) {
                        List<string> keyValuesList = keyValue.Value.ToList();
                        if (keyValuesList.Count > 1) {
                            response.DefaultResponseHeaders.Add(keyValue.Key, string.Join(",", keyValue.Value.ToArray()));
                        } else {
                            if (keyValuesList.Count > 0) {
                                response.DefaultResponseHeaders.Add(keyValue.Key, keyValue.Value.Single());
                            }
                        }
                    }
                });
            }
        }

        public string Post(string requestUri, string body) {
            return DoRequest(requestUri, body, RequestVerb.Post);
        }

        public string Delete(string requestUri, string body) {
            return DoRequest(requestUri, body, RequestVerb.Delete);
        }

        public string Put(string requestUri, string body) {
            return DoRequest(requestUri, body, RequestVerb.Put);
        }

        private string DoRequest(string requestUri, string body, RequestVerb verb) {
            using (var handler = new HttpClientHandler() { CookieContainer = _cookies }) {
                using (var httpClient = new HttpClient(handler)) {
                    httpClient.BaseAddress = new Uri(_baseAddress);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaTypeAccept));
                    HttpResponseMessage responseMessage = new HttpResponseMessage();

                    switch (verb) {
                        case RequestVerb.Post:
                            responseMessage = httpClient.PostAsync(requestUri, new StringContent(body, Encoding.UTF8, _mediaTypeAccept)).Result;
                            break;

                        case RequestVerb.Delete:
                            responseMessage = httpClient.DeleteAsync(requestUri).Result;
                            break;

                        case RequestVerb.Put:
                            responseMessage = httpClient.PutAsync(requestUri, new StringContent(body, Encoding.UTF8, _mediaTypeAccept)).Result;
                            break;
                    }

                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
        }

        private void AdicionaMensagemErro(string codigo, string conteudo, BaseResponse response) {
            response.Erro += string.Concat(codigo, conteudo);
        }

        private TResponse VerifyError<TResponse>(TResponse response, HttpResponseMessage responseMessage, string responseContent)
            where TResponse : BaseResponse {
            switch (responseMessage.StatusCode) {
                case System.Net.HttpStatusCode.BadRequest:
                    AdicionaMensagemErro("HTTP-400", "Request mal formado.", response);
                    break;
                case System.Net.HttpStatusCode.Forbidden:
                    AdicionaMensagemErro("HTTP-403", "Request proibido. O servidor recusou a requisição. ", response);
                    break;
                case System.Net.HttpStatusCode.InternalServerError:
                    AdicionaMensagemErro("HTTP-500", "Ocorreu um erro interno ao servidor." + responseMessage.ReasonPhrase + ". Retorno: " + responseContent, response);
                    break;
                case System.Net.HttpStatusCode.NotFound:
                    AdicionaMensagemErro("HTTP-404", "Recurso não encontrado no servidor.", response);
                    break;
                case System.Net.HttpStatusCode.ServiceUnavailable:
                    AdicionaMensagemErro("HTTP-503", "Serviço indisponível.", response);
                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                    AdicionaMensagemErro("HTTP-401", "O acesso a este recurso não está autorizado.", response);
                    break;
            }

            return response;
        }
    }

    public class JsonConverter : DateTimeConverterBase {
        private string formatDate = JsonSerializerConfig.JsonFormatDate;
        private string cultureInfo = JsonSerializerConfig.JsonCultureInfo;
        private CultureInfo culture;

        public JsonConverter() {
            culture = CultureInfo.CreateSpecificCulture(cultureInfo);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.Value != null) {
                return DateTime.Parse(reader.Value.ToString(), culture);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value != null) {
                writer.WriteValue(((DateTime)value).ToString(formatDate));
            }
        }
    }

    public static class JsonSerializerConfig {
        private static string jsonFormatDate = ConfigurationManager.AppSettings["JsonFormatDate"];
        private static string jsonCultureInfo = ConfigurationManager.AppSettings["JsonCultureInfo"];

        public static string JsonFormatDate {
            get {
                if (string.IsNullOrEmpty(jsonFormatDate)) {
                    return DateTimeConfig.DATE_TIME_FORMAT;
                }

                return jsonFormatDate;
            }
        }
        public static string JsonCultureInfo {
            get {
                if (string.IsNullOrEmpty(jsonCultureInfo)) {
                    return DateTimeConfig.CULTURE_INFO;
                }

                return jsonCultureInfo;
            }
        }
    }

    public static class DateTimeConfig {
        public const string DATE_TIME_FORMAT = "yyyy-MM-ddTHH:mm:ssZ";//"dd/MM/yyyy HH:mm:ss";
        public const string CULTURE_INFO = "en-US"; //"pt-BR";
    }
}
