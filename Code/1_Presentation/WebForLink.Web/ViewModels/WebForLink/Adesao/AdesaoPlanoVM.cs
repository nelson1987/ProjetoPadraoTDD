using System.Collections.Generic;
using System.Web.Mvc;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Web.ViewModels.Adesao
{
    public class AdesaoPlanoVM
    {
        public AdesaoPlanoVM()
        {
        }
        public AdesaoPlanoVM(int id, string nome, int usuarios, int disco, int fornecedores, double preco, string chave, string esquema, UrlHelper url, List<AdesaoPropriedadePlanoVM> propriedades)
        {
            Id = id;
            UsuariosCount = usuarios;
            TamanhoDisco = disco;
            FornecedoresCadastrados = fornecedores;
            Preco = preco;
            Nome = nome;
            Chave = chave;
            Esquema = esquema;
            UrlChave = CriarChave(url);
            Propriedades = propriedades;
        }
        public int Id { get; set; }
        public int UsuariosCount { get; set; }
        public int TamanhoDisco { get; set; }
        public int FornecedoresCadastrados { get; set; }
        public double Preco { get; internal set; }
        public string Nome { get; internal set; }
        public string UrlChave { get; private set; }
        public string Chave { get; internal set; }
        public string Esquema { get; internal set; }

        public List<AdesaoPropriedadePlanoVM> Propriedades { get; internal set; }

        public string CriarChave(UrlHelper Url)
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            string chaveCriptografada = Cripto.Criptografar(string.Format("planoId={0}", Id), Chave);
            var novo = Url.Action("PreCadastro", "Adesao", new
            {
                chaveUrl = chaveCriptografada
            }, Esquema);
            return novo;
        }
    }
}