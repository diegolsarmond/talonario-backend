using Moq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.ViewModels;
using Xunit;

namespace TalonarioTests.ApplicationTests
{
    public class UsuarioApplicationServiceTests
    {
        #region Public Methods

        [Fact]
        public async Task Logout_ComDadosCorretos_RetornaTrue()
        {
            //arrange
            Mock<IUsuarioRepository> usuarioRepository = new();
            usuarioRepository.Setup(u => u.Logout(It.IsAny<string>(), It.IsAny<string>()))
                             .ReturnsAsync(true);
            UsuarioLogout usuarioLogout = new("cpf", "idDispositivo");
            UsuarioApplicationService usuarioService = new(usuarioRepository.Object, null, null);

            //act
            var retorno = await usuarioService.Logout(usuarioLogout);

            //assert
            Assert.True(retorno);
        }

        [Fact]
        public async Task Logout_ComDadosIncorretos_RetornaFalse()
        {
            //arrange
            Mock<IUsuarioRepository> usuarioRepository = new();
            usuarioRepository.Setup(u => u.Logout(It.IsAny<string>(), It.IsAny<string>()))
                             .ReturnsAsync(false);
            UsuarioLogout usuarioLogout = new("cpf", "idDispositivo");
            UsuarioApplicationService usuarioService = new(usuarioRepository.Object, null, null);

            //act
            var retorno = await usuarioService.Logout(usuarioLogout);

            //assert
            Assert.False(retorno);
        }

        #endregion Public Methods
    }
}