using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInHouse.EFCoreApi.Application.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email é requerido")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é requerido")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
