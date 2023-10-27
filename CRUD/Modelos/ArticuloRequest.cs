namespace CRUD.Modelos
{
    public class ArticuloRequest
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Precio { get; set; }
        public string Imagen { get; set; }
        public int Stock { get; set; }

        public Guid TiendaId { get; set; }

    }
}
