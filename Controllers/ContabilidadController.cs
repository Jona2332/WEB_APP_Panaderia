using Microsoft.AspNetCore.Mvc;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Controllers
{
    public class ContabilidadController : Controller
    {
        private readonly IContabilidadModel _contabilidadModel;

        public ContabilidadController(IContabilidadModel contabilidadModel)
        {
            _contabilidadModel = contabilidadModel;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CuentasContables()
        {
            var cuentas = _contabilidadModel.ConsultarCuentasContables();
            return View(cuentas);
        }

        [HttpGet]
        public IActionResult CrearCuentaContable()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCuentaContable(CuentaContableEntity cuenta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contabilidadModel.InsertarCuentaContable(cuenta);
                    return RedirectToAction("CuentasContables");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear la cuenta: " + ex.Message);
                }
            }
            return View(cuenta);
        }

        [HttpGet]
        public IActionResult EditarCuentaContable(int id)
        {
            var cuenta = _contabilidadModel.ConsultarCuentasContables().FirstOrDefault(c => c.IdCuenta == id);
            if (cuenta == null)
            {
                return NotFound();
            }
            return View(cuenta);
        }

        [HttpPost]
        public IActionResult EditarCuentaContable(CuentaContableEntity cuenta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contabilidadModel.ActualizarCuentaContable(cuenta);
                    return RedirectToAction("CuentasContables");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar la cuenta: " + ex.Message);
                }
            }
            return View(cuenta);
        }

        [HttpPost]
        public IActionResult EliminarCuentaContable(int id)
        {
            try
            {
                _contabilidadModel.EliminarCuentaContable(id);
                return RedirectToAction("CuentasContables");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult EstadoResultados()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerarEstadoResultados(DateTime fechaInicio, DateTime fechaFin)
        {
            var estadoResultados = _contabilidadModel.GenerarEstadoResultados(fechaInicio, fechaFin);
            return View("EstadoResultados", estadoResultados);
        }

        public IActionResult BalanceGeneral()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerarBalanceGeneral(DateTime fechaCorte)
        {
            var balanceGeneral = _contabilidadModel.GenerarBalanceGeneral(fechaCorte);

            var viewModel = new BalanceGeneralViewModel
            {
                //FechaCorte = fechaCorte,
                FechaCorte = fechaCorte,
                Activos = balanceGeneral.Where(b => b.Categoria == "Activos").ToList(),
                Pasivos = balanceGeneral.Where(b => b.Categoria == "Pasivos").ToList(),
                Capital = balanceGeneral.Where(b => b.Categoria == "Capital").ToList()
            };
            if (viewModel == null)
            {
                 
            }

            return View("BalanceGeneral", viewModel);
        }
    }
}