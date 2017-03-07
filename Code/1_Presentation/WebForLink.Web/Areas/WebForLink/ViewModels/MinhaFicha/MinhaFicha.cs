
namespace WebForLink.Areas.WebForLink.ViewModels.MinhaFicha
{
    public class MinhaFichaCadastralVM
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Cpf { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnae { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public MinhaFichaCadastralReceitaFederalVM RoboReceita;
        public MinhaFichaCadastralSintegraVM RoboSintegra;
        public MinhaFichaCadastralSimplesNacionalVM RoboSimplesNacional;
        public MinhaFichaCadastralDadosEnderecosVM DadosEndereco;
        public MinhaFichaCadastralDadosBancariosVM DadosBancarios;
        public MinhaFichaCadastralDadosContatosVM DadosContatos;
        public MinhaFichaCadastralServicosVM Serviços;
        public MinhaFichaCadastralMateriaisVM Materiais;
    }

    public class MinhaFichaCadastralReceitaFederalVM
    {
    }
    public class MinhaFichaCadastralSintegraVM
    {
    }
    public class MinhaFichaCadastralSimplesNacionalVM
    {
    }
    public class MinhaFichaCadastralDadosEnderecosVM
    {
    }
    public class MinhaFichaCadastralDadosBancariosVM
    {
    }
    public class MinhaFichaCadastralDadosContatosVM
    {
    }
    public class MinhaFichaCadastralServicosVM
    {
    }
    public class MinhaFichaCadastralMateriaisVM
    {
    }
}