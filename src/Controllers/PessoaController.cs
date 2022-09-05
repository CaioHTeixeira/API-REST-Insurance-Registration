using Microsoft.AspNetCore.Mvc;
using src.Models;

namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase{
    
    [HttpGet]
    public Pessoa Get() {
        Pessoa pessoa = new Pessoa("Caio", 26, "13322244433");
        Contrato contrato = new Contrato("caio", 30.32);

        pessoa.contratos.Add(contrato);

        return pessoa;
    }

    [HttpPost]
    public Pessoa Post([FromBody]Pessoa pessoa) {
        return pessoa;
    }

    [HttpPut("{id}")]
    public string Put([FromRoute]int id, [FromBody]Pessoa pessoa) {
        return "Dados do id" + id + "atualizados.";    
        Console.WriteLine(pessoa);
        Console.WriteLine(id);
    }

    [HttpDelete("{id}")]
    public string Delete([FromRoute]int id) {
        return "deletado pessoa de Id " + id;
    }
}