namespace ApiConsumer
{
    /// <summary>
    /// Clase GenericResponse que será devuelta en una petición http
    /// </summary>
    /// <typeparam name="T">Objecto a retornar en la propiedad ObjectData</typeparam>
    public class GenericResponse<T>
    {
        /// <summary>
        /// Mensaje que devuelve la ejecución de un método http
        /// </summary>
        public string MessageError { get; set; }

        /// <summary>
        /// Variable booleana que indica el estado de la petición
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Respuesta de la petición, mapeado en un objecto T
        /// </summary>
        public T ObjectData { get; set; }

        /// <summary>
        /// Respuesta de la petición como string
        /// </summary>
        public string StringData { get; set; }
    }
}
