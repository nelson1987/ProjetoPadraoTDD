namespace WebForLink.Domain.Enums
{
    public enum EnumTiposFluxo
    {
        None = 0,
        CadastroFornecedorNacional = 10,
        CadastroFornecedorNacionalDireto = 20,
        CadastroFornecedorPF = 30,
        CadastroFornecedorPFDireto = 40,
        CadastroFornecedorEstrangeiro = 50,
        AmpliacaoFornecedor = 60,
        ModificacoesGerais = 70,
        ModificacaoDadosFiscais = 80,
        ModificacaoDadosBancarios = 90,
        ModificacaoDadosContato = 100,
        BloqueioFornecedor = 110,
        DesbloqueioFornecedor = 120,
        ModificacaoDocumentos = 130,
        ModificacaoQuestionarioDinamico = 140,
        ModificacaoEndereco = 150,
        ModificacaoServicoMaterial = 160
    }
}