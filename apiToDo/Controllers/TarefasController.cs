using apiToDo.DTO;
using apiToDo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace apiToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly Tarefas _tarefas;

        public TarefasController()
        {
            _tarefas = new Tarefas();
        }

        [HttpGet("lstTarefas")]
        public ActionResult ListarTarefas()
        {
            try
            {
                var lista = _tarefas.lstTarefas();
                return Ok(lista); // Status 200 com a lista
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Erro ao listar tarefas: {ex.Message}" });
            }
        }

        // Bônus - Buscar tarefa por ID
        [HttpGet("lstTarefasPorID")]
        public ActionResult BuscarTarefaPorId([FromQuery] int ID_TAREFA)
        {
            try
            {
                var tarefa = _tarefas.BuscarTarefaPorId(ID_TAREFA);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return NotFound(new { msg = $"Erro ao buscar tarefa: {ex.Message}" });
            }
        }

        [HttpPost("InserirTarefas")]
        public ActionResult InserirTarefas([FromBody] TarefaDTO request)
        {
            try
            {
                _tarefas.InserirTarefa(request);
                return Ok(_tarefas.lstTarefas()); // Status 200 com lista atualizada
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Erro ao inserir tarefa: {ex.Message}" });
            }
        }

        [HttpDelete("DeleteTask")]
        public ActionResult DeletarTarefa([FromQuery] int ID_TAREFA)
        {
            try
            {
                // Busca a tarefa para verificar se ela existe
                var tarefaExistente = _tarefas.lstTarefas().FirstOrDefault(t => t.ID_TAREFA == ID_TAREFA);

                // Se a tarefa não for encontrada
                if (tarefaExistente == null)
                {
                    // Retorna erro 404 - tarefa não encontrada
                    return NotFound(new { msg = $"Tarefa com o ID {ID_TAREFA} não encontrada." });
                }

                // Remove a tarefa da lista
                _tarefas.DeletarTarefa(ID_TAREFA);

                // Retorna a lista atualizada
                return Ok(_tarefas.lstTarefas());
            }
            catch (Exception ex)
            {
                // Retorna erro 400 com mensagem de erro
                return BadRequest(new { msg = $"Erro ao deletar tarefa: {ex.Message}" });
            }
        }

        // Bônus - Atualizar tarefa
        [HttpPut("AtualizarTarefa")]
        public ActionResult AtualizarTarefa([FromBody] TarefaDTO request)
        {
            try
            {
                _tarefas.AtualizarTarefa(request);
                return Ok(_tarefas.lstTarefas());
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = $"Erro ao atualizar tarefa: {ex.Message}" });
            }
        }

    }
}
