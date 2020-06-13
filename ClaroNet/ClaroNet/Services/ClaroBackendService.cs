using ApiConsumer;
using ClaroNet.Services.Response;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClaroNet.Services
{
    public class ClaroBackendService : BaseService
    {
        public async Task<GenericResponse<LoginResponse>> login(string email, string pass)
        {

            string urlbase = "https://backendclaro.herokuapp.com";
            string route = "login";
            var res = base.Post<LoginResponse>(urlbase, route, new { email = email.Trim(), password = pass.Trim() });
            return res;

        }
    
        public async Task<GenericResponse<RecargasResponse.RecargasResponse>> getRecargas(Pcr PuntoAsignado)
        {
            string urlbase = "https://backendclaro.herokuapp.com";
            string route = $"Recargas";
            IDictionary<string, List<string>> parameters = new Dictionary<string, List<string>>();
            parameters.Add("nombreDelPunto", new List<string> { PuntoAsignado.NombreDelPunto });
            var result = base.Get<RecargasResponse.RecargasResponse>(urlbase, route,parameters );
            return result;
        }
    
        public async Task<GenericResponse<RecargasResponse.RecargasResponse>> SaveRecargas(RecargasRquest.RecargasRequest datos)
        {
            string urlbase = "https://backendclaro.herokuapp.com";
            string route = $"Recargas";
            var respo=base.Post<RecargasResponse.RecargasResponse>(urlbase, route, datos);
            return respo;
        }
    
    }

}
namespace ClaroNet.Services.Response
{
    using Newtonsoft.Json;

    public partial class LoginResponse
    {
        [JsonProperty("mensagge")]
        public string Mensagge { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("datosusuario")]
        public Datosusuario Datosusuario { get; set; }

        [JsonProperty("pcr")]
        public Pcr Pcr { get; set; }
    }

    public partial class Datosusuario
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellido")]
        public string Apellido { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("rol")]
        public string Rol { get; set; }
    }

    public partial class Pcr
    {
        [JsonProperty("nombreDelPunto")]
        public string NombreDelPunto { get; set; }

        [JsonProperty("numeroHabilitado")]
        public long NumeroHabilitado { get; set; }

        [JsonProperty("pin")]
        public long Pin { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }
    }
}

namespace ClaroNet.Services.RecargasResponse
{

    public partial class RecargasResponse
    {
        [JsonProperty("mensagge")]
        public string Mensagge { get; set; }

        [JsonProperty("data")]
        public List<DatosRecargas> Data { get; set; }

      

    }

    public partial class DatosRecargas
    {
        [JsonProperty("pcr")]
        public Pcr Pcr { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("fecha")]
        public string Fecha { get; set; }

        public string MensajeTelefono
        {
            get
            {
                try
                {
                    string datosVisible = string.Empty;
                    if (Data.Contains("fallo"))                    
                        return Data?.Split('.')?[0];                    
                    return Data?.Split('.')?[1];
                }
                catch (System.Exception)
                { 
                    return Data;
                }
               
             
            }

        }
       

    }

    public partial class Pcr
    {
        [JsonProperty("nombreDelPunto")]
        public string NombreDelPunto { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }
    }


}

namespace ClaroNet.Services.RecargasRquest
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class RecargasRequest
    {
        [JsonProperty("pcr")]
        public Pcr Pcr { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("fecha")]
        public string Fecha { get; set; }


    }

    public partial class Pcr
    {
        [JsonProperty("nombreDelPunto")]
        public string NombreDelPunto { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }
    }
}

