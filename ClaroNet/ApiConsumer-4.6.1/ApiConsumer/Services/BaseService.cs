using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace ApiConsumer
{
    /// <summary>
    /// Clase base que contiene los métodos http
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Método encargado de realizar petición HTTP - GET
        /// </summary>
        /// <typeparam name="T">Modelo de respuesta según contrato</typeparam>
        /// <param name="apiUrl">Url/Host de la API</param>
        /// <param name="controllerMethodUrl">Controlar/acción a ejecutar</param>
        /// <param name="queryStringParameters">Dictionario de parámetros a enviar por querystring en la petición</param>
        /// <param name="specificHeaders">Diccionario de headers a enviar en la petición</param>
        /// <param name="returnJsonObject">Variable booleana para deserializar objecto si la api retorna un json</param>
        /// <returns></returns>
        protected GenericResponse<T> Get<T>(string apiUrl,
                                            string controllerMethodUrl,
                                            IDictionary<string, List<string>> queryStringParameters = null,
                                            IDictionary<string, string> specificHeaders = null,
                                            bool returnJsonObject = true)
        {
            string result = null;
            try
            {
                #region Creación de cliente HTTP, armado de parámetros a enviar por querystring (si existen) y url
                HttpClient httpClient = new HttpClient();

                string QueryStringParameters = string.Empty;
                if (queryStringParameters != null)
                {
                    foreach (KeyValuePair<string, List<string>> queryStringParameter in queryStringParameters)
                    {
                        if (queryStringParameter.Value.Count > 1)
                        {
                            QueryStringParameters += $"{queryStringParameter.Key}=";
                            foreach (string param in queryStringParameter.Value)
                            {
                                QueryStringParameters += $"{param};";
                            }
                        }
                        else
                        {
                            QueryStringParameters += $"{queryStringParameter.Key}={queryStringParameter.Value[0]}&";
                        }
                    }
                }

                string url = $"{apiUrl}/{controllerMethodUrl}?{QueryStringParameters}";
                #endregion

                #region Armado de los headers en caso de que existan
                if (specificHeaders != null)
                {
                    foreach (KeyValuePair<string, string> header in specificHeaders)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                #endregion

                #region Ejecución de petición GET, obtención de respuesta, y lectura de resultado
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                result = response.Content.ReadAsStringAsync().Result;
                #endregion

                #region Verificación de estado HTTP y resultado
                if (!response.IsSuccessStatusCode | string.IsNullOrEmpty(result))
                    throw new ExceptionHelper(response.StatusCode,
                                                  response.RequestMessage.ToString(),
                                                  string.IsNullOrEmpty(result) ? null : result);
                #endregion

                #region Creación de modelo Generic a retornar (Success = true)
                var genericResponse = new GenericResponse<T>
                {
                    MessageError = "Successful",
                    Success = true,
                    StringData = result
                };
                #endregion

                #region Deserialización JSON a modelo en caso de corresponder
                if (returnJsonObject)
                {
                    try
                    {
                        T resultDeserialize = JsonConvert.DeserializeObject<T>(result);
                        genericResponse.ObjectData = resultDeserialize;
                    }
                    catch (Exception)
                    {
                        throw new ExceptionHelper("Error in Client - Cannot deserialize object - Review model and contract");
                    }
                    
                }
                #endregion

                #region Devolución modelo genericResponse
                return genericResponse;
                #endregion
            }
            catch (Exception e)
            {
                #region Devolución modelo Generic (Success = false)
                return new GenericResponse<T>
                {
                    MessageError = e.Message,
                    Success = false,
                    StringData = result
                };
                #endregion
            }
        }

        /// <summary>
        /// Método encargado de real zizar petición HTTP - POST
        /// </summary>
        /// <typeparam name="T">Modelo de respuesta según contrato</typeparam>
        /// <param name="apiUrl">Url/Host de la API</param>
        /// <param name="controllerMethodUrl">Controlar/acción a ejecutar</param>
        /// <param name="BodyParameters">Objeto a enviar en la petición</param>
        /// <param name="specificHeaders">Diccionario de headers a enviar en la petición</param>
        /// <param name="contentHeader">Header de contenido a enviar en la petición</param>
        /// <param name="returnJsonObject">Variable booleana para deserializar objecto si la api retorna un json</param>
        /// <returns></returns>
        protected GenericResponse<T> Post<T>(string apiUrl,
                                             string controllerMethodUrl,
                                             object BodyParameters,
                                             IDictionary<string, string> specificHeaders = null,
                                             string contentHeader = "application/json",
                                             bool returnJsonObject = true)
        {
            string result = null;
            try
            {
                #region Creación de cliente HTTP, armado de url
                HttpClient httpClient = new HttpClient();
                string url = $"{apiUrl}/{controllerMethodUrl}";
                #endregion

                #region Armado del objeto a enviar en la petición segun el tipo del contenido
                HttpContent theContent = null;

                if (BodyParameters != null)
                {
                    switch (contentHeader)
                    {
                        case "application/x-www-form-urlenconded":
                            var jsonObjectFormUrlEnconded = JsonConvert.SerializeObject(BodyParameters);
                            var dictionaryObjectFormUrlEnconded = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonObjectFormUrlEnconded);
                            theContent = new FormUrlEncodedContent(dictionaryObjectFormUrlEnconded);
                            break;
                        case "application/json":
                            theContent = new ObjectContent(BodyParameters.GetType(),
                                                           BodyParameters,
                                                           new JsonMediaTypeFormatter(),
                                                           new MediaTypeHeaderValue("application/json"));
                            break;
                    }
                }
                #endregion

                #region Armado de los headers en caso de que existan
                if (specificHeaders != null)
                {
                    foreach (KeyValuePair<string, string> header in specificHeaders)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                #endregion

                #region Ejecución de petición POST, obtención de respuesta, y lectura de resultado
                HttpResponseMessage response = httpClient.PostAsync(url, theContent).Result;
                result = response.Content.ReadAsStringAsync().Result;
                #endregion

                #region Verificación de estado HTTP y resultado
                if (!response.IsSuccessStatusCode)
                    throw new ExceptionHelper(response.StatusCode,
                                                  response.RequestMessage.ToString(),
                                                  string.IsNullOrEmpty(result) ? null : result);
                #endregion

                #region Creación de modelo Generic a retornar (Success = true)
                var genericResponse = new GenericResponse<T> { Success = true };
                #endregion

                #region Devolución modelo genericResponse sin data
                if (string.IsNullOrEmpty(result))
                {
                    genericResponse.MessageError = "No data";
                    return genericResponse;
                }
                #endregion

                #region Devolución modelo genericResponse con data y deserialización JSON a modelo en caso de corresponder
                else
                {
                    genericResponse.MessageError = "Successful";
                    genericResponse.StringData = result;
                    if (returnJsonObject)
                    {
                        try
                        {
                            T resultDeserialize = JsonConvert.DeserializeObject<T>(result);
                            genericResponse.ObjectData = resultDeserialize;
                        }
                        catch (Exception)
                        {
                            throw new ExceptionHelper("Error in Client - Cannot deserialize object - Review model and contract");
                        }
                    }

                    return genericResponse;
                }
                #endregion

            }
            catch (Exception e)
            {
                #region Devolución modelo Generic (Success = false)
                return new GenericResponse<T>
                {
                    MessageError = e.Message,
                    Success = false,
                    StringData = result
                };
                #endregion
            }
        }
    }
}
