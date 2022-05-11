namespace DevInHouse.EFCoreApi.Api.DTOs.Request
{
    public class LivroRequest
    {
        public string Titulo { get; set; }
        public int CategoriaId { get; set; }
        public int AutorId { get; set; }
        public DateTime DataPublicacao { get; set; }
        public decimal Preco { get; set; }
    }
}