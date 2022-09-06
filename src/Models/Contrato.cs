namespace src.Models;

public class Contrato {
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public string TokenId { get; set; }
    public double Valor { get; set; }
    public bool Pago { get; set; }
    public int PessoaId { get; set; }

    public Contrato()
    {
        this.DataCriacao = DateTime.Now;
        this.TokenId = "000000";
        this.Valor = 0;
        this.Pago = false;
    }

    public Contrato(string TokenId, double Valor)
    {
        this.DataCriacao = DateTime.Now;
        this.TokenId = TokenId;
        this.Valor = Valor;
        this.Pago = false;
    }
}