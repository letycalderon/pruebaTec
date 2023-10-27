namespace CRUD.Modelos
{
    public class Tienda
    {
        public Guid Id { get; set; }
        public string Sucursal { get; set; }
        public string Dirección { get; set; }


        public List<Articulos> Articulos { get; set; }
    }
}
