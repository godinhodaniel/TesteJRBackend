using apiToDo.DTO;
using apiToDo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace apiToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        [HttpGet("lstTarefas")]
        public ActionResult lstTarefas()
        {
            try
            {
                var tarefas = new Tarefas().lstTarefas();
                return StatusCode(200, tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpPost("InserirTarefas")]
        public ActionResult InserirTarefas([FromBody] TarefaDTO Request)
        {
            try
            {
                var tarefas = new Tarefas();
                tarefas.InserirTarefa(Request);

                return StatusCode(200, tarefas.lstTarefas()); // Retorna a lista de tarefas com status 200
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpDelete("DeletarTarefa")]
        public ActionResult DeleteTask([FromQuery] int ID_TAREFA)
        {
            try //catch para erros do bloco de codigos abaixo
            {
                var tarefas = new Tarefas(); //nova instancia da classe para acessar metodos

                // Verificando se a tarefa com o ID_TAREFA existe
                var tarefaExistente = tarefas.lstTarefas().FirstOrDefault(t => t.ID_TAREFA == ID_TAREFA);

                if (tarefaExistente == null) //se não há tarefas com ID informado
                {
                    // Se a tarefa não for encontrada, retorna um erro 404 (Not Found)
                    return StatusCode(404, new { msg = $"A tarefa com o ID {ID_TAREFA} não foi encontrada." });
                }

                // método para deletar a tarefa com o ID fornecido
                tarefas.DeletarTarefa(ID_TAREFA);

                // se houver sucesso retorna 200 (ok), com a lista atualizada de tarefas
                return StatusCode(200, tarefas.lstTarefas());
            }
            catch (Exception ex) // caso aconteça alguma Exception durante o processo
            {
                // mensagem de erro se houver Exception
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }
    }
}