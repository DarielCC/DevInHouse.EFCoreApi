using System.ComponentModel.DataAnnotations;

namespace DevInHouse.EFCoreApi.Application.ViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Publicação")]
        public DateTime Publicacao { get; set; }

        [Display(Name = "Autor")]
        public string Autor { get; set; }

        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Display(Name = "Preço")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Preco { get; set; }
    }
}
