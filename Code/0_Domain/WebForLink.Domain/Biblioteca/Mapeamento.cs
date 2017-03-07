using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebForDocs.Dominio.Models;
using WebForDocs.ViewModels;

namespace WebForDocs.Biblioteca
{
    public static class Mapeamento
    {
        public static void PopularDadosReceita(FichaCadastralVM ficha, WFD_SOL_CAD_PJPF fornNacional)
        {
            ficha.RazaoSocial = fornNacional.RAZAO_SOCIAL;
            ficha.NomeFantasia = fornNacional.NOME_FANTASIA;
            ficha.Nome = fornNacional.NOME;
            ficha.CNAE = fornNacional.CNAE;
            if (ficha.TipoFornecedor != 2)
                ficha.CNPJ_CPF = fornNacional.PJPF_TIPO == 3
                    ? Convert.ToUInt64(fornNacional.CPF).ToString(@"000\.000\.000\-00")
                    : Convert.ToUInt64(fornNacional.CNPJ).ToString(@"00\.000\.000\/0000\-00");
            ficha.InscricaoEstadual = fornNacional.INSCR_ESTADUAL;
            ficha.InscricaoMunicipal = fornNacional.INSCR_MUNICIPAL;
            ficha.TipoFornecedor = fornNacional.PJPF_TIPO;
            ficha.Observacao = fornNacional.OBSERVACAO;
            ficha.CategoriaId = fornNacional.CATEGORIA_ID;
        }
        public static void PopularDadosReceita(FichaCadastralVM ficha, WFD_PJPF fornNacional)
        {
            ficha.CNPJ_CPF = fornNacional.TIPO_PJPF_ID == 3
                ? Convert.ToUInt64(fornNacional.CPF).ToString(@"000\.000\.000\-00")
                : Convert.ToUInt64(fornNacional.CNPJ).ToString(@"00\.000\.000\/0000\-00");
            ficha.RazaoSocial = fornNacional.TIPO_PJPF_ID != 3
                ? fornNacional.RAZAO_SOCIAL
                : fornNacional.NOME;
            ficha.NomeFantasia = fornNacional.NOME_FANTASIA;
            ficha.Nome = fornNacional.NOME;
            ficha.CNAE = fornNacional.CNAE;
            ficha.InscricaoEstadual = fornNacional.INSCR_ESTADUAL;
            ficha.InscricaoMunicipal = fornNacional.INSCR_MUNICIPAL;
        }
        public static void PopularEndereco(FichaCadastralVM ficha, WFD_SOL_CAD_PJPF solicitacaoCadastroFornecedor)
        {
            ficha.Bairro = solicitacaoCadastroFornecedor.BAIRRO;
            ficha.Cep = solicitacaoCadastroFornecedor.CEP;
            ficha.Cidade = solicitacaoCadastroFornecedor.CIDADE;
            ficha.Complemento = solicitacaoCadastroFornecedor.COMPLEMENTO;
            ficha.Endereco = solicitacaoCadastroFornecedor.ENDERECO;
            ficha.Numero = solicitacaoCadastroFornecedor.NUMERO;
            ficha.Pais = solicitacaoCadastroFornecedor.PAIS;
            ficha.Estado = solicitacaoCadastroFornecedor.UF;
            ficha.TipoLogradouro = solicitacaoCadastroFornecedor.TP_LOGRADOURO;
        }
        public static void PopularEndereco(FichaCadastralVM ficha, WFD_PJPF fornecedor)
        {
            ficha.Bairro = fornecedor.BAIRRO;
            ficha.Cep = fornecedor.CEP;
            ficha.Cidade = fornecedor.CIDADE;
            ficha.Complemento = fornecedor.COMPLEMENTO;
            ficha.Endereco = fornecedor.ENDERECO;
            ficha.Numero = fornecedor.NUMERO;
            ficha.Pais = fornecedor.PAIS;
            ficha.Estado = fornecedor.UF;
        }
    }
}