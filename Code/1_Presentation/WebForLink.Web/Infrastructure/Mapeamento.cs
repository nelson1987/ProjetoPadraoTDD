using AutoMapper;
using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Infrastructure
{
    public static class Mapeamento
    {
        public static void PopularDadosReceita(FichaCadastralWebForLinkVM ficha, SolicitacaoCadastroFornecedor fornNacional)
        {
            //ficha.RazaoSocial = fornNacional.RAZAO_SOCIAL;
            //ficha.NomeFantasia = fornNacional.NOME_FANTASIA;
            //ficha.Nome = fornNacional.NOME;
            //ficha.CNAE = fornNacional.CNAE;
            //ficha.InscricaoEstadual = fornNacional.INSCR_ESTADUAL;
            //ficha.InscricaoMunicipal = fornNacional.INSCR_MUNICIPAL;
            //ficha.TipoFornecedor = fornNacional.PJPF_TIPO;
            //ficha.Observacao = fornNacional.OBSERVACAO;
            //ficha.CategoriaId = fornNacional.CATEGORIA_ID;

            Mapper.Map<FichaCadastralWebForLinkVM>(fornNacional);

            if (ficha.TipoFornecedor != 2)
                ficha.CNPJ_CPF = fornNacional.PJPF_TIPO == 3
                    ? Convert.ToUInt64(fornNacional.CPF).ToString(@"000\.000\.000\-00")
                    : Convert.ToUInt64(fornNacional.CNPJ).ToString(@"00\.000\.000\/0000\-00");
        }
        public static void PopularDadosReceita(FichaCadastralWebForLinkVM ficha, Fornecedor fornNacional)
        {
            //ficha.CNPJ_CPF = fornNacional.TIPO_PJPF_ID == 3
            //    ? Convert.ToUInt64(fornNacional.CPF).ToString(@"000\.000\.000\-00")
            //    : Convert.ToUInt64(fornNacional.CNPJ).ToString(@"00\.000\.000\/0000\-00");
            //ficha.RazaoSocial = fornNacional.TIPO_PJPF_ID != 3
            //    ? fornNacional.RAZAO_SOCIAL
            //    : fornNacional.NOME;
            //ficha.NomeFantasia = fornNacional.NOME_FANTASIA;
            //ficha.Nome = fornNacional.NOME;
            //ficha.CNAE = fornNacional.CNAE;
            //ficha.InscricaoEstadual = fornNacional.INSCR_ESTADUAL;
            //ficha.InscricaoMunicipal = fornNacional.INSCR_MUNICIPAL;

            Mapper.Map<FichaCadastralWebForLinkVM>(fornNacional);
        }
        public static void PopularEndereco(FichaCadastralWebForLinkVM ficha, SolicitacaoCadastroFornecedor solicitacaoCadastroFornecedor)
        {
            //ficha.Bairro = solicitacaoCadastroFornecedor.BAIRRO;
            //ficha.Cep = solicitacaoCadastroFornecedor.CEP;
            //ficha.Cidade = solicitacaoCadastroFornecedor.CIDADE;
            //ficha.Complemento = solicitacaoCadastroFornecedor.COMPLEMENTO;
            //ficha.Endereco = solicitacaoCadastroFornecedor.ENDERECO;
            //ficha.Numero = solicitacaoCadastroFornecedor.NUMERO;
            //ficha.Pais = solicitacaoCadastroFornecedor.PAIS;
            //ficha.Estado = solicitacaoCadastroFornecedor.UF;
            //ficha.TipoLogradouro = solicitacaoCadastroFornecedor.TP_LOGRADOURO;
            Mapper.Map<FichaCadastralWebForLinkVM>(solicitacaoCadastroFornecedor);
        }
        public static void PopularEndereco(FichaCadastralWebForLinkVM ficha, Fornecedor fornecedor)
        {
            //ficha.Bairro = fornecedor.BAIRRO;
            //ficha.Cep = fornecedor.CEP;
            //ficha.Cidade = fornecedor.CIDADE;
            //ficha.Complemento = fornecedor.COMPLEMENTO;
            //ficha.Endereco = fornecedor.ENDERECO;
            //ficha.Numero = fornecedor.NUMERO;
            //ficha.Pais = fornecedor.PAIS;
            //ficha.Estado = fornecedor.UF;
            Mapper.Map<FichaCadastralWebForLinkVM>(fornecedor);
        }
    }
}