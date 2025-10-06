using System.Text;
using System.Linq;
namespace CadeteriaAPI.Models
{
    public class Cadeteria
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        private List<Cadete> listadoCadetes;
        private List<Pedido> listadoPedidos;

        public List<Cadete> Cadetes => listadoCadetes;
        public List<Pedido> Pedidos => listadoPedidos;

        public Cadeteria(string nombre, string telefono)
        {
            Nombre = nombre;
            Telefono = telefono;
            listadoCadetes = new List<Cadete>();
            listadoPedidos = new List<Pedido>();
        }

        public string AgregarPedido(int nro, string obs, string nombre, string direccion, int telefono, string referencia)
        {
            Cliente cliente = new Cliente(nombre, direccion, telefono, referencia);
            Pedido pedido = new Pedido(nro, obs, cliente, EstadoPedido.Pendiente);
            listadoPedidos.Add(pedido);
            return $"Pedido #{nro} agregado correctamente.";
        }
        
        public void AltaPedido(Pedido pedido)
        {
            Pedidos.Add(pedido);
        }

        public string AsignarPedido(int nroPedido, int idCadete)
        {
            var pedido = listadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);
            var cadete = listadoCadetes.FirstOrDefault(c => c.Id == idCadete);

            if (pedido != null && cadete != null)
            {
                pedido.CadeteAsignado = cadete; //modifico
                return $"Pedido #{nroPedido} asignado a cadete {cadete.Nombre} (ID: {cadete.Id})";
            }else
            {
            if (pedido == null)
                return $"No se encontró el pedido #{nroPedido}";
            if (cadete == null)
                return $"No se encontró el cadete con ID: {idCadete}";
            }
            return "Error inesperado al asignar el pedido.";
        }

        public int JornalACobrar(int idCadete)
        {
            var pedidosEntregados = listadoPedidos.Where(p => p.CadeteAsignado?.Id == idCadete && p.EstaEntregado()).Count();
            return pedidosEntregados * 500;
        }

        public string ReasignarPedido(int nroPedido, int nuevoIdCadete)
        {
            var pedido = listadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);
            var nuevoCadete = listadoCadetes.FirstOrDefault(c => c.Id == nuevoIdCadete);

            if (pedido != null && nuevoCadete != null)
            {
                pedido.CadeteAsignado = nuevoCadete;
                return $"Pedido #{nroPedido} fue reasignado al cadete {nuevoCadete.Nombre}";
            }
            else
            {
                return"No se pudo reasignar el pedido. Verifica el número de pedido y el ID del nuevo cadete.";
            }
        }
    
        public string CambiarEstadoPedido(int nroPedido, EstadoPedido nuevoEstado)
        {
            var pedido = listadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);

            if (pedido != null)
            {
                pedido.CambiarEstado(nuevoEstado);
                return $"Estado del pedido #{nroPedido} cambiado a {nuevoEstado}";
            }else
            {
                return $"No se encontró el pedido #{nroPedido}";
            }
        }

        public void AgregarListaCadetes(List<Cadete> cadetes)
        {
            listadoCadetes = cadetes;
        }

        public void AgregarListaPedidos(List<Pedido> pedidos)
        {
            listadoPedidos = pedidos;
        }


        public List<Pedido> GetPedidos()
        {
            return listadoPedidos;
        }

        public List<Cadete> GetCadetes()
        {
            return listadoCadetes;
        }

        public Informe GenerarInforme()
        {
            var informe = new Informe();
            int totalPedidos = 0;

            foreach (var cadete in listadoCadetes)
            {
                int pedidosEntregados = listadoPedidos.Count(p => p.CadeteAsignado?.Id == cadete.Id && p.EstaEntregado());

                int jornal = pedidosEntregados * 500;

                JornalCadete jc = new JornalCadete();
                jc.NombreCadete = cadete.Nombre;
                jc.Jornal = jornal;
                informe.Jornales.Add(jc);

                totalPedidos += pedidosEntregados;
            }

            int totalCadetes = listadoCadetes.Count;

            if (totalCadetes > 0)
            {
                informe.PromedioEntregasPorCadete = (double)totalPedidos / totalCadetes;
            }
            else
            {
                informe.PromedioEntregasPorCadete = 0;
            }

            informe.TotalPedidosEntregados = totalPedidos;
            return informe;
        }
    }
}
