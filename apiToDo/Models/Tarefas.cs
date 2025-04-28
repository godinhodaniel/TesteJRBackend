using apiToDo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apiToDo.Models
{
    public class Tarefas
    {
        // Lista de tarefas persistente para toda a vida útil da aplicação
        private static List<TarefaDTO> _lstTarefas = new List<TarefaDTO>
        {
            new TarefaDTO { ID_TAREFA = 1, DS_TAREFA = "Fazer Compras" },
            new TarefaDTO { ID_TAREFA = 2, DS_TAREFA = "Fazer Atividades Faculdade" },
            new TarefaDTO { ID_TAREFA = 3, DS_TAREFA = "Subir Projeto de Teste no GitHub" }
        };

        // Retorna a lista de tarefas
        public List<TarefaDTO> lstTarefas()
        {
            return _lstTarefas; // Retorna a lista persistente
        }

        // Método para adicionar uma tarefa
        public void InserirTarefa(TarefaDTO request)
        {
            try
            {
                // Adiciona a nova tarefa à lista persistente
                _lstTarefas.Add(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir tarefa: {ex.Message}");
            }
        }

        // Método para deletar uma tarefa com base no ID
        public void DeletarTarefa(int ID_TAREFA)
        {
            try
            {
                // Encontra a tarefa pela ID
                var tarefa = _lstTarefas.FirstOrDefault(x => x.ID_TAREFA == ID_TAREFA);

                if (tarefa != null)
                {
                    // Remove a tarefa da lista
                    _lstTarefas.Remove(tarefa);
                }
                else
                {
                    throw new Exception("Tarefa não encontrada.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar tarefa: {ex.Message}");
            }
        }
    }
}
