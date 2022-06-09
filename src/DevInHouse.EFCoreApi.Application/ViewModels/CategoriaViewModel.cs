using System.ComponentModel.DataAnnotations;

namespace DevInHouse.EFCoreApi.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
