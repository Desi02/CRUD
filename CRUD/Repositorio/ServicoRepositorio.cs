using CRUD.Models;
using CRUD.ORM;

namespace CRUD.Repositorio
{
    public class ServicoRepositorio
    {
        private BdElysiumContext _context;
        public ServicoRepositorio(BdElysiumContext context)
        {
            _context = context;

        }
        public List<ServicoVM> ListarServico()
        {
            List<ServicoVM> listFun = new List<ServicoVM>();

            var listTb = _context.TbServicos.ToList();

            foreach (var item in listTb)
            {
                var servicos = new ServicoVM
                {
                    Id = item.Id,
                    TipoServico = item.TipoServico,
                    Valor = item.Valor
                };

                listFun.Add(servicos);
            }

            return listFun;
        }

        public bool InserirServico(string TipoServico, decimal Valor)
        {
            try
            {
                TbServico Servico = new TbServico();
                Servico.TipoServico = TipoServico;
                Servico.Valor = Valor;
                

                _context.TbServicos.Add(Servico);  // Supondo que _context.TbServico seja o DbSet para a entidade de servicos
                _context.SaveChanges();

                return true;  // Retorna true para indicar sucesso
            }
            catch (Exception ex)
            {
                // Trate o erro ou faça um log do ex.Message se necessário
                return false;  // Retorna false para indicar falha
            }
        }

        public bool AtualizarServico(int id, string tipoServico, decimal Valor)
        {
            try
            {
                // Busca o servico pelo ID
                var servico = _context.TbServicos.FirstOrDefault(u => u.Id == id);
                if (servico != null)
                {
                    // Atualiza os dados do servico
                    servico.TipoServico = tipoServico;
                    servico.Valor = Valor;
                   
                    // Salva as mudanças no banco de dados
                    _context.SaveChanges();

                    return true;  // Retorna verdadeiro se a atualização for bem-sucedida
                }
                else
                {
                    return false;  // Retorna falso se o servico não foi encontrado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o servico com ID {id}: {ex.Message}");
                return false;
            }
        }

        public bool ExcluirServico(int id)
        {
            try
            {
                // Busca o servico pelo ID
                var Servico = _context.TbServicos.FirstOrDefault(u => u.Id == id);

                // Se o servico não for encontrado, lança uma exceção personalizada
                if (Servico == null)
                {
                    throw new KeyNotFoundException("Serviço não encontrado.");
                }


                // Remove o servico do banco de dados
                _context.TbServicos.Remove(Servico);
                _context.SaveChanges();  // Isso pode lançar uma exceção se houver dependências

                // Se tudo correr bem, retorna true indicando sucesso
                return true;

            }
            catch (Exception ex)
            {
                // Aqui tratamos qualquer erro inesperado e logamos para depuração
                Console.WriteLine($"Erro ao excluir o serviço com ID {id}: {ex.Message}");

                // Relança a exceção para ser capturada pelo controlador
                throw new Exception($"Erro ao excluir o serviço: {ex.Message}");
            }
        }

        internal bool InserirServico(int tipoServico, decimal valor)
        {
            throw new NotImplementedException();
        }
    }
}
    

