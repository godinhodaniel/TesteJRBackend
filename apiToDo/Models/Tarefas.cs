using apiToDo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apiToDo.Models
{
    public class Tarefas
    {
        // Lista de tarefas persistente em memória com alguns dados iniciais
        private static List<TarefaDTO> _lstTarefas = new List<TarefaDTO>
        {
            new TarefaDTO { ID_TAREFA = 1, DS_TAREFA = "Fazer Compras" },
            new TarefaDTO { ID_TAREFA = 2, DS_TAREFA = "Fazer Atividades Faculdade" },
            new TarefaDTO { ID_TAREFA = 3, DS_TAREFA = "Subir Projeto de Teste no GitHub" }
        };

        private static int _proximoId = _lstTarefas.Max(t => t.ID_TAREFA) + 1;

        public List<TarefaDTO> lstTarefas()
        {
            return _lstTarefas;
        }

        // Insere uma nova tarefa na lista com ID gerado automaticamente
        public void InserirTarefa(TarefaDTO request)
        {
            try
            {
                // Atribui o próximo ID disponível à nova tarefa
                request.ID_TAREFA = _proximoId++;

                _lstTarefas.Add(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir tarefa: {ex.Message}");
            }
        }

        // Método responsável por deletar uma tarefa a partir do seu ID
        public void DeletarTarefa(int ID_TAREFA)
        {
            try
            {
                // Procura na lista a primeira tarefa cujo ID seja igual ao ID_TAREFA informado
                var tarefa = _lstTarefas.FirstOrDefault(x => x.ID_TAREFA == ID_TAREFA);

                // Verifica se a tarefa foi encontrada (FirstOrDefault retorna null se não encontrar)
                if (tarefa == null)
                {
                    // Se a tarefa não for encontrada, lança uma exceção com mensagem informando que não existe
                    throw new Exception($"Tarefa com ID {ID_TAREFA} não encontrada.");
                }

                // Se a tarefa foi encontrada, remove a tarefa da lista de tarefas
                _lstTarefas.Remove(tarefa);
            }
            catch (Exception ex)
            {
                // Caso ocorra qualquer erro no processo de deleção, lança uma nova exceção com a mensagem de erro original
                throw new Exception($"Erro ao deletar tarefa: {ex.Message}");
            }
        }

        // Atualiza a descrição de uma tarefa existente com base no ID
        public void AtualizarTarefa(TarefaDTO request)
        {
            try
            {
                // Busca a tarefa correspondente ao ID informado
                var tarefa = _lstTarefas.FirstOrDefault(x => x.ID_TAREFA == request.ID_TAREFA);

                // Se a tarefa não for encontrada, lança exceção
                if (tarefa == null)
                    throw new Exception($"Tarefa com ID {request.ID_TAREFA} não encontrada para atualização.");

                // Se encontrada, atualiza a descrição da tarefa
                tarefa.DS_TAREFA = request.DS_TAREFA;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao atualizar tarefa: {ex.Message}");
            }
        }

        // Busca uma tarefa específica pelo ID
        public TarefaDTO BuscarTarefaPorId(int ID_TAREFA)
        {
            try
            {
                var tarefa = _lstTarefas.FirstOrDefault(x => x.ID_TAREFA == ID_TAREFA);

                if (tarefa == null)
                    throw new Exception($"Tarefa com ID {ID_TAREFA} não encontrada.");

                return tarefa;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tarefa: {ex.Message}");
            }
        }
    }
}
