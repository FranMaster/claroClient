using ApiConsumer;
using ClaroNet.Services.Response;
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
            var res = base.Post<LoginResponse>(urlbase, route, new { email = email.Trim(), password = pass.Trim() }, null, "application/x-www-form-urlenconded");
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

    using Newtonsoft.Json;

    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class RecargasResponse
    {
        [JsonProperty("mensagge")]
        public string Mensagge { get; set; }

        [JsonProperty("data")]
        public List<DatosRecargas> Data { get; set; }
    }

    public partial class DatosRecargas
    {
        [JsonProperty("nombreDelPunto")]
        public string NombreDelPunto { get; set; }

        [JsonProperty("NumeroARecargar")]
        public string NumeroARecargar { get; set; }

        [JsonProperty("monto")]
        public long Monto { get; set; }

        [JsonProperty("hora")]
        public string Hora { get; set; }

        [JsonProperty("fecha")]
        public string Fecha { get; set; }

        [JsonProperty("ubicacion")]
        public Ubicacion Ubicacion { get; set; }
    }

    public partial class Ubicacion
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }
    }
}



