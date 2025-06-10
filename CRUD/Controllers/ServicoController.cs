using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUD.Models;
using CRUD.Repositorio;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.ORM;

namespace CRUD.Controllers
{
    public class ServicoController : Controller
    {
        private readonly ServicoRepositorio _servicoRepositorio;

        public ServicoController(ServicoRepositorio servicoRepositorio)
        {
            _servicoRepositorio = servicoRepositorio;

        }
        public IActionResult Index()
        {
            var Servico = _servicoRepositorio.ListarServico();
            return View(Servico);
        }
        public IActionResult InserirServico(string TipoServico, decimal Valor)
        {
            try
            {
                // Chama o método do repositório que realiza a inserção no banco de dados
                var resultado = _servicoRepositorio.InserirServico(TipoServico, Valor);

                // Verifica o resultado da inserção
                if (resultado)
                {
                    // Se o resultado for verdadeiro, significa que o servico foi inserido com sucesso
                    return Json(new { success = true, message = "Servico inserido com sucesso!" });
                }
                else
                {
                    // Se o resultado for falso, significa que houve um erro ao tentar inserir o servico
                    return Json(new { success = false, message = "Erro ao inserir o servico. Tente novamente." });
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro inesperado, captura e exibe o erro
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }
        public IActionResult AtualizarServico(int id,string TipoServico, decimal Valor )
        {
            try
            {
                // Chama o repositório para atualizar o servico
                var resultado = _servicoRepositorio.AtualizarServico(id, TipoServico, Valor);

                if (resultado)
                {
                    return Json(new { success = true, message = "Servico atualizado com sucesso!" });
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
        public IActionResult ExcluirServico(int id)
        {
            try
            {
                // Chama o repositório para excluir o Servico
                var resultado = _servicoRepositorio.ExcluirServico(id);

                if (resultado)
                {
                    return Json(new { success = true, message = "Servico excluído com sucesso!" });
                }
                else
                {
                    // Se o resultado for falso, você pode fornecer uma mensagem mais específica.
                    return Json(new { success = false, message = "Não foi possível excluir o servico. Verifique se ele está vinculado a outros registros no sistema." });
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


