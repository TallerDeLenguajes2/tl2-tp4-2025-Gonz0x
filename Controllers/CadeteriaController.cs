using Microsoft.AspNetCore.Mvc;
using CadeteriaAPI.Models;

namespace CadeteriaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadeteriaController : ControllerBase
    {
        private static Cadeteria cadeteria = new Cadeteria("MALDITO LUNES", "3919329");

        static CadeteriaController()
        {
            cadeteria.Cadetes.Add(new Cadete(1, "Pedro Picapiedra", "Av. Alem 712", 134251));
            cadeteria.Cadetes.Add(new Cadete(2, "María PanzaFria", "AV JUJUY AL 4000", 432552));
        }
/* 
        var cadetes = new List<Cadete>                  Otra forma
        {
            new Cadete(1, "Carlos", "Calle 1", 123456),
            new Cadete(2, "Lucía", "Calle 2", 654321),
            new Cadete(3, "Matías", "Calle 3", 111222)
        };

        cadeteria.CargarCadetes(cadetes); */

        [HttpGet("pedidos")]
        public ActionResult<List<Pedido>> GetPedidos()
        {
            return Ok(cadeteria.GetPedidos());
        }
        
        [HttpGet("cadetes")]
        public ActionResult<List<Cadete>> GetCadetes()
        {
            return Ok(cadeteria.GetCadetes());
        }

        [HttpGet("informe")]
        public ActionResult<Informe> GetInforme()
        {
            var informe = cadeteria.GenerarInforme();
            return Ok(informe);
        }

        [HttpPost("pedido")]
        public ActionResult<string> AgregarPedido([FromBody] PedidoDTO pedidoDto)
        {
            var resultado = cadeteria.AgregarPedido(pedidoDto.Nro, pedidoDto.Observaciones, pedidoDto.NombreCliente, pedidoDto.DireccionCliente, pedidoDto.TelefonoCliente, pedidoDto.Referencia);
            return Ok(resultado);
        }

        [HttpPut("asignar")]
        public ActionResult<string> AsignarPedido(int idPedido, int idCadete)
        {
            var resultado = cadeteria.AsignarPedido(idPedido, idCadete);
            return Ok(resultado);
        }

        [HttpPut("cambiarestado")]
        public ActionResult<string> CambiarEstadoPedido(int idPedido, int nuevoEstado)
        {
            var estado = (EstadoPedido)nuevoEstado;
            var resultado = cadeteria.CambiarEstadoPedido(idPedido, estado);
            return Ok(resultado);
        }

        [HttpPut("reasignar")]
        public ActionResult<string> CambiarCadetePedido(int idPedido, int idNuevoCadete)
        {
            var resultado = cadeteria.ReasignarPedido(idPedido, idNuevoCadete);
            return Ok(resultado);
        }
    }
}
