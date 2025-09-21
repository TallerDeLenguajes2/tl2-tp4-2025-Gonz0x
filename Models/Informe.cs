namespace CadeteriaAPI.Models
{
    public class Informe
    {
        public int TotalPedidosEntregados { get; set; }
        public double PromedioEntregasPorCadete { get; set; }
        public List<JornalCadete> Jornales { get; set; } = new();
    }
}

