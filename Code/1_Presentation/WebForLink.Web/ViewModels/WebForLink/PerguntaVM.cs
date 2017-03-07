using System.Collections.Generic;
using System.Web.Mvc;

namespace WebForLink.Web.ViewModels
{
    public class PerguntaVM
    {
        public int Id { get; set; }
        public int AbaId { get; set; }
        public string Titulo { get; set; }
        public string TipoDado { get; set; }
        public string ExibeNome { get; set; }
        public bool Dominio { get; set; }
        public bool Obrigatorio { get; set; }
        public bool Leitura { get; set; }
        public bool Escrita { get; set; }
        public int SolicitacaoId { get; set; }
        public int RespostaId { get; set; }
        public int RespostaFornecedorId { get; set; }
        public int Tamanho { get; set; }
        public int Ordem { get; set; }
        public bool EPai { get; set; }
        public int? PerguntaPai { get; set; }
        public string Resposta { get; set; }
        public string RespostaFornecedor { get; set; }
        public List<SelectListItem> DominioList { get; set; }
        public List<string> RespostaCheckbox { get; set; }
        public int DominioListId { get; set; }
        public int PaiRespondido { get; set; }
        public object Objeto
        {
            get
            {
                string attributo = "form-control Resposta";

                if (TipoDado == "NUMERAL")
                {
                    attributo = "form-control Resposta tdNumeral";
                }
                if (TipoDado == "DECIMAL V2")
                {
                    attributo = "form-control Resposta tdDecimalV2";
                }
                if (TipoDado == "DECIMAL V3")
                {
                    attributo = "form-control Resposta tdDecimalV3";
                }
                if (TipoDado == "DATA")
                {
                    attributo = "form-control Resposta tdData";
                }
                if (Escrita)
                {
                    return new
                    {
                        @Class = attributo,
                        //onChange = EPai != null ? "formatarPaieFilho(" + Id + ", this.value);" : string.Empty,
                        maxlength = Tamanho,
                        identificador = Id,
                        permissao = "true"
                    };
                }
                return new
                {
                    @Class = attributo,
                    //onChange = EPai != null ? "formatarPaieFilho(" + Id + ", this.value);" : string.Empty,
                    maxlength = Tamanho,
                    Disabled = Escrita == false,
                    identificador = Id,
                    permissao = "true"
                };

            }
        }
        public bool PulaLinha { get; set; }
    }
}