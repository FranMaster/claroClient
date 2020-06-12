using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace ApiConsumer
{
    /// <summary>
    /// Custom Exception para generar mensaje controlado cuando falla una petición http
    /// </summary>
    public class ExceptionHelper : Exception
    {
        /// <summary>
        /// Custom Exception para generar mensaje controlado cuando falla una petición http
        /// </summary>
        /// <param name="statusCode">Codigo de estado de respuesta</param>
        /// <param name="message">Request message</param>
        /// <param name="responseContent">Contenido de respuesta</param>
        public ExceptionHelper(HttpStatusCode statusCode, string message, string responseContent = null) :
        base(createMessage(statusCode, message, responseContent))
        { }

        private static string createMessage(HttpStatusCode statusCode, string message, string responseContent = null)
        {
            string responseContentString = string.Empty;
            if (responseContent != null)
            {
                try
                {
                    var dictionaryObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
                    foreach (KeyValuePair<string, string> obj in dictionaryObject)
                    {
                        responseContentString += " - " + obj.Key + ": " + obj.Value;
                    }
                }
                catch
                {
                    responseContentString = responseContent;
                }
            }
            return $"Request failed with status code: {((int)statusCode).ToString()}-{statusCode} - {message.Replace(",", " -")}{responseContentString}";
        }

        public ExceptionHelper(string message) : base(message) { }
    }
}
