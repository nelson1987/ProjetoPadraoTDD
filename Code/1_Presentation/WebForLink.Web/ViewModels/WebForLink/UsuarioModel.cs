using System.Collections.Generic;
using System.Web.Mvc;

namespace WebForLink.Web.ViewModels
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string DatNascimento { get; set; }
        public string Principal { get; set; }
        public string TrocarSenha { get; set; }
        public string CPF { get; set; }
        public bool? Ativo { get; set; }
        public string Cargo { get; set; }
        public string Login { get; set; }
        public string LoginSSO { get; set; }
        public string Dominio { get; set; }
        public string PrimeiroAcesso { get; set; }
        public string DtAtivacao { get; set; }
        public string DtCriacao { get; set; }
        public string ContaTentativa { get; set; }
        public ContratanteVM Contratante { get; set; }
        public int ContratanteId { get; set; }
        public PapelModel Papel { get; set; }
        public int PapelModelId { get; set; }
        public PerfilVM Perfil { get; set; }
        public int PerfilModelId { get; set; }
        public List<SelectListItem> ContratanteList { get; set; }
        public List<SelectListItem> PapelList { get; set; }
        public List<SelectListItem> PerfilList { get; set; }
        public List<ContratanteVM> ContratantesModelList { get; set; }
        public List<PapelModel> PapelModelList { get; set; }
        public List<PerfilVM> PerfilModelList { get; set; }
    }
}