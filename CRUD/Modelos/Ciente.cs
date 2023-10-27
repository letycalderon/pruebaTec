﻿namespace CRUD.Modelos
{
    public class Ciente
    {
        public Guid Id {  get; set; }
        public string Username { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }

        public List<ClientesArticulos> ClientesArticulos { get; set; }
    }
}
