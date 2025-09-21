using System.Linq;
namespace CadeteriaAPI.Models
{
    public class Cadete
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public string Direccion {get; set;}
        public int Telefono {get; set;}

        public Cadete(int id, string nombre, string direccion, int telefono)
        {
            Id = id;
            Telefono = telefono;
            Nombre = nombre;
            Direccion = direccion;
        }

        public Cadete() { } // Constructor vacío necesario para deserialización en Web API
    }
}
