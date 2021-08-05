using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;
using WebApi.Services;

namespace Tests
{
    [TestClass]
    public class GeneralTests
    {
        private readonly PasswordService _passwordService = new PasswordService();
        private readonly UserService _userService = new UserService();

        [TestMethod]
        public void Deve_retornar_falso_para_senha_sem_minimo_caracteres()
        {
            Assert.AreEqual(false, _passwordService.ValidatePassword("123456789a@bc"));
        }

        [TestMethod]
        public void Deve_retornar_falso_para_senha_sem_letra_maiuscula()
        {
            Assert.AreEqual(false, _passwordService.ValidatePassword("123456789a@bcder"));
        }

        [TestMethod]
        public void Deve_retornar_falso_para_senha_com_caracteres_repetidos_em_sequencia()
        {
            var a = _passwordService.ValidatePassword("123456789a@@bCder");
            Assert.AreEqual(false, a);
        }

        [TestMethod]
        public void Deve_retornar_verdadeiro_para_senha_valida()
        {
            Assert.AreEqual(true, _passwordService.ValidatePassword("123456789aR@bcder"));
        }

        [TestMethod]
        public void Deve_retornar_verdadeiro_para_senha_valida_gerada()
        {
            var password = _passwordService.CreatePassword();
            Assert.AreEqual(true, _passwordService.ValidatePassword(password));
        }

        [TestMethod]
        public void Deve_retornar_null_para_autenticacao_usuario_invalido()
        {
            var response = _userService.Authenticate(new AuthenticateRequest { Login = "Roberth", Password = "123456" });
            Assert.AreEqual(null, response);
        }

        [TestMethod]
        public void Deve_retornar_usuario_autenticado()
        {
            var response = _userService.Authenticate(new AuthenticateRequest { Login = "Admin", Password = "!#123@456#!" });
            Assert.AreNotEqual(null, response);
        }


    }
}
