using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using src.Models;
using src.Persistence;

namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase{
    
    private DatabaseContext _context { get; set; }

    public PessoaController(DatabaseContext context) //faz a injeção de dependencia
    {
        this._context = context;
    }

    [HttpGet]
    public ActionResult<List<Pessoa>> Get() {

        var result = this._context.Pessoas.Include(p => p.contratos).ToList();
        
        if (!result.Any()) {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Pessoa> Post([FromBody]Pessoa pessoa) {
        try {
            this._context.Pessoas.Add(pessoa);
            this._context.SaveChanges();
        }
        catch (System.Exception) {
            return BadRequest(new {
                msg = "Falha ao salvar os dados",
                status = HttpStatusCode.BadRequest
            });
        }

        return Created("Criado", pessoa);
    }

    [HttpPut("{id}")]
    public ActionResult<Object> Put([FromRoute]int id, [FromBody]Pessoa pessoa) {

        var result = this._context.Pessoas.SingleOrDefault(e => e.Id == id);

        if (result is null) {
            return NotFound(new {
                msg = "Registro não encontrado",
                status = HttpStatusCode.NotFound
            });
        }
        try {
            this._context.Pessoas.Update(pessoa);
            this._context.SaveChanges();
        }
        catch (System.Exception) {
            return BadRequest(new {
                msg = "Houve erro ao enviar solicitação de atualização do id " + id,
                status = HttpStatusCode.BadRequest
            });
        }
        return Ok(new {
            msg = "Dados do id" + id + "atualizados.",
            status = HttpStatusCode.OK
        });    
    }

    [HttpDelete("{id}")]
    public ActionResult<Object> Delete([FromRoute]int id) {
        var result = this._context.Pessoas.SingleOrDefault(e => e.Id == id);

        if (result is null) {
            return BadRequest(new { //formato json
                msg = "Conteúdo inexistente, solicitação invalida.",
                status = HttpStatusCode.BadRequest, //enum para os status code..
            });
        }
        this._context.Pessoas.Remove(result);
        this._context.SaveChanges();

        return Ok(new {
                msg = "deletado pessoa de Id " + id,
                status = HttpStatusCode.OK
        });
    }
}