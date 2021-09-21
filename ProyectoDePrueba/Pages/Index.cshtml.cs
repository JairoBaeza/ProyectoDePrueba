using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProyectoDePrueba.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDePrueba.Pages
{
    public class IndexModel : PageModel
    {
        [Display(Name="Usuario")]
        [BindProperty]
        [Required(ErrorMessage ="El campo usuario es requerido")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Contraseña")]
        [BindProperty]
        [Required(ErrorMessage = "El campo contraseña es requerido")]
        public string Password { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var usuario = this.NombreUsuario;
                var password = this.Password;
                //Validar en la BD
                var repo = new LoginRepositorio();
                bool resultado = repo.UsuarioExiste(usuario, password);
                if (resultado == true)
                {
                    return RedirectToPage("./Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"El usuario o contraseña no es valido");
                    return Page();
                }
                
            }
            return Page();
        }
    }
}
