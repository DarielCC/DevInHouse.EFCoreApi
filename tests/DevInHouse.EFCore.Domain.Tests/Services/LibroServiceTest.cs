using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Core.Services;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using DevInHouse.EFCoreApi.Domain.Notifications;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevInHouse.EFCore.Domain.Tests.Services
{
    public class LibroServiceTest
    {
        private Mock<ILivroRepository> _mockLivroRepository;
        private Mock<IAutorRepository> _mockAutorRepository;
        private Mock<INotificacaoService> _mockNotificacaoService;

        private LivroService _livroService;

        public LibroServiceTest()
        {
            _mockLivroRepository = new Mock<ILivroRepository>();
            _mockAutorRepository = new Mock<IAutorRepository>();
            _mockNotificacaoService = new Mock<INotificacaoService>();

            _livroService = new LivroService(_mockLivroRepository.Object, _mockAutorRepository.Object, _mockNotificacaoService.Object);
        }

        [Fact]
        public async void ObterLivrosAsync_RetornarListaDeLivro_IEnumerableDeLivros()
        {
            //Arrage
            var titulo = "Teste";
            var livro = new Livro("Teste", 1, 1, DateTime.Now, 10);
            var livrosMock =  new List<Livro>() { livro };
            _mockLivroRepository.Setup(m => m.ObterLivrosAsync(titulo)).ReturnsAsync(livrosMock);

            //Act
            var livros = await _livroService.ObterLivrosAsync(titulo);

            //Assert
            Assert.Contains(livro, livros);
            Assert.Equal(titulo, livrosMock.FirstOrDefault().Titulo);
            Assert.Same(livro, livrosMock.FirstOrDefault());
            _mockLivroRepository.Verify(m => m.ObterLivrosAsync(titulo), Times.Once);
        }

        [Fact]
        public async Task CriarLivroAsync_CriaLivro_RetornaDefaultIdDoLivro_QuandoAutorNull()
        {
            //Arrage
            _mockAutorRepository.Setup(m => m.ObterPorIdAsync(1)).Returns(Task.FromResult<Autor?>(null));
            _mockNotificacaoService.Setup(m => m.InserirNotificacao(It.IsAny<Notificacao>()));

            //Act
            var id = await _livroService.CriarLivroAsync(It.IsAny<string>(), It.IsAny<int>(), 1, It.IsAny<DateTime>(), It.IsAny<decimal>());

            //Assert
            Assert.Equal(default, id);
            _mockAutorRepository.Verify(m => m.ObterPorIdAsync(1), Times.Once);
            _mockNotificacaoService.Verify(m => m.InserirNotificacao(It.IsAny<Notificacao>()), Times.Once);
            _mockLivroRepository.Verify(m => m.ObterPorTituloAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task CriarLivroAsync_CriaLivro_RetornaDefaultIdDoLivro_QuandoLivroExiste()
        {
            //Arrage
            _mockAutorRepository.Setup(m => m.ObterPorIdAsync(1)).ReturnsAsync(new Autor(It.IsAny<string>(), It.IsAny<string>()));
            _mockNotificacaoService.Setup(m => m.InserirNotificacao(It.IsAny<Notificacao>()));
            _mockLivroRepository.Setup(m => m.ObterPorTituloAsync(It.IsAny<string>())).ReturnsAsync(new Livro(It.IsAny<string>(), It.IsAny<int>(), 1, It.IsAny<DateTime>(), It.IsAny<decimal>()));

            //Act
            var id = await _livroService.CriarLivroAsync(It.IsAny<string>(), It.IsAny<int>(), 1, It.IsAny<DateTime>(), It.IsAny<decimal>());

            //Assert
            Assert.Equal(default, id);
            _mockAutorRepository.Verify(m => m.ObterPorIdAsync(1), Times.Once);
            _mockNotificacaoService.Verify(m => m.InserirNotificacao(It.IsAny<Notificacao>()), Times.Once);
            _mockLivroRepository.Verify(m => m.ObterPorTituloAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CriarLivroAsync_CriaLivro_RetornaDefaultIdDoLivro_QuandoLivroInvalido()
        {
            //Arrage
            _mockAutorRepository.Setup(m => m.ObterPorIdAsync(1)).ReturnsAsync(new Autor(It.IsAny<string>(), It.IsAny<string>()));
            _mockNotificacaoService.Setup(m => m.InserirNotificacoes(It.IsAny<ValidationResult>()));
            _mockLivroRepository.Setup(m => m.ObterPorTituloAsync(It.IsAny<string>()));
                        
            //Act
            var id = await _livroService.CriarLivroAsync(string.Empty, It.IsAny<int>(), 1, It.IsAny<DateTime>(), It.IsAny<decimal>());

            //Assert
            Assert.Equal(default, id);
            _mockAutorRepository.Verify(m => m.ObterPorIdAsync(1), Times.Once);
            _mockNotificacaoService.Verify(m => m.InserirNotificacoes(It.IsAny<ValidationResult>()), Times.Once);
            _mockLivroRepository.Verify(m => m.ObterPorTituloAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CriarLivroAsync_CriaLivro_RetornaIdDoLivro()
        {
            //Arrage
            var idMock = 1;

            _mockAutorRepository.Setup(m => m.ObterPorIdAsync(1)).ReturnsAsync(new Autor(It.IsAny<string>(), It.IsAny<string>()));
            _mockLivroRepository.Setup(m => m.ObterPorTituloAsync(It.IsAny<string>()));
            _mockLivroRepository.Setup(x => x.InserirLivroAsync(It.IsAny<Livro>())).ReturnsAsync(idMock);

            //Act
            var id = await _livroService.CriarLivroAsync("Titulo", 1, 1, DateTime.Now, 100);

            //Assert
            Assert.Equal(idMock, id);
            _mockAutorRepository.Verify(m => m.ObterPorIdAsync(1), Times.Once);
            _mockLivroRepository.Verify(m => m.ObterPorTituloAsync(It.IsAny<string>()), Times.Once);
            _mockLivroRepository.Verify(m => m.InserirLivroAsync(It.IsAny<Livro>()), Times.Once);
        }
    }
}
