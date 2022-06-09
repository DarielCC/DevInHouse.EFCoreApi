using System.ComponentModel.DataAnnotations;

namespace DevInHouse.EFCoreApi.Application.ViewModels
{
    public class AutorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome completo")]
        public string NomeCompleto { get; set; }
    }
}
