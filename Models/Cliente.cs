namespace CadeteriaAPI.Models
{
    public class Cliente
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string DatosReferenciaDireccion { get; set; }

        public Cliente() { } // Necesario para deserialización automática

        public Cliente(string nombre, string direccion, int telefono, string referencia)
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            DatosReferenciaDireccion = referencia;
        }

        public string VerDireccionCliente()
        {
            return $"{Direccion} - {DatosReferenciaDireccion}";
        }

        public string VerDatosCliente()
        {
            return $"Nombre: {Nombre} | Teléfono: {Telefono} | Dirección: {VerDireccionCliente()}";
        }
    }
}
