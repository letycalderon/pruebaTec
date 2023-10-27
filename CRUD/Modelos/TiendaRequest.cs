namespace CRUD.Modelos
{
    public class TiendaRequest
    {
        public string Sucursal { get; set; }
        public string Dirección { get; set; }
        public List<Articulos> Articulos { get; set; }
    }
}
