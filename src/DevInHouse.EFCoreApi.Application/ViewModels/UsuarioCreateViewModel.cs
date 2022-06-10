using System.ComponentModel.DataAnnotations;

namespace DevInHouse.EFCoreApi.Application.ViewModels
{
    public class UsuarioCreateViewModel
    {
        [Required(ErrorMessage = "Email é requerido")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é requerido")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmacaoSenha { get; set; }

        [Display(Name = "É Admin")]
        public bool IsAdmin { get; set; }
    }
}
