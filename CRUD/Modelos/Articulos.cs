using System.Text.Json.Serialization;

namespace CRUD.Modelos
{
    public class Articulos
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Precio { get; set; }
        public string Imagen { get; set; }
        public int Stock { get; set; }

        public Guid TiendaId { get; set; }

        [JsonIgnore]
        public Tienda Tienda { get; set; }

        public List<ClientesArticulos> ClientesArticulos { get; set; }

    }

}
