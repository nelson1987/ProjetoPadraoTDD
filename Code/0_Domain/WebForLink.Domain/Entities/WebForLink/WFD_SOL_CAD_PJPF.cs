using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class SolicitacaoCadastroFornecedor
    {
        public int ID { get; set; }
        public int SOLICITACAO_ID { get; set; }
        public int CATEGORIA_ID { get; set; }
        public int ORG_COMPRAS_ID { get; set; }
        public int PJPF_TIPO { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string RAZAO_SOCIAL { get; set; }
        public string NOME { get; set; }
        public string NOME_FANTASIA { get; set; }
        public string CNAE { get; set; }
        public string INSCR_ESTADUAL { get; set; }
        public string INSCR_MUNICIPAL { get; set; }
        public string TP_LOGRADOURO { get; set; }
        public string ENDERECO { get; set; }
        public string NUMERO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string CEP { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string UF { get; set; }
        public string PAIS { get; set; }
        public string OBSERVACAO { get; set; }
        public bool EhExpansao { get; set; }
        public int? EXPANSAO_PARA_CONTR_ID { get; set; }
        public string COD_PJPF_ERP { get; set; }
        public int? ROBO_ID { get; set; }
        public string CLIENTE { get; set; }
        public string GRUPO_EMPRESA { get; set; }
        public DateTime? DT_NASCIMENTO { get; set; }
        public virtual FORNECEDOR_CATEGORIA WFD_PJPF_CATEGORIA { get; set; }
        public virtual ROBO WFD_PJPF_ROBO { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }

        public bool PessoaFisica(SolicitacaoCadastroFornecedor cadastroEmpresa)
        {
            return cadastroEmpresa.PessoaFisicaEstrangeira(cadastroEmpresa) ||
                   cadastroEmpresa.PessoaFisicaNacional(cadastroEmpresa);
        }

        public bool PessoaJuridica(SolicitacaoCadastroFornecedor cadastroEmpresa)
        {
            return cadastroEmpresa.PessoaJuridicaEstrangeira(cadastroEmpresa) ||
                   cadastroEmpresa.PessoaJuridicaNacional(cadastroEmpresa);
        }

        public bool PessoaFisicaEstrangeira(SolicitacaoCadastroFornecedor cadastroEmpresa)
        {
            return cadastroEmpresa.PJPF_TIPO == 1;
        }

        public bool PessoaJuridicaEstrangeira(SolicitacaoCadastroFornecedor cadastroEmpresa)
        {
            return cadastroEmpresa.PJPF_TIPO == 2;
        }

        public bool PessoaFisicaNacional(SolicitacaoCadastroFornecedor cadastroEmpresa)
        {
            return cadastroEmpresa.PJPF_TIPO == 3;
        }

        public bool PessoaJuridicaNacional(SolicitacaoCadastroFornecedor cadastroEmpresa)
        {
            return cadastroEmpresa.PJPF_TIPO == 4;
        }
    }
}