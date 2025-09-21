namespace CadeteriaAPI.Models
{
    public enum EstadoPedido
    {
        Pendiente,
        Entregado,
        Cancelado
    }

    public class Pedido
    {    

        public int Nro { get; set; }
        public string Observaciones { get; set; }
        public Cliente Cliente { get; set; }
        public EstadoPedido Estado { get; set; }
        public Cadete? CadeteAsignado { get; set;}

        public Pedido() { }
        
        public Pedido(int nro, string obs, Cliente cliente, EstadoPedido estado)
        {
            Nro = nro;
            Observaciones = obs;
            Cliente = cliente;
            Estado = estado;
            CadeteAsignado = null;
        }
        
        public bool EstaEntregado()
        {
            return Estado == EstadoPedido.Entregado;
        }

        public void CambiarEstado(EstadoPedido nuevoEstado)
        {
            Estado = nuevoEstado;
        }

        public string VerPedido()
        {
            return $"Pedido #{Nro}: {Observaciones} | Estado: {Estado} | Cliente: {Cliente.VerDatosCliente()}";
        }

    }
}
