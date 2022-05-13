using System.ComponentModel.DataAnnotations;

namespace DevInHouse.EFCoreApi.Api.DTOs.Request
{
    public class LivroRequest
    {
        [Required(ErrorMessage = "Titulo é requerido")]
        [StringLength(255, ErrorMessage = "Titulo não pode exceder de 255 carateres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Categoria é requerido")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Autor é requerido")]
        public int AutorId { get; set; }

        [Required(ErrorMessage = "Data da publicação é requerido")]
        [DataType(DataType.DateTime)]
        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage = "Preço é requerido")]
        [Range(1, 999, ErrorMessage = "O preçõ deve estar entre 1 e 999")]
        public decimal Preco { get; set; }
    }
}