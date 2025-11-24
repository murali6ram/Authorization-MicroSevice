using System.Net;

namespace Common.Response
{
    public class BaseResponse
    {
        /// <summary>
        /// Code Property
        /// </summary>
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// Message Property
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// Data Property
        /// </summary>
        public object Data { get; set; } = new { };

        /// <summary>
        /// Create Ok Response
        /// </summary>
        /// <param name="data">object</param>
        /// <param name="message">string</param>
        /// <returns></returns>
        public static BaseResponse OK(object data, string message = "") => CreateResponse(HttpStatusCode.OK, message: message, data: data);

        /// <summary>
        /// Create Fobidden Response 
        /// </summary>
        /// <param name="message">string</param>
        /// <returns></returns>
        public static BaseResponse Forbidden(string message = "") => CreateResponse(HttpStatusCode.Forbidden, message: message);

        /// <summary>
        /// Create Response Based on Code ,Message and Data
        /// </summary>
        /// <param name="code">HttpStatusCode</param>
        /// <param name="message">string</param>
        /// <param name="data">object</param>
        /// <returns></returns>
        public static BaseResponse CreateResponse(HttpStatusCode code = HttpStatusCode.OK, string message = "", object? data = null)
        {
            return new BaseResponse
            {
                Code = code,
                Message = message,
                Data = data ?? new { }
            };
        }
    }
}
