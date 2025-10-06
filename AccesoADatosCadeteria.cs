using System.Text.Json;
using CadeteriaAPI.Models;
public class AccesoADatosCadeteria
{

    public Cadeteria Obtener()
    {    
        string archivoJson = "Cadeteria.json";
        string json = File.ReadAllText(archivoJson);
        Cadeteria? cadeteria = JsonSerializer.Deserialize<Cadeteria>(json);
        if(cadeteria == null)
        {
            throw new Exception("Error al deserializar cadetería");
        }else
        {
            return cadeteria;
        }
    }
}