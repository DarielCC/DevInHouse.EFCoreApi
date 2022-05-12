namespace DevInHouse.EFCoreApi.Api.DTOs
{
    public class LivroDTO
    {
        public string Titulo { get; set; }
        public CategoriaDTO Categoria { get; set; }
        public AutorDTO Autor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public decimal Preco { get; set; }
    }
}
