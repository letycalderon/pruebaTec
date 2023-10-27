using System.Text.Json.Serialization;

namespace CRUD.Modelos
{
    public class ClientesArticulos
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ArticuloId { get; set; }

        [JsonIgnore]
        public Ciente Ciente { get; set; }
        [JsonIgnore]
        public Articulos Articulos { get; set; }
    }
}
