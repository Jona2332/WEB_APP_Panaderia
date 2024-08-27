using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace WEB_APP_Panaderia.Controllers
{

    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly ILogsModel _generalesModel;
        private readonly IUsuariosModel _usuariosModel;
        private readonly IUsuariosRolesModel _usuariosRolesModel;


        public UsuariosController(ILogger<UsuariosController> logger, IUsuariosModel usuariosModel, IUsuariosRolesModel usuariosRolesModel, ILogsModel generalesModel)
        {
            _logger = logger;
            _usuariosModel = usuariosModel;
            _generalesModel = generalesModel;
            _usuariosRolesModel = usuariosRolesModel;

        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }


        [TypeFilter(typeof(JwtAuthorizationFilter))]
        public IActionResult Usuarios()
        {
            try
            {
                var viewModel = new ViewModel
                {
                    Usuarios = _usuariosModel.GetAllUsers(),
                    Usuario = new UsuariosEntities(),
                    Roles = _usuariosRolesModel.ConsultarUsuariosRoles()
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [AllowAnonymous]
        [HttpPost]

        public IActionResult ValidarCredenciales(UsuariosEntities entidad)
        {
            try
            {

                var resultado = _usuariosModel.ValidarCredenciales(entidad);

                if (resultado != null)
                {
                    string rolDescripcion = string.Empty;
                    string estado = string.Empty;


                    if (resultado.descripcion == "admin")
                    {
                        rolDescripcion = "Administrador";
                    }
                    HttpContext.Session.SetString("id_usuario", resultado.idUsuario.ToString());
                    HttpContext.Session.SetString("Correo", resultado.correo);
                    HttpContext.Session.SetString("Nombre", resultado.nombre);
                    HttpContext.Session.SetString("Rol", rolDescripcion);
                    HttpContext.Session.SetString("Estado", estado);
                    HttpContext.Session.SetString("Token", resultado.Token);
                    return RedirectToAction("Metricas", "Home");
                }

                else
                {
                    TempData["mensaje"] = "Usuario y contraseña incorrectos.\n Por favor verifique sus credenciales e intente de nuevo.";
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                //return View("Error");
                return RedirectToAction("ErrorUsuario", "Home");

            }


        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
			//HttpContext.Session.Remove("id_usuario");
			return RedirectToAction("Index", "Home");

        }
        [TypeFilter(typeof(JwtAuthorizationFilter))]
        [HttpPost]
        public IActionResult ActualizarUsuario(UsuariosEntities usuario)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogError($"Errores de validación del modelo: {string.Join(", ", errors)}");
                return BadRequest(errors);
            }

            try
            {
                _usuariosModel.ActualizarUsuario(usuario);
                return RedirectToAction("Usuarios", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el usuario: {ex.Message}");
                return RedirectToAction("Usuarios", "Home");
            }
        }


        [TypeFilter(typeof(JwtAuthorizationFilter))]
        [HttpPost]
        public IActionResult RegistrarUsuarios(ViewModel model)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogError($"Errores de validación del modelo: {string.Join(", ", errors)}");
                return View(model);
            }
            try
            {
                // Log de los datos recibidos
                _logger.LogInformation($"Datos recibidos: Nombre={model.Usuario.nombre}, Correo={model.Usuario.correo}");

                if (string.IsNullOrEmpty(model.Usuario.nombre) || string.IsNullOrEmpty(model.Usuario.correo) || string.IsNullOrEmpty(model.Usuario.contrasenna))
                {
                    throw new Exception("Todos los campos son obligatorios.");
                }

                _usuariosModel.RegistrarUsuarios(model.Usuario);
                TempData["Mensaje"] = "Usuario registrado exitosamente.";
                return RedirectToAction("Usuarios", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al registrar usuario: {ex.Message}");
                TempData["Mensaje"] = "Error al registrar el usuario: " + ex.Message;
                return RedirectToAction("Usuarios", "Home");
            }
        }

        [HttpPost]
        public IActionResult RecuperarContrasenna(UsuariosEntities entidad)
        {
            try
            {
                _usuariosModel.RecuperarContrasenna(entidad);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                RegistrarBitacora(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult BuscarExisteCorreo(string Correo)
        {
            var resultado = _usuariosModel.BuscarExisteCorreo(Correo);
            var jsonResult = JsonConvert.SerializeObject(resultado);
            return Content(jsonResult, "application/json");
        }

        [TypeFilter(typeof(JwtAuthorizationFilter))]
        [HttpGet]
        public IActionResult GetUsuarioById(int id)
        {
            try
            {
                var usuario = _usuariosModel.GetUsuarioById(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Json(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el usuario: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [TypeFilter(typeof(JwtAuthorizationFilter))]
        [HttpPost]
        public IActionResult DesactivarUsuario(int idUsuario)
        {
            try
            {
                _usuariosModel.DesactivarUsuario(idUsuario);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al desactivar el usuario: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }


        [TypeFilter(typeof(JwtAuthorizationFilter))]
        public void RegistrarBitacora(Exception ex, ControllerContext contexto)
        {
            LogsEntities error = new LogsEntities();
            error.Origen = ControllerContext.ActionDescriptor.ControllerName + " - " + ControllerContext.ActionDescriptor.ActionName;
            error.Detalle = ex.Message;

            _generalesModel.RegistrarBitacora(error);
        }




    }

}
