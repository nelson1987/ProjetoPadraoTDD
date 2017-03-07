using System;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Carrinho : ISelfValidation
    {
        public Carrinho()
        {
        }

        public Carrinho(string loginUsuario, string codigoClienteUsuario, string cnpjConvidado, string radicalConvidado,
            int idFornecedor) : this()
        {
            LoginUsuario = loginUsuario;
            CodigoClienteUsuario = codigoClienteUsuario;
            CnpjConvidado = cnpjConvidado;
            RadicalConvidado = radicalConvidado;
            IdFornecedor = idFornecedor;
        }

        public int Id { get; set; }
        public int IdFornecedor { get; set; }
        public string LoginUsuario { get; set; }
        public string CodigoClienteUsuario { get; set; }
        public DateTime DataConvite { get; set; }
        public int StatusConvite { get; set; }
        public string CnpjConvidado { get; set; }
        public string RadicalConvidado { get; set; }
        public string RazaoSocial { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        //public Carrinho CriarCarrinhoInicial(Carrinho carrinho)
        //{
        //    carrinho.DataConvite = DateTime.Now;
        //    carrinho.StatusConvite = (int)EnumTipoCarrinho.Pendente;

        //    return carrinho;
        //}
        public bool EhValido
        {
            get
            {
                if (!string.IsNullOrEmpty(CodigoClienteUsuario) && !string.IsNullOrEmpty(LoginUsuario) &&
                    !string.IsNullOrEmpty(CnpjConvidado) && !string.IsNullOrEmpty(RadicalConvidado))
                    return true;

                return false;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }
}