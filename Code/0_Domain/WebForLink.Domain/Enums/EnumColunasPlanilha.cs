using System.ComponentModel;

namespace WebForLink.Domain.Enums
{
    public enum EnumColunasPlanilha
    {
        [Description("CNPJ / CPF")] CNPJouCPF = 1,
        [Description("Razão Social / Nome")] RazaoSocialOuNome = 2,
        [Description("Nascimento")] DataNascimento = 3,
        [Description("Nome Contato")] NomeContato = 4,
        [Description("E-Mail")] Email = 5,
        [Description("Telefone")] Telefone = 6,
        [Description("Celular")] Celular = 7,
        [Description("Novo Fornecedor")] NovoFornecedor = 8,
        [Description("Código ERP")] CodigoERP = 9,
        [Description("Inscrição Estadual")] InscricaoEstadual = 10
    }
}