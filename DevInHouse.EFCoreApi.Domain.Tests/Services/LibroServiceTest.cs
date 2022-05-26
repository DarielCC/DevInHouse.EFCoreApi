using System;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace DevInHouse.EFCoreApi.Domain.Tests.Services
{
    public class LibroServiceTest
    {
        private Mock<ILivroRepository> _mockLivroRepository;
        private Mock<IAutorRepository> _mockAutorRepository;
        private Mock<INotificacaoService> _mockNotificacaoService;

        private LibroService _libroService;

        public LibroServiceTest()
        {
            _mockLivroRepository = new Mock<ILivroRepository>();
            _mockAutorRepository = new Mock<IAutorRepository>();
            _mockNotificacaoService = new Mock<INotificacaoService>();

            _libroService = new LibroService(_mockLivroRepository.Object, _mockAutorRepository.Object, _mockNotificacaoService.Object);
        }

        [Fact]
        public void ObterLivrosAsync_RetornarListaDeLivro_IEnumerableDeLivros()
        {
            //Arrage
            var titulo = "Teste";
            var livrosMock = new List<Livro>() { new Livro() { Titulo = titulo } };
            _mockLivroRepository.Setup(m => m.ObterLivrosAsync(titulo)).Return(listaDeLivros);

            //Act
            var libros = _libroService.ObterLivrosAsync(titulo);

            //Assert
            Assert.Contains(new Livro() { Titulo = titulo }, libros);
        }
    }
}
