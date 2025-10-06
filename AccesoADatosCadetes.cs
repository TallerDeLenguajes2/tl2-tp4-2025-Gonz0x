using System.Text.Json;

public class AccesoADatosCadetes
{

    public List<Cadete> Obtener()
    {    
        string archivoJson = "Cadetes.json";
        string json = File.ReadAllText(archivoJson);
        LList<Cadete>? cadetes = JsonSerializer.Deserialize<List<Cadete>>(json);
        if(cadetes == null)
        {
            throw new Exception("Error al deserializar cadetes");
        }else
        {
            return cadetes;
        }
    }
}