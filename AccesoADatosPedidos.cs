using System.Text.Json;
using CadeteriaAPI.Models;
public class AccesoADatosPedidos
{

    public List<Pedido> Obtener()
    {    
        string archivoJson = "Pedidos.json";
        string json = File.ReadAllText(archivoJson);
        List<Pedido>? pedidos = JsonSerializer.Deserialize<List<Pedido>>(json);
        if(pedidos == null)
        {
            throw new Exception("Error al deserializar pedidos");
        }else
        {
            return pedidos;
        }        
    }

    public void Guardar(List<Pedido> pedidos)
    {
        string archivoJson = "Pedidos.json";
        string json = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(archivoJson, json);
    } 
}