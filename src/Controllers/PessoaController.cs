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
    public List<Pessoa> Get() {
        // Pessoa pessoa = new Pessoa("Caio", 26, "13322244433");
        // Contrato contrato = new Contrato("caio", 30.32);

        // pessoa.contratos.Add(contrato);
        
        return this._context.Pessoas.Include(p => p.contratos).ToList();
    }

    [HttpPost]
    public Pessoa Post([FromBody]Pessoa pessoa) {
        this._context.Pessoas.Add(pessoa);
        this._context.SaveChanges();

        return pessoa;
    }

    [HttpPut("{id}")]
    public string Put([FromRoute]int id, [FromBody]Pessoa pessoa) {
        this._context.Pessoas.Update(pessoa);
        this._context.SaveChanges();

        return "Dados do id" + id + "atualizados.";    
    }

    [HttpDelete("{id}")]
    public string Delete([FromRoute]int id) {
        var result = this._context.Pessoas.SingleOrDefault(e => e.Id == id);

        this._context.Pessoas.Remove(result);
        this._context.SaveChanges();

        return "deletado pessoa de Id " + id;
    }
}