using System;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB_APP_Panaderia.Models;
using WEB_APP_Panaderia.Services;

namespace WEB_APP_Panaderia.Controllers
{
    public class EmailController : Controller
    {
        private readonly IApiService _apiService;

        public EmailController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public ActionResult Index()
        {
            var emails = _apiService.GetEmails();
            return View(emails);
        }

        [HttpPost]
        public IActionResult DeleteEmail(int id)
        {
            try
            {
                _apiService.DeleteEmail(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SendEmail([FromBody] Email email)
        {
            Console.WriteLine($"Received email: {JsonConvert.SerializeObject(email)}");
            if (email == null || string.IsNullOrEmpty(email.Recipients) || string.IsNullOrEmpty(email.Subject) || string.IsNullOrEmpty(email.Body))
            {
                return BadRequest("Datos de correo inválidos");
            }
            _apiService.SendEmail(email);
            return Ok();
        }

        public ActionResult GetClientes()
        {
            var clientes = _apiService.GetClientes();
            return Json(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage()
        {
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file != null && file.Length > 0)
                {
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var guid = Guid.NewGuid().ToString();
                        var newFileName = guid + "_" + fileName;
                        var relativePath = Path.Combine("img", newFileName);
                        var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

                        // Asegúrate de que el directorio exista
                        var directory = Path.GetDirectoryName(absolutePath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        using (var stream = new FileStream(absolutePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var imageUrl = "/" + relativePath.Replace("\\", "/");
                        return Json(new { success = true, imageUrl = imageUrl });
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, error = ex.Message });
                    }
                }
            }
            return Json(new { success = false });
        }
    }
}
