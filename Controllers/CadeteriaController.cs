using Microsoft.AspNetCore.Mvc;
using CadeteriaAPI.Models;

namespace CadeteriaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadeteriaController : ControllerBase
    {
        private Cadeteria cadeteria; //cadeteria ya no es estetica
        private AccesoADatosCadeteria ADCadeteria;
        private AccesoADatosCadetes ADCadetes;
        private AccesoADatosPedidos ADPedidos;

        public CadeteriaController()
        {
            ADCadeteria= new AccesoADatosCadeteria();
            ADCadetes= new AccesoADatosCadetes();
            ADPedidos= new AccesoADatosPedidos();
            cadeteria = ADCadeteria.Obtener();
            cadeteria.AgregarListaCadetes(ADCadetes.Obtener());
            cadeteria.AgregarListaPedidos(ADPedidos.Obtener());
        }

        [HttpGet("pedidos")]
        public ActionResult<List<Pedido>> GetPedidos()
        {
            return Ok(ADPedidos.Obtener());
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
        public ActionResult<string> AgregarPedido([FromBody] Pedido pedido)
        {
            cadeteria.AltaPedido(pedido);
            ADPedidos.Guardar(cadeteria.GetPedidos());
            return Created("Pedido creado con EXITO!", pedido);
        }

        [HttpPut("asignar")]
        public ActionResult<Pedido> AsignarPedido(int idPedido, int idCadete)
        {
            var resultado = cadeteria.AsignarPedido(idPedido, idCadete);
            if(resultado == null)
            {
                return NotFound();
            }
            ADPedidos.Guardar(cadeteria.GetPedidos());
            return Ok(resultado);
        }

        [HttpPut("cambiarestado")]
        public ActionResult<Pedido> CambiarEstadoPedido(int idPedido, int nuevoEstado)
        {
            var estado = (EstadoPedido)nuevoEstado;
            var resultado = cadeteria.CambiarEstadoPedido(idPedido, estado);
            if (resultado == null)
            {
                return NotFound("Pedido no encontrado");
            }
            ADPedidos.Guardar(cadeteria.GetPedidos());
            return Ok(resultado);
        }

        [HttpPut("reasignar")]
        public ActionResult<string> CambiarCadetePedido(int idPedido, int idNuevoCadete)
        {
            var resultado = cadeteria.ReasignarPedido(idPedido, idNuevoCadete);
            if (resultado == null)
            {
                return NotFound("Pedido no encontrado");
            }
            ADPedidos.Guardar(cadeteria.GetPedidos());
            return Ok(resultado);
        }
    }
}
