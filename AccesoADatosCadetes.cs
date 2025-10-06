using System.Text.Json;
using CadeteriaAPI.Models;
public class AccesoADatosCadetes
{

    public List<Cadete> Obtener()
    {    
        string archivoJson = "Cadetes.json";
        string json = File.ReadAllText(archivoJson);
        List<Cadete>? cadetes = JsonSerializer.Deserialize<List<Cadete>>(json);
        if(cadetes == null)
        {
            throw new Exception("Error al deserializar cadetes");
        }else
        {
            return cadetes;
        }
    }
}