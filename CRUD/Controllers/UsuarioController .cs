using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUD.Models;
using CRUD.Repositorio;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;

        }
        public IActionResult Index()
        {
            var Usuarios = _usuarioRepositorio.ListarUsuarios();
            return View(Usuarios);
        }
        public IActionResult InserirUsuario(string Nome, string Senha, string Email, string Telefone, int TipoUsuario)
        {
            try
            {
                // Chama o método do repositório que realiza a inserção no banco de dados
                var resultado = _usuarioRepositorio.InserirUsuario(Nome, Email, Telefone, Senha, TipoUsuario);

                // Verifica o resultado da inserção
                if (resultado)
                {
                    // Se o resultado for verdadeiro, significa que o usuário foi inserido com sucesso
                    return Json(new { success = true, message = "Cliente inserido com sucesso!" });
                }
                else
                {
                    // Se o resultado for falso, significa que houve um erro ao tentar inserir o usuário
                    return Json(new { success = false, message = "Erro ao inserir o cliente. Tente novamente." });
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro inesperado, captura e exibe o erro
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }
        public IActionResult AtualizarUsuario(int id, string Nome, string Senha, string Email, string Telefone, int TipoUsuario)
        {
            try
            {
                // Chama o repositório para atualizar o usuário
                var resultado = _usuarioRepositorio.AtualizarUsuario(id, Nome, Email, Telefone, Senha, TipoUsuario);

                if (resultado)
                {
                    return Json(new { success = true, message = "Usuário atualizado com sucesso!" });
                }
                else
                {
                    return Json(new { success = false, message = "Erro ao atualizar o usuário. Verifique se o usuário existe." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }
        public IActionResult ExcluirUsuario(int id)
        {
            try
            {
                // Chama o repositório para excluir o usuário
                var resultado = _usuarioRepositorio.ExcluirUsuario(id);

                if (resultado)
                {
                    return Json(new { success = true, message = "Usuário excluído com sucesso!" });
                }
                else
                {
                    // Se o resultado for falso, você pode fornecer uma mensagem mais específica.
                    return Json(new { success = false, message = "Não foi possível excluir o usuário. Verifique se ele está vinculado a outros registros no sistema." });
                }
            }
            catch (Exception ex)
            {
                // Captura qualquer erro e inclui a mensagem detalhada da exceção
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }

    }
}


