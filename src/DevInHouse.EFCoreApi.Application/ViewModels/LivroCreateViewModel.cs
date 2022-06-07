using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DevInHouse.EFCoreApi.Application.ViewModels
{
    public class LivroCreateViewModel
    {
        [Display(Name = "Título")]
        [Required(ErrorMessage = "Titulo é requerido")]
        [StringLength(255, ErrorMessage = "Titulo não pode exceder de 255 carateres")]
        public string Titulo { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Categoria é requerido")]
        public int CategoriaId { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Autor é requerido")]
        public int AutorId { get; set; }


        [Display(Name = "Publicação")]
        [DataType(DataType.Date, ErrorMessageResourceName = "Data inválida")]
        [Required(ErrorMessage = "Publicação é requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Publicacao { get; set; }

        [Display(Name = "Preço")]
        [DataType(DataType.Currency, ErrorMessageResourceName = "Preço inválido")]
        [Range(1, 9999, ErrorMessage = "Preço inválido")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Preço é requerido")]
        public decimal Preco { get; set; }


        [Display(Name = "Autores")]
        public IEnumerable<AutorViewModel> Autores { get; set; }

        [Display(Name = "Categorias")]
        public IEnumerable<CategoriaViewModel> Categorias { get; set; }
    }
}
