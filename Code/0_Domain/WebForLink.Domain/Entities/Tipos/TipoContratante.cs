namespace WebForLink.Domain.Entities.Tipos
{
    //public abstract class TipoContratante
    //{
    //    private TipoContratante()
    //    {
    //    }

    //    protected TipoContratante(string nome)
    //        : this()
    //    {
    //        Nome = nome;
    //    }

    //    /*
    //    protected TipoContratante(int id, string nome)
    //        : this(nome)
    //    {
    //        Id = id;
    //        Nome = nome;
    //    }
    //    */
    //    public int Id { get; private set; }
    //    public string Nome { get; private set; }
    //    public List<Contratante> Contratantes { get; private set; }
    //}

    /// <summary>
    ///     Cliente Âncora
    /// </summary>
    public class ClienteAncora : Contratante
    {
        //public ClienteAncora(string razaoSocial)
        //    : base(razaoSocial, new EmpressaPessoaJuridica())
        //{
        //}
        public ClienteAncora(string razaoSocial, Aplicacao aplicacao)
            : base(razaoSocial, new EmpresaPessoaJuridica(), aplicacao)
        {
        }
    }

    /// <summary>
    ///     Fornecedor Individual
    /// </summary>
    public class FornecedorIndividual : Contratante
    {
        //public FornecedorIndividual(string razaoSocial, TipoEmpresa tipoEmpresa)
        //    : base(razaoSocial, tipoEmpresa)
        //{
        //}
        public FornecedorIndividual(string razaoSocial, TipoEmpresa tipoEmpresa, Aplicacao aplicacao)
            : base(razaoSocial, tipoEmpresa, aplicacao)
        {
        }
    }

    /// <summary>
    ///     Fabricante Âncora
    /// </summary>
    public class FabricanteAncora : Contratante
    {
        //public FabricanteAncora(string razaoSocial)
        //    : base(razaoSocial, new EmpressaPessoaJuridica())
        //{
        //}
        public FabricanteAncora(string razaoSocial, Aplicacao aplicacao)
            : base(razaoSocial, new EmpresaPessoaJuridica(), aplicacao)
        {
        }
    }
}