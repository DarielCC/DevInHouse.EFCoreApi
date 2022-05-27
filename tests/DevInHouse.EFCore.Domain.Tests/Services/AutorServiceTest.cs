using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using DevInHouse.EFCoreApi.Domain.Services;
using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCore.Domain.Tests.Services
{
    public class AutorServiceTest
    {

        private Mock<IAutorRepository> _mockAutorRepository;
        private AutorService _autorService;

        public AutorServiceTest()
        {
            _mockAutorRepository = new Mock<IAutorRepository>();        
            _autorService = new AutorService(_mockAutorRepository.Object);
        }

        [Theory]
        [InlineData("", "Sobrenome")]
        [InlineData("Nome", "")]
        [InlineData("", "")]
        [InlineData(null, "Sobrenome")]
        [InlineData("Nome", null)]
        [InlineData(null, null)]
        public async void CriarAutorAsync_ValidaParametros_RetornaException(string nome, string sobrenome)
        {
            //Arrage
            _mockAutorRepository.Setup(m => m.ObterPorNomeAsync(nome)).ReturnsAsync(It.IsAny<Autor>());

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _autorService.CriarAutorAsync(nome, sobrenome));

            //Assert
            Assert.Equal("Nome e/ou Sobrenome não podem estar vazios", exception.Message);
            _mockAutorRepository.Verify(m => m.ObterPorNomeAsync(nome), Times.Never);
        }

        [Fact]
        public async void CriarAutorAsync_VerificaAutorPorNome_RetornaException()
        {
            //Arrage
            var nome = "Nome";
            var sobrenome = "Sobrenome";
            _mockAutorRepository.Setup(m => m.ObterPorNomeAsync(nome)).ReturnsAsync(new Autor(nome, sobrenome));
            _mockAutorRepository.Setup(m => m.InserirAutorAsync(It.IsAny<Autor>()));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _autorService.CriarAutorAsync(nome, sobrenome));

            //Assert
            Assert.Equal("Autor já existe", exception.Message);
            _mockAutorRepository.Verify(m => m.ObterPorNomeAsync(nome), Times.Once);
            _mockAutorRepository.Verify(m => m.InserirAutorAsync(It.IsAny<Autor>()), Times.Never);
        }


        [Fact]
        public async void CriarAutorAsync_CriaUmAutor_RetornaAutorId()
        {
            //Arrage
            var nome = "Nome";
            var sobrenome = "Sobrenome";
            var idMock = 1;
            _mockAutorRepository.Setup(m => m.ObterPorNomeAsync(nome)).ReturnsAsync(It.IsAny<Autor>());
            _mockAutorRepository.Setup(m => m.InserirAutorAsync(It.IsAny<Autor>())).ReturnsAsync(idMock);

            //Act
            var id = await _autorService.CriarAutorAsync(nome, sobrenome);

            //Assert
            Assert.Equal(idMock, id);
            _mockAutorRepository.Verify(m => m.ObterPorNomeAsync(nome), Times.Once);
            _mockAutorRepository.Verify(m => m.InserirAutorAsync(It.IsAny<Autor>()), Times.Once);
        }
    }
}
