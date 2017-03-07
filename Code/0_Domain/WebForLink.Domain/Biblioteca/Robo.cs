using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForDocs.Biblioteca
{
    public class Robo
    {
        public int code { get; set; }

        public string html { get; set; }

        public data data { get; set; }

        public string uuid { get; set; }
    }

    public class data
    {
        public string data_situacao_cadastral { get; set; }

        public string hora_emissao { get; set; }

        //public string municipio_ibge { get; set; }

        //public int codigo_ibge { get; set; }

        public List<string> outras_atividades { get; set; }

        public string situacao_cadastral { get; set; }

        public string data_abertura { get; set; }

        public string razao_social { get; set; }

        public string razao_social_abreviado { get; set; } //Fazer com os demais campos abreviados

        public string ente_federativo_responsavel { get; set; }

        public string telefone { get; set; }

        public string cnpj { get; set; }

        public string endereco_eletronico { get; set; }

        public string obs_ibge { get; set; }

        public string bairro { get; set; }

        public string matriz_filial { get; set; }

        public string numero { get; set; }

        public string situacao_especial { get; set; }

        public string cep { get; set; }

        public string complemento { get; set; }

        public string motivo_situacao_cadastral { get; set; }

        public string municipio { get; set; }

        public string logradouro { get; set; }

        public string nome_fantasia { get; set; }

        public string atividade_economica_principal { get; set; }

        public string natureza_juridica { get; set; }

        public string data_emissao { get; set; }

        public string data_situacao_especial { get; set; }

        public string uf { get; set; }

    }
}